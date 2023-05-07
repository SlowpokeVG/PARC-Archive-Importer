using ParLibrary.Sllz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PARC_Archive_Importer
{
    public partial class Form1 : Form
    {
        private int FileCount;
        private int FileStartOffset;
        private int FolderCount;

        private int FolderStartOffset;
        private byte[] InterOpenedArchive;

        private string OpenedArchName;
        private string OpenedArchPath;

        private int CompressionOption = 2;
        private byte[][] CompressedFiles;

        private bool WideFile = false;


        public Form1()
        {
            InitializeComponent();
            comboBoxCompression.SelectedIndex = 2;
        }

        public static void WriteInt32(int value, byte[] target, int address)
        {
            byte[] result = new byte[4];
            result[0] = (byte)((value & 0xFF000000) >> 24);
            result[1] = (byte)((value & 0x00FF0000) >> 16);
            result[2] = (byte)((value & 0x0000FF00) >> 8);
            result[3] = (byte)(value & 0x000000FF);

            for (int i = 0; i <= 3; i++) target[address + i] = result[i];
        }


        private void ParseSourceArray(byte[] OrigArchive)
        {
            listArch.Items.Clear();
            listArch.Update();
            listArch.Refresh();

            buttonWiden.Enabled = false;
            buttonWiden.Text = "Widen";

            FileCount = Extensions.ReadInt32LE(OrigArchive, 24);
            FolderCount = Extensions.ReadInt32LE(OrigArchive, 16);
            FileStartOffset = Extensions.ReadInt32LE(OrigArchive, 28);
            FolderStartOffset = Extensions.ReadInt32LE(OrigArchive, 20);

            List<string> FileList = new List<string>();
            FileList.Clear();
            string[] FolderNames = new string[FileCount];
            int FolderCounter = 0;

            for (int Folder = 0; Folder < FolderCount; Folder++)
            {
                int FilesInFolder = Extensions.ReadInt32LE(OrigArchive, FolderStartOffset + 8 + Folder * 32);
                for (int m = 0; m < FilesInFolder; m++)
                    FolderNames[FolderCounter + m] = Encoding.ASCII.GetString(OrigArchive, Folder * 64 + 32, 64)
                        .TrimEnd((char)0);

                FolderCounter += FilesInFolder;
            }

            for (int i = 0; i < FileCount; i++)
            {
                string NameOfFile = Encoding.ASCII.GetString(OrigArchive, 32 + (FolderCount + i) * 64, 64)
                    .TrimEnd((char)0);
                FileList.Add(NameOfFile);
                int FileDescriptionOffset = FileStartOffset + i * 32;
                int FileStart = Extensions.ReadInt32LE(OrigArchive, 12 + FileDescriptionOffset);
                int FileSizeOffset = Extensions.ReadInt32LE(OrigArchive, 4 + FileDescriptionOffset);
                int FileSizeCompressedOffset = Extensions.ReadInt32LE(OrigArchive, 8 + FileDescriptionOffset);
                string IsCompressed = "No";
                var Parameters = new CompressorParameters(0, 0);

                if (OrigArchive[FileDescriptionOffset] != 0x00)
                {
                    Parameters.Endianness = OrigArchive[FileStart + 4];
                    Parameters.Version = OrigArchive[FileStart + 5];
                    IsCompressed = $"Yes  (v{Parameters.Version})";
                }

                ListViewItem item = new ListViewItem();
                item.Text = NameOfFile;
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add(IsCompressed);
                item.SubItems[5].Tag = Parameters;
                item.SubItems.Add("");
                item.SubItems.Add(FolderNames[i]);
                listArch.Items.Add(item);

                Extensions.SetListItem(listArch, i, 1, FileDescriptionOffset, radioButtonHex.Checked);
                Extensions.SetListItem(listArch, i, 2, FileStart, radioButtonHex.Checked);
                Extensions.SetListItem(listArch, i, 3, FileSizeOffset, radioButtonHex.Checked);
                Extensions.SetListItem(listArch, i, 4, FileSizeCompressedOffset, radioButtonHex.Checked);
                Extensions.SetListItem(listArch, i, 6, i + 1);
            }
        }

        private bool IsWide(ListView list)
        {
            bool wide = true;
            for (int i = 0; i < list.Items.Count - 1; i++)
                if ((Extensions.GetListItem(list, i + 1, 2) -
                     Extensions.GetListItem(list, i, 2)) % 2048 != 0)
                {
                    wide = false;
                    break;
                }

            return wide;
        }

        private void buttonOpenArch_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Original File";
            theDialog.Filter = "PARC archives (*.par)|*.par|All files (*.*)|*.*";

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                Refresh();

                LoadArchive(theDialog.FileName);

                Cursor = Cursors.Default;
            }
        }

        private void LoadArchive(string filename)
        {
            if (listImport.Items.Count > 0)
            {
                if (checkBoxMuteWarnings.Checked
                 || MessageBox.Show("The import list will be reset. Is that okay?",
                                    "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    CompressedFiles = null;
                    listImport.Items.Clear();
                    listImport.Update();
                    listImport.Refresh();

                    buttonInject.Enabled = false;
                    buttonClear.Enabled = false;
                }
                else
                    return;
            }

            try
            {
                OpenedArchName = Path.GetFileName(filename);
                Text = OpenedArchName + " - PARC Archive Importer";
                OpenedArchPath = Path.GetDirectoryName(filename);
                InterOpenedArchive = File.ReadAllBytes(filename);

                listArch.Enabled = false;
                listArch.Visible = false;

                ParseSourceArray(InterOpenedArchive);

                listArch.Enabled = true;
                listArch.Visible = true;

                listArchOriginalReference.Items.Clear();

                foreach (ListViewItem item in listArch.Items)
                    listArchOriginalReference.Items.Add((ListViewItem)item.Clone());

                WideFile = IsWide(listArchOriginalReference);

                buttonWiden.Enabled = !WideFile;
                if (WideFile) buttonWiden.Text = "Wide";

                buttonImportFiles.Enabled = true;
                comboBoxCompression.Enabled = true;
                groupBoxCompressionVersion.Enabled = comboBoxCompression.SelectedIndex != 0;
                buttonSave.Enabled = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonWiden_Click(object sender, EventArgs e)
        {
            Enabled = false;
            listArch.Enabled = false;
            listArch.Visible = false;
            Cursor = Cursors.WaitCursor;

            WideFile = true;
            UpdateFileStarts();

            buttonRevert.Enabled = true;
            buttonWiden.Enabled = false;
            buttonWiden.Text = "Wide";

            Cursor = Cursors.Default;
            listArch.Enabled = true;
            listArch.Visible = true;
            Enabled = true;

        }

        private void PushListToArchiveFileDescs(ListView list, byte[] Archive)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                int FileDescriptionOffset = Extensions.GetListItem(list, i, 1);

                Archive[FileDescriptionOffset] = (byte)(list.Items[i].SubItems[5].Text != "No" ? 0x80 : 0x00);
                WriteInt32(Extensions.GetListItem(list, i, 3), Archive, 4 + FileDescriptionOffset);
                WriteInt32(Extensions.GetListItem(list, i, 4), Archive, 8 + FileDescriptionOffset);
                WriteInt32(Extensions.GetListItem(list, i, 2), Archive, 12 + FileDescriptionOffset);
            }

            if (Extensions.ReadInt32LE(Archive, 12) != 0)
            {
                int ParSize = Extensions.GetListItem(list, list.Items.Count - 1, 2)
                            + Extensions.GetListItem(list, list.Items.Count - 1, 4);
                WriteInt32(ParSize, Archive, 12);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog DialogSave = new SaveFileDialog();
            DialogSave.Title = "Save Edited File";
            DialogSave.Filter = "PARC archives (*.par)|*.par";
            DialogSave.FileName = OpenedArchName;
            DialogSave.InitialDirectory = OpenedArchPath;

            if (DialogSave.FileName != "" && DialogSave.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                Enabled = false;
                Refresh();

                byte[] ArchiveCopy = new byte[InterOpenedArchive.Length];
                InterOpenedArchive.CopyTo(ArchiveCopy, 0);

                PushListToArchiveFileDescs(listArch, ArchiveCopy);

                FileStream fcreate = File.Open(DialogSave.FileName, FileMode.Create, FileAccess.Write);
                fcreate.Write(ArchiveCopy, 0, Extensions.GetListItem(listArchOriginalReference, 0, 2));

                for (int m = 0; m < listArch.Items.Count; m++)
                {
                    bool IsImported = false;

                    for (int n = 0; n < listImport.Items.Count; n++)
                        if (listImport.Items[n].SubItems[4].Text == "Yes" && listImport.Items[n].SubItems[8].Text != "No")
                            if (Extensions.GetListItem(listImport, n, 3) - 1 == m)
                            {
                                if (listArch.Items[m].SubItems[5].Text == "No")
                                {
                                    FileStream import = File.Open(listImport.Items[n].SubItems[5].Text, FileMode.Open);
                                    import.CopyTo(fcreate);
                                    import.Flush();
                                    import.Close();
                                }
                                else
                                    fcreate.Write(CompressedFiles[n], 0, CompressedFiles[n].Length);

                                IsImported = true;
                                break;
                            }

                    if (!IsImported)
                        fcreate.Write(ArchiveCopy, Extensions.GetListItem(listArchOriginalReference, m, 2),
                            Extensions.GetListItem(listArchOriginalReference, m, 4));

                    if (m < listArchOriginalReference.Items.Count - 1)
                    {
                        if (Extensions.GetListItem(listArch, m + 1, 2) > Extensions.GetListItem(listArch, m, 4) +
                            Extensions.GetListItem(listArch, m, 2))
                        {
                            byte[] Padding = new byte[Extensions.GetListItem(listArch, m + 1, 2)
                                                    - Extensions.GetListItem(listArch, m, 4)
                                                    - Extensions.GetListItem(listArch, m, 2)];
                            fcreate.Write(Padding, 0, Padding.Length);
                        }
                    }
                    else if (checkBoxPadLast.Checked)
                    {
                        byte[] Padding = new byte[2048 - (Extensions.GetListItem(listArch, m, 2) + Extensions.GetListItem(listArch, m, 4)) % 2048];
                        fcreate.Write(Padding, 0, Padding.Length);
                    }
                }

                fcreate.Flush();
                fcreate.Close();

                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void buttonImportFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Original File";
            theDialog.Filter = "All files (*.*)|*.*";
            theDialog.InitialDirectory = OpenedArchPath;
            theDialog.Multiselect = true;
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                if (listImport.Items.Count > 0 && !checkBoxMuteWarnings.Checked)
                {
                    var result = MessageBox.Show("Discard the current import list?",
                                                 "Warning", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Cancel)
                        return;

                    else if (result == DialogResult.Yes)
                    {
                        CompressedFiles = null;
                        listImport.Items.Clear();
                        listImport.Update();
                        listImport.Refresh();
                    }
                }

                Cursor = Cursors.WaitCursor;
                listImport.Enabled = false;
                listImport.Visible = false;

                foreach (string InjectFile in theDialog.FileNames)
                {
                    List<int> IdInArchive = new List<int>();
                    foreach (ListViewItem item in listArch.Items)
                        if (string.Equals(item.Text, Path.GetFileName(InjectFile),
                                StringComparison.CurrentCultureIgnoreCase))
                            IdInArchive.Add((int)item.SubItems[6].Tag);

                    if (IdInArchive.Count == 1)
                    {
                        ListViewItem ImportedItem = new ListViewItem();
                        ImportedItem.Text = Path.GetFileName(InjectFile);
                        int length = (int)new FileInfo(InjectFile).Length;
                        ImportedItem.SubItems.Add(listArch.Items[IdInArchive[0] - 1].SubItems[1].Text);
                        ImportedItem.SubItems.Add(listArch.Items[IdInArchive[0] - 1].SubItems[2].Text);
                        ImportedItem.SubItems.Add(listArch.Items[IdInArchive[0] - 1].SubItems[6].Text);
                        ImportedItem.SubItems[1].Tag = listArch.Items[IdInArchive[0] - 1].SubItems[1].Tag;
                        ImportedItem.SubItems[2].Tag = listArch.Items[IdInArchive[0] - 1].SubItems[2].Tag;
                        ImportedItem.SubItems[3].Tag = listArch.Items[IdInArchive[0] - 1].SubItems[6].Tag;
                        ImportedItem.SubItems.Add("Yes");
                        ImportedItem.SubItems.Add(InjectFile);
                        ImportedItem.SubItems.Add(Convert.ToString(length, radioButtonHex.Checked ? 16 : 10));
                        ImportedItem.SubItems[6].Tag = length;
                        ImportedItem.SubItems.Add("--");
                        ImportedItem.SubItems[7].Tag = length;
                        ImportedItem.SubItems.Add("No");
                        listImport.Items.Add(ImportedItem);
                    }
                    else if (IdInArchive.Count > 1)
                    {
                        string NameCurrentImport = Path.GetFileName(InjectFile);
                        string[] FolderList = new string[listArch.Items.Count];

                        foreach (int ids in IdInArchive) FolderList[ids - 1] = listArch.Items[ids - 1].SubItems[7].Text;


                        using (Form2 form2 = new Form2())
                        {
                            if (FolderList.Length > 0) form2.ShowingArray(FolderList, NameCurrentImport);

                            if (form2.ShowDialog() == DialogResult.OK)
                                foreach (int FileToInject in form2.IDofarch)
                                {
                                    ListViewItem ImportedItem = new ListViewItem();
                                    ImportedItem.Text = Path.GetFileName(InjectFile);
                                    int length = (int)new FileInfo(InjectFile).Length;
                                    ImportedItem.SubItems.Add(listArch.Items[FileToInject].SubItems[1].Text);
                                    ImportedItem.SubItems.Add(listArch.Items[FileToInject].SubItems[2].Text);
                                    ImportedItem.SubItems.Add(listArch.Items[FileToInject].SubItems[6].Text);
                                    ImportedItem.SubItems[1].Tag = listArch.Items[FileToInject].SubItems[1].Tag;
                                    ImportedItem.SubItems[2].Tag = listArch.Items[FileToInject].SubItems[2].Tag;
                                    ImportedItem.SubItems[3].Tag = listArch.Items[FileToInject].SubItems[6].Tag;
                                    ImportedItem.SubItems.Add("Yes");
                                    ImportedItem.SubItems.Add(InjectFile);
                                    ImportedItem.SubItems.Add(Convert.ToString(length, radioButtonHex.Checked ? 16 : 10));
                                    ImportedItem.SubItems[6].Tag = length;
                                    ImportedItem.SubItems.Add("--");
                                    ImportedItem.SubItems[7].Tag = length;
                                    ImportedItem.SubItems.Add("No");
                                    listImport.Items.Add(ImportedItem);
                                }
                        }
                    }
                    else
                    {
                        ListViewItem ImportedItem = new ListViewItem();
                        ImportedItem.Text = Path.GetFileName(InjectFile);

                        ImportedItem.SubItems.Add("");
                        ImportedItem.SubItems.Add("");
                        ImportedItem.SubItems.Add("");
                        ImportedItem.SubItems.Add("No");

                        ImportedItem.SubItems.Add(InjectFile);
                        listImport.Items.Add(ImportedItem);
                    }
                }


                if (listImport.Items.Count > 0)
                {
                    EliminateDuplicateImports();

                    buttonClear.Enabled = true;
                    buttonInject.Enabled = true;
                }

                listImport.Enabled = true;
                listImport.Visible = true;
                Cursor = Cursors.Default;
            }
        }

        private void EliminateDuplicateImports()
        {
            for (int n = 0; n < listImport.Items.Count; n++)
                if (listImport.Items[n].SubItems[4].Text == "Yes")
                    for (int other = n + 1; other < listImport.Items.Count; other++)
                        if (listImport.Items[other].SubItems[4].Text == "Yes"
                         && Extensions.GetListItem(listImport, n, 3) == Extensions.GetListItem(listImport, other, 3)) // Same ID
                        {
                            if (checkBoxMuteWarnings.Checked
                             // Assume same path import is an intentional re-import
                             || listImport.Items[n].SubItems[5].Text == listImport.Items[other].SubItems[5].Text
                             || MessageBox.Show($"Import conflict at i {Extensions.GetListItem(listImport, n, 3)}." +
                                                $"\n\nDo you want to import \"{listImport.Items[other].SubItems[5].Text}\"" +
                                                $"\ninstead of \"{listImport.Items[n].SubItems[5].Text}\"?",
                                                "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                listImport.Items.Remove(listImport.Items[n]);
                                RemoveCompressedFile(n);
                                other = n;
                            }
                            else
                            {
                                listImport.Items.Remove(listImport.Items[other]);
                                other--;
                            }
                        }
        }

        private void RemoveCompressedFile(int index)
        {
            if (CompressedFiles != null && index <= CompressedFiles.Length - 1)
            {
                while (index < CompressedFiles.Length - 1)
                {
                    CompressedFiles[index] = CompressedFiles[index + 1];
                    index++;
                }
                CompressedFiles[index] = null;
            }
        }

        private int getVersionOfCompressedFiles()
        {
            int version = 0;

            if (CompressedFiles != null && CompressedFiles.Length > 0)
                for (int n = 0; n < CompressedFiles.Length; n++)
                    if (CompressedFiles[n] != null && CompressedFiles[n].Length > 0)
                    {
                        version = CompressedFiles[n][5];
                        break;
                    }

            return version;
        }

        private void CompressImports()
        {
            if (CompressedFiles == null)
                CompressedFiles = new byte[listImport.Items.Count][];
            else if (CompressedFiles.Length < listImport.Items.Count)
            {
                byte[][] tempArray = new byte[listImport.Items.Count][];
                CompressedFiles.CopyTo(tempArray, 0);
                CompressedFiles = tempArray;
            }

            if (CompressionOption > 0)
            {
                int version = radioButtonCompVer1.Checked ? 1 : 2;
                Compressor comp = new Compressor(version, 0);

                foreach (ListViewItem ImportedItem in listImport.Items)
                {
                    if (ImportedItem.SubItems[4].Text == "Yes")
                    {
                        var OrigParams = (CompressorParameters)listArchOriginalReference.Items[(int)ImportedItem.SubItems[3].Tag - 1].SubItems[5].Tag;

                        if (CompressionOption == 1 || CompressionOption == 2 && OrigParams.Version > 0)
                        {
                            if (radioButtonCompVerAuto.Checked)
                                comp = new Compressor(OrigParams);

                            int n = ImportedItem.Index;

                            if (CompressedFiles[n] == null || CompressedFiles[n].Length == 0
                             || CompressedFiles[n][5] != (CompressionOption == 1 ? version : OrigParams.Version))
                            {
                                FileStream import = File.Open(ImportedItem.SubItems[5].Text, FileMode.Open);
                                CompressedFiles[n] = new byte[import.Length];
                                import.Read(CompressedFiles[n], 0, (int)import.Length);
                                import.Flush();
                                import.Close();

                                CompressedFiles[n] = comp.Convert(CompressedFiles[n]);
                            }

                            Extensions.SetListItem(listImport, n, 7, CompressedFiles[n].Length, radioButtonHex.Checked);
                        }
                    }

                    if (progressBarInject.Visible) progressBarInject.PerformStep();
                }
            }

            else if (progressBarInject.Visible)
                progressBarInject.Increment(listImport.Items.Count);
        }

        private void UpdateFileStarts()
        {
            // Beginning with the second item, adjust its file offset based on the previous item.
            for (int i = 1; i < listArch.Items.Count; i++)
            {
                int FileStart = Extensions.GetListItem(listArch, i - 1, 2)
                              + Extensions.GetListItem(listArch, i - 1, 4);

                int Padding = 0;
                if (FileStart % 2048 != 0)
                    Padding = 2048 - FileStart % 2048;

                if (WideFile || Extensions.GetListItem(listArch, i, 4) > Padding)
                    FileStart += Padding;

                Extensions.SetListItem(listArch, i, 2, FileStart, radioButtonHex.Checked);
            }
        }

        private void buttonInject_Click(object sender, EventArgs e)
        {
            Enabled = false;

            if (CompressionOption == 0 || checkBoxMuteWarnings.Checked
             || MessageBox.Show("Files must be compressed before injection."
             + "\nIf necessary, the process may take some time. Continue?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                progressBarInject.Maximum = listImport.Items.Count + 2;
                progressBarInject.Value = 0;
                progressBarInject.Visible = true;

                CompressImports();

                bool listArchChanged = false;
                foreach (ListViewItem ImportedItem in listImport.Items)
                {
                    if (ImportedItem.SubItems[4].Text == "Yes")
                    {
                        byte Version = 0;
                        byte Endianness = 0;
                        if (CompressionOption != 0
                         && CompressedFiles[ImportedItem.Index] != null
                         && CompressedFiles[ImportedItem.Index].Length > 0)
                        {
                            Endianness = CompressedFiles[ImportedItem.Index][4];
                            Version = CompressedFiles[ImportedItem.Index][5];
                        }

                        if (ImportedItem.SubItems[8].Text == "No" || (int)ImportedItem.SubItems[8].Tag != Version)
                        {
                            int i = (int)ImportedItem.SubItems[3].Tag - 1;

                            listArch.Items[i].SubItems[5].Text = Version == 0 ? "No" : $"Yes  (v{Version})";
                            ((CompressorParameters)listArch.Items[i].SubItems[5].Tag).Version = Version;
                            ((CompressorParameters)listArch.Items[i].SubItems[5].Tag).Endianness = Endianness;

                            Extensions.SetListItem(listArch, i, 3, (int)ImportedItem.SubItems[6].Tag, radioButtonHex.Checked);
                            Extensions.SetListItem(listArch, i, 4, (int)ImportedItem.SubItems[Version == 0 ? 6 : 7].Tag, radioButtonHex.Checked);

                            ImportedItem.SubItems[8].Text = "Yes" + (Version == 0 ? "" : $"  (v{Version})");
                            ImportedItem.SubItems[8].Tag = Version;

                            listArchChanged = true;
                        }
                    }
                }

                progressBarInject.PerformStep();

                if (listArchChanged)
                {
                    UpdateFileStarts();
                    buttonRevert.Enabled = true;
                }

                progressBarInject.PerformStep();
                progressBarInject.Visible = false;

                listArch.Update();
                listImport.Update();

                Cursor = Cursors.Default;
            }

            Enabled = true;
        }

        private void comboBoxCompression_Commit(object sender, EventArgs e)
        {
            if (comboBoxCompression.SelectedIndex != CompressionOption)
            {
                groupBoxCompressionVersion.Enabled = comboBoxCompression.SelectedIndex != 0;

                if (comboBoxCompression.SelectedIndex == 2)
                    radioButtonCompVerAuto.Enabled = true;

                else
                {
                    if (CompressionOption == 2 && radioButtonCompVerAuto.Checked)
                        radioButtonCompVer1.Checked = true;

                    radioButtonCompVerAuto.Enabled = false;
                }

                CompressionOption = comboBoxCompression.SelectedIndex;
            }
        }

        private void revertListArchItemAt(int i)
        {
            ListViewItem orig = listArchOriginalReference.Items[i];

            Extensions.SetListItem(listArch, i, 3, (int)orig.SubItems[3].Tag, radioButtonHex.Checked);
            Extensions.SetListItem(listArch, i, 4, (int)orig.SubItems[4].Tag, radioButtonHex.Checked);
            listArch.Items[i].SubItems[5].Text = orig.SubItems[5].Text;
            listArch.Items[i].SubItems[5].Tag = orig.SubItems[5].Tag;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Enabled = false;

            if (checkBoxMuteWarnings.Checked
             || MessageBox.Show("Clear the list of imports?\nThis will undo all injections if any have occurred.",
                                "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                listArch.Enabled = false;
                listArch.Visible = false;
                listImport.Enabled = false;
                listImport.Visible = false;

                bool listArchChanged = false;
                foreach (ListViewItem item in listImport.Items)
                    if (item.SubItems[4].Text == "Yes" && item.SubItems[8].Text != "No")
                    {
                        revertListArchItemAt((int)item.SubItems[3].Tag - 1);
                        listArchChanged = true;
                    }

                if (listArchChanged)
                {
                    UpdateFileStarts();

                    if (WideFile == IsWide(listArch))
                        buttonRevert.Enabled = false;
                }

                listImport.Items.Clear();

                CompressedFiles = null;

                buttonClear.Enabled = false;
                buttonInject.Enabled = false;

                listArch.Update();
                listImport.Update();

                listArch.Enabled = true;
                listArch.Visible = true;
                listImport.Enabled = true;
                listImport.Visible = true;

                Cursor = Cursors.Default;
            }

            Enabled = true;
        }

        private void buttonRevert_Click(object sender, EventArgs e)
        {
            Enabled = false;

            if (checkBoxMuteWarnings.Checked
             || MessageBox.Show("Revert all unsaved changes to the PAR?",
                                "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                listArch.Enabled = false;
                listArch.Visible = false;
                listImport.Enabled = false;
                listImport.Visible = false;

                listArch.Items.Clear();

                foreach (ListViewItem item in listArchOriginalReference.Items)
                    listArch.Items.Add((ListViewItem)item.Clone());

                WideFile = IsWide(listArchOriginalReference);

                if (listImport.Items.Count > 0)
                {
                    foreach (ListViewItem item in listImport.Items)
                        if (item.SubItems[4].Text == "Yes")
                        {
                            item.SubItems[8].Text = "No";
                            item.SubItems[8].Tag = null;
                        }
                }

                buttonRevert.Enabled = false;

                listArch.Update();
                listImport.Update();

                listArch.Enabled = true;
                listArch.Visible = true;
                listImport.Enabled = true;
                listImport.Visible = true;

                Cursor = Cursors.Default;
            }

            Enabled = true;
        }

        private void radioButtonHex_CheckedChanged(object sender, EventArgs e)
        {
            if (listArch.Items.Count > 0)
            {
                Cursor = Cursors.WaitCursor;

                listArch.Enabled = false;
                foreach (ListViewItem item in listArch.Items)
                {
                    item.SubItems[1].Text = Convert.ToString((int)item.SubItems[1].Tag, radioButtonHex.Checked ? 16 : 10);
                    item.SubItems[2].Text = Convert.ToString((int)item.SubItems[2].Tag, radioButtonHex.Checked ? 16 : 10);
                    item.SubItems[3].Text = Convert.ToString((int)item.SubItems[3].Tag, radioButtonHex.Checked ? 16 : 10);
                    item.SubItems[4].Text = Convert.ToString((int)item.SubItems[4].Tag, radioButtonHex.Checked ? 16 : 10);
                }
                listArch.Enabled = true;
                listArch.Update();

                if (listImport.Items.Count > 0)
                {
                    listImport.Enabled = false;
                    foreach (ListViewItem item in listImport.Items)
                    {
                        if (item.SubItems[4].Text == "Yes")
                        {
                            item.SubItems[1].Text = Convert.ToString((int)item.SubItems[1].Tag, radioButtonHex.Checked ? 16 : 10);
                            item.SubItems[2].Text = Convert.ToString((int)item.SubItems[2].Tag, radioButtonHex.Checked ? 16 : 10);
                            item.SubItems[6].Text = Convert.ToString((int)item.SubItems[6].Tag, radioButtonHex.Checked ? 16 : 10);
                            if (item.SubItems[7].Text != "--")
                                item.SubItems[7].Text = Convert.ToString((int)item.SubItems[7].Tag, radioButtonHex.Checked ? 16 : 10);
                        }
                    }
                    listImport.Enabled = true;
                    listImport.Update();
                }

                Cursor = Cursors.Default;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            listArch.Width = (Width - 38) / 2 + 1;

            listImport.Width = Width - listArch.Width - 54;
            listImport.Location = new Point(15 + listArch.Width + 8, listImport.Location.Y);

            checkBoxMuteWarnings.Location = new Point(listImport.Location.X, checkBoxMuteWarnings.Location.Y);
            buttonImportFiles.Location = new Point(listImport.Location.X, buttonImportFiles.Location.Y);
            buttonClear.Location = new Point(listImport.Location.X + buttonImportFiles.Width + 7, buttonClear.Location.Y);
        }

        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PARC Archive Importer"
              + "\n\"Knock It Outta The PARC\" Edition!"
              + "\n\nA tool by SlowpokeVG."
              + "\nContributions by kurdtkobain, aposteriorist."
              + "\n\nThanks to Kaplas80 for some code from ParManager.",
              "About", MessageBoxButtons.OK);
        }
    }
}