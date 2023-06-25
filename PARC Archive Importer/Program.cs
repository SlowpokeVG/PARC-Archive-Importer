using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PARC_Archive_Importer
{
    class HeaderbeforeWide
    {
        public int id { get; set; }
        public int filestart { get; set; }
        public int filesize { get; set; }
        public int zeroestoadd { get; set; }

        public HeaderbeforeWide(int ID, int FileStart, int FileSize, int ZeroesToAdd)
        {
            ID = id;
            FileStart = filestart;
            FileSize = filesize;
            ZeroesToAdd = zeroestoadd;
        }
    }

    public static class Extensions
    {
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }

        public static int ReadInt32LE(byte[] ByteArray, int Offset)
        {
            byte[] ValueBytes = ByteArray.SubArray(Offset, 4);
            Array.Reverse(ValueBytes);
            return BitConverter.ToInt32(ValueBytes, 0);
        }

        public static void SetListItem(ListView TargetListView, int Row, int Column, int Value, bool HexString = false)
        {
            ListViewItem.ListViewSubItem item = TargetListView.Items[Row].SubItems[Column];
            item.Tag = Value;
            item.Text = Convert.ToString(Value, HexString ? 16 : 10);
        }

        public static int GetListItem(ListView TargetListView, int Row, int Column)
        {
            return (int) TargetListView.Items[Row].SubItems[Column].Tag;
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
