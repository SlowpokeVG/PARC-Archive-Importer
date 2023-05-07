// -------------------------------------------------------
// © Kaplas. Licensed under MIT. See LICENSE for details.
// -------------------------------------------------------
using ParLibrary.Sllz;

namespace ParLibrary.Sllz
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    /// <summary>
    /// Manages SLLZ compression used in Yakuza games.
    /// </summary>
    public class Compressor
    {
        private const int MAX_WINDOW_SIZE = 4096;
        private const int MAX_ENCODED_LENGTH = 18;

        private int Version;
        private int Endianness;

        /// <summary>
        /// Initializes the compressor parameters.
        /// </summary>
        /// <param name="parameters">Compressor configuration.</param>
        public Compressor(CompressorParameters parameters)
        {
            Version = parameters.Version;
            Endianness = parameters.Endianness;
        }

        public Compressor(int Version, int Endianness)
        {
            this.Version = Version;
            this.Endianness = Endianness;
        }

        public Compressor()
        {
            Version = 1;
            Endianness = 0;
        }

        /// <summary>Compresses a file with SLLZ.</summary>
        /// <returns>The compressed file.</returns>
        /// <param name="source">Source file to compress, loaded into a byte array.</param>
        public byte[] Convert(byte[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Compress(source, Version, Endianness);
        }

        private static byte[] Compress(byte[] inputData, int Version, int Endianness)
        {
            byte[] compressedData;
            uint compressedDataLength;

            if (Version == 1)
            {
                try
                {
                    compressedData = new byte[(uint)inputData.Length + 2048];
                    compressedDataLength = CompressV1(inputData, compressedData);
                }
                catch (SllzCompressorException)
                {
                    compressedData = inputData;
                    compressedDataLength = (uint)inputData.Length;
                }
            }
            else if (Version == 2)
            {
                if (inputData.Length < 0x1B)
                {
                    throw new FormatException($"SLLZv2: Input size must be more than 0x1A.");
                }

                compressedData = CompressV2(inputData).ToArray();
                compressedDataLength = (uint)compressedData.Length;
            }
            else
            {
                throw new FormatException($"SLLZ: Unknown compression version {Version}.");
            }

            if (compressedData == inputData)
            {
                return inputData;
            }

            byte[] outputData = new byte[compressedDataLength + 0x10];
            Encoding.ASCII.GetBytes("SLLZ").CopyTo(outputData, 0);
            outputData[4] = (byte)Endianness;
            outputData[5] = (byte)Version;
            // These next three values are LE by default.
            outputData[6] = 0x10; // Header size
            BitConverter.GetBytes(inputData.Length).CopyTo(outputData, 8);
            BitConverter.GetBytes(outputData.Length).CopyTo(outputData, 12); // data + header

            // Flip LE values if Endianness is BE.
            if (Endianness == 1)
            {
                Array.Reverse(outputData, 6, 2);
                Array.Reverse(outputData, 8, 4);
                Array.Reverse(outputData, 12, 4);
            }

            Array.Copy(compressedData, 0, outputData, 0x10, compressedDataLength);

            return outputData;
        }

        private static uint CompressV1(byte[] inputData, byte[] outputData)
        {
            uint outputSize = (uint)outputData.Length;

            uint inputPosition = 0;
            uint outputPosition = 0;
            byte currentFlag = 0x00;
            var bitCount = 0;
            long flagPosition = outputPosition;

            outputData[flagPosition] = 0x00;
            outputPosition++;

            if (outputPosition >= outputSize)
            {
                throw new SllzCompressorException("Compressed size is bigger than original size.");
            }

            while (inputPosition < inputData.Length)
            {
                uint windowSize = Math.Min(inputPosition, MAX_WINDOW_SIZE);
                uint maxOffsetLength = Math.Min((uint)(inputData.Length - inputPosition), MAX_ENCODED_LENGTH);

                Tuple<uint, uint> match = FindMatch(inputData, inputPosition, windowSize, maxOffsetLength);

                if (match == null)
                {
                    // currentFlag |= (byte)(0 << (7 - bitCount)); // It's zero
                    bitCount++;

                    if (bitCount == 0x08)
                    {
                        outputData[flagPosition] = currentFlag;

                        currentFlag = 0x00;
                        bitCount = 0x00;
                        flagPosition = outputPosition;
                        outputData[flagPosition] = 0x00;
                        outputPosition++;
                        if (outputPosition >= outputSize)
                        {
                            throw new SllzCompressorException("Compressed size is bigger than original size.");
                        }
                    }

                    outputData[outputPosition] = inputData[inputPosition];
                    inputPosition++;
                    outputPosition++;

                    if (outputPosition >= outputSize)
                    {
                        throw new SllzCompressorException("Compressed size is bigger than original size.");
                    }
                }
                else
                {
                    currentFlag |= (byte)(1 << 7 - bitCount);
                    bitCount++;

                    if (bitCount == 0x08)
                    {
                        outputData[flagPosition] = currentFlag;

                        currentFlag = 0x00;
                        bitCount = 0x00;
                        flagPosition = outputPosition;
                        outputData[flagPosition] = 0x00;
                        outputPosition++;

                        if (outputPosition >= outputSize)
                        {
                            throw new SllzCompressorException("Compressed size is bigger than original size.");
                        }
                    }

                    var offset = (short)(match.Item1 - 1 << 4);
                    var size = (short)(match.Item2 - 3 & 0x0F);

                    var tuple = (short)(offset | size);

                    outputData[outputPosition] = (byte)tuple;
                    outputPosition++;

                    if (outputPosition >= outputSize)
                    {
                        throw new SllzCompressorException("Compressed size is bigger than original size.");
                    }

                    outputData[outputPosition] = (byte)(tuple >> 8);
                    outputPosition++;

                    if (outputPosition >= outputSize)
                    {
                        throw new SllzCompressorException("Compressed size is bigger than original size.");
                    }

                    inputPosition += match.Item2;
                }
            }

            outputData[flagPosition] = currentFlag;

            return outputPosition;
        }

        private static MemoryStream CompressV2(byte[] inputData)
        {
            MemoryStream outputDataStream = new MemoryStream();
            var writer = new BinaryWriter(outputDataStream);

            var currentPosition = 0;

            while (currentPosition < inputData.Length)
            {
                int decompressedChunkSize = Math.Min(inputData.Length - currentPosition, 0x10000);
                var decompressedData = new byte[decompressedChunkSize];
                Array.Copy(inputData, currentPosition, decompressedData, 0, decompressedChunkSize);

                var compressedData = ZLibCompress(decompressedData);

                int compressedDataLength = compressedData.Length + 5;
                writer.Write((byte)(compressedDataLength >> 16));
                writer.Write((byte)(compressedDataLength >> 8));
                writer.Write((byte)compressedDataLength);
                int temp = decompressedChunkSize - 1;
                writer.Write((byte)(temp >> 8));
                writer.Write((byte)temp);
                writer.Write(compressedData);

                currentPosition += decompressedChunkSize;
            }

            return outputDataStream;
        }

        private static Tuple<uint, uint> FindMatch(byte[] inputData, uint inputPosition, uint windowSize, uint maxOffsetLength)
        {
            ReadOnlySpan<byte> bytes = inputData;
            ReadOnlySpan<byte> data = bytes.Slice((int)(inputPosition - windowSize), (int)windowSize);

            uint currentLength = maxOffsetLength;

            while (currentLength >= 3)
            {
                ReadOnlySpan<byte> pattern = bytes.Slice((int)inputPosition, (int)currentLength);

                int pos = data.LastIndexOf(pattern);

                if (pos >= 0)
                {
                    return new Tuple<uint, uint>((uint)(windowSize - pos), currentLength);
                }

                currentLength--;
            }

            return null;
        }

        private static byte[] ZLibCompress(byte[] decompressedData)
        {
            using var inputMemoryStream  = new MemoryStream(decompressedData);
            using var outputMemoryStream = new MemoryStream();
            using var zLibStream = new ZLibStream(outputMemoryStream, CompressionLevel.SmallestSize);

            inputMemoryStream.CopyTo(zLibStream);
            zLibStream.Close();

            return outputMemoryStream.ToArray();
        }
    }
}
