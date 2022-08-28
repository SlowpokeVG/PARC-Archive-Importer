using System;
using System.Collections.Generic;
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

        public string[] FolderList;
        private int FolderStartOffset;
        private byte[] InterOpenedArchive;
        public string NameCurrentImport;

        private string OpenedArchName;
        private string OpenedArchPath;

        public Form1()
        {
            InitializeComponent();
        }

        public static void WriteInt32(int value, byte[] target, int address)
        {
            byte[] result = new byte[4];
            result[0] = (byte) ((value & 0xFF000000) >> 24);
            result[1] = (byte) ((value & 0x00FF0000) >> 16);
            result[2] = (byte) ((value & 0x0000FF00) >> 8);
            result[3] = (byte) (value & 0x000000FF);

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
                        .TrimEnd((char) 0);

                FolderCounter += FilesInFolder;
            }

            for (int ID = 1; ID <= FileCount; ID++)
            {
                string NameOfFile = Encoding.ASCII.GetString(OrigArchive, 32 + FolderCount * 64 + (ID - 1) * 64, 64)
                    .TrimEnd((char) 0);
                ;
                FileList.Add(NameOfFile);
                int FileDescriptionOffset = FileStartOffset + (ID - 1) * 32;
                int FileStart = Extensions.ReadInt32LE(OrigArchive, 12 + FileDescriptionOffset);
                int FileSizeOffset = Extensions.ReadInt32LE(OrigArchive, 4 + FileDescriptionOffset);
                int FileSizeCompressedOffset = Extensions.ReadInt32LE(OrigArchive, 8 + FileDescriptionOffset);
                string IsCompressed = "No";
                if (OrigArchive[FileDescriptionOffset] != 0x00) IsCompressed = "Yes";

                ListViewItem item = new ListViewItem();
                item.Text = NameOfFile;
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add(IsCompressed);
                item.SubItems.Add("");
                item.SubItems.Add(FolderNames[ID - 1]);
                listArch.Items.Add(item);

                Extensions.SetListItem(listArch, ID - 1, 1, FileDescriptionOffset);
                Extensions.SetListItem(listArch, ID - 1, 2, FileStart);
                Extensions.SetListItem(listArch, ID - 1, 3, FileSizeOffset);
                Extensions.SetListItem(listArch, ID - 1, 4, FileSizeCompressedOffset);
                Extensions.SetListItem(listArch, ID - 1, 6, ID);
            }
        }

        private bool IsWide()
        {
            bool IsFileWide = true;
            for (int i = 0; i < listArch.Items.Count - 1; i++)
                if ((Extensions.GetListItem(listArch, i + 1, 2) -
                     Extensions.GetListItem(listArch, i, 2)) % 2048 != 0)
                {
                    IsFileWide = false;
                    break;
                }

            return IsFileWide;
        }

        private void buttonOpenArch_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Original File";
            theDialog.Filter = "PARC archives (*.par)|*.par|All files (*.*)|*.*";

            if (theDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    OpenedArchName = Path.GetFileName(theDialog.FileName);
                    Text = OpenedArchName + " - PARC Archive Importer";
                    OpenedArchPath = Path.GetDirectoryName(theDialog.FileName);
                    InterOpenedArchive = File.ReadAllBytes(theDialog.FileName);

                    listArch.Enabled = false;
                    listArch.Visible = false;

                    ParseSourceArray(InterOpenedArchive);

                    listArch.Enabled = true;
                    listArch.Visible = true;
                    listViewOriginalReference.Items.Clear();
                    listViewOriginalReference.Update();
                    listViewOriginalReference.Refresh();

                    foreach (ListViewItem item in listArch.Items)
                        listViewOriginalReference.Items.Add((ListViewItem) item.Clone());


                    if (!IsWide())
                    {
                        buttonWiden.Enabled = true;
                        buttonWiden.Text = "Widen";
                    }
                    else
                    {
                        buttonImportFiles.Enabled = true;
                        buttonWiden.Text = "Wide";
                    }
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

            int TotalDifference = 0;

            for (int i = 0; i < listArch.Items.Count - 1; i++)
            {
                int OriginalFileSize =
                    Extensions.GetListItem(listArch, i + 1, 2) - Extensions.GetListItem(listArch, i, 2);


                if (OriginalFileSize % 2048 != 0)
                {
                    int Filesize = Extensions.GetListItem(listArch, i, 4);
                    int Multiplier = Filesize / 2048 + 1;
                    int NewDifference = 2048 * Multiplier - OriginalFileSize;

                    TotalDifference += NewDifference;


                    //change startpoints for files after this one
                    for (int m = i + 1; m < listArch.Items.Count; m++)
                    {
                        listArch.Items[m].SubItems[2].Tag =
                            Extensions.GetListItem(listArch, m, 2) + NewDifference;
                        WriteInt32(Extensions.GetListItem(listArch, m, 2), InterOpenedArchive,
                            12 + Extensions.GetListItem(listArch, m, 1));
                    }
                }
            }

            //change overall size
            if (Extensions.ReadInt32LE(InterOpenedArchive, 12) != 0)
                WriteInt32(Extensions.ReadInt32LE(InterOpenedArchive, 12) + TotalDifference, InterOpenedArchive, 12);

            listArch.Enabled = true;
            Enabled = true;
            listArch.Visible = true;

            if (!IsWide())
            {
                buttonWiden.Enabled = true;
                buttonWiden.Text = "Widen";
            }
            else
            {
                buttonWiden.Enabled = false;
                buttonImportFiles.Enabled = true;
                buttonWiden.Text = "Wide";
            }

            ParseSourceArray(InterOpenedArchive);
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
                FileStream fcreate = File.Open(DialogSave.FileName, FileMode.Create, FileAccess.Write);
                fcreate.Write(InterOpenedArchive, 0, Extensions.GetListItem(listViewOriginalReference, 0, 2));

                for (int m = 0; m < listArch.Items.Count; m++)
                {
                    bool IsImported = false;

                    for (int n = 0; n < listImport.Items.Count; n++)
                        if (listImport.Items[n].SubItems[4].Text == "Yes")
                            if (Extensions.GetListItem(listImport, n, 3) - 1 == m)
                            {
                                FileStream import = File.Open(listImport.Items[n].SubItems[5].Text, FileMode.Open);
                                import.CopyTo(fcreate);
                                import.Flush();
                                import.Close();
                                IsImported = true;
                                break;
                            }

                    if (!IsImported)
                        fcreate.Write(InterOpenedArchive, Extensions.GetListItem(listViewOriginalReference, m, 2),
                            Extensions.GetListItem(listViewOriginalReference, m, 4));

                    if (m < listViewOriginalReference.Items.Count - 1)
                    {
                        if (Extensions.GetListItem(listArch, m + 1, 2) > Extensions.GetListItem(listArch, m, 4) +
                            Extensions.GetListItem(listArch, m, 2))
                        {
                            byte[] Padding = new byte[Extensions.GetListItem(listArch, m + 1, 2) -
                                                      (Extensions.GetListItem(listArch, m, 4) +
                                                       Extensions.GetListItem(listArch, m, 2))];
                            fcreate.Write(Padding, 0, Padding.Length);
                        }
                    }
                    else
                    {
                        byte[] Padding = new byte[(Extensions.GetListItem(listArch, m, 4) / 2048 + 1) * 2048 -
                                                  Extensions.GetListItem(listArch, m, 4)];
                        fcreate.Write(Padding, 0, Padding.Length);
                    }
                }

                fcreate.Flush();
                fcreate.Close();
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
                listImport.Items.Clear();
                listImport.Update();
                listImport.Refresh();
                foreach (string InjectFile in theDialog.FileNames)
                {
                    List<int> IdInArchive = new List<int>();
                    foreach (ListViewItem item in listArch.Items)
                        if (string.Equals(item.Text, Path.GetFileName(InjectFile),
                                StringComparison.CurrentCultureIgnoreCase))
                            IdInArchive.Add((int) item.SubItems[6].Tag);

                    if (IdInArchive.Count == 1)
                    {
                        ListViewItem ImportedItem = new ListViewItem();
                        ImportedItem.Text = Path.GetFileName(InjectFile);

                        ImportedItem.SubItems.Add(listArch.Items[IdInArchive[0] - 1].SubItems[1].Text);
                        ImportedItem.SubItems.Add(listArch.Items[IdInArchive[0] - 1].SubItems[2].Text);
                        ImportedItem.SubItems.Add(listArch.Items[IdInArchive[0] - 1].SubItems[6].Text);
                        ImportedItem.SubItems.Add("Yes");

                        ImportedItem.SubItems.Add(InjectFile);
                        long length = new FileInfo(InjectFile).Length;
                        ImportedItem.SubItems.Add(length.ToString());
                        listImport.Items.Add(ImportedItem);
                    }
                    else if (IdInArchive.Count > 1)
                    {
                        NameCurrentImport = Path.GetFileName(InjectFile);
                        FolderList = new string[listArch.Items.Count];

                        foreach (int ids in IdInArchive) FolderList[ids - 1] = listArch.Items[ids - 1].SubItems[7].Text;


                        using (Form2 form2 = new Form2())
                        {
                            if (FolderList.Length > 0) form2.ShowingArray(FolderList, NameCurrentImport);

                            if (form2.ShowDialog() == DialogResult.OK)
                                foreach (int FileToInject in form2.IDofarch)
                                {
                                    ListViewItem ImportedItem = new ListViewItem();
                                    ImportedItem.Text = Path.GetFileName(InjectFile);
                                    long length = new FileInfo(InjectFile).Length;
                                    ImportedItem.SubItems.Add(listArch.Items[FileToInject].SubItems[1].Text);
                                    ImportedItem.SubItems.Add(listArch.Items[FileToInject].SubItems[2].Text);
                                    ImportedItem.SubItems.Add(listArch.Items[FileToInject].SubItems[6].Text);
                                    ImportedItem.SubItems.Add("Yes");
                                    ImportedItem.SubItems.Add(InjectFile);
                                    // FileInfo fInfo = new FileInfo(InjectFile);

                                    ImportedItem.SubItems.Add(length.ToString());

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

                foreach (ListViewItem ImportedItem in listImport.Items)
                    if (ImportedItem.SubItems[4].Text == "Yes")
                    {
                        ImportedItem.SubItems[1].Tag = int.Parse(ImportedItem.SubItems[1].Text);
                        ImportedItem.SubItems[2].Tag = int.Parse(ImportedItem.SubItems[2].Text);
                        ImportedItem.SubItems[3].Tag = int.Parse(ImportedItem.SubItems[3].Text);
                        ImportedItem.SubItems[6].Tag = int.Parse(ImportedItem.SubItems[6].Text);
                    }
            }
        }

        private byte[] ChangedHeaderImport(int differencewd, int filenumber, byte[] headerwd, int injectedfilesize)
        {
            //change overall size
            if (Extensions.ReadInt32LE(headerwd, 12) != 0)
                WriteInt32(Extensions.ReadInt32LE(headerwd, 12) + differencewd, headerwd, 12);

            //change startpoints for files after this one
            for (int i = filenumber + 1; i < listArch.Items.Count; i++)
                WriteInt32(Extensions.ReadInt32LE(headerwd, 12 + Extensions.GetListItem(listArch, i, 1)) +
                           differencewd, headerwd, 12 + Extensions.GetListItem(listArch, i, 1));

            //change sizes for imported file
            WriteInt32(injectedfilesize, headerwd, 4 + Extensions.GetListItem(listArch, filenumber, 1));
            WriteInt32(injectedfilesize, headerwd, 8 + Extensions.GetListItem(listArch, filenumber, 1));

            headerwd[Extensions.GetListItem(listArch, filenumber, 1)] = 0x00;

            return headerwd;
        }

        private void buttonInject_Click(object sender, EventArgs e)
        {
            Enabled = false;
            listArch.Visible = false;

            foreach (ListViewItem ImportedItem in listImport.Items)
                if (ImportedItem.SubItems[4].Text == "Yes")
                {
                    int InjectedFileSize = (int) ImportedItem.SubItems[6].Tag;

                    int Multiplier = InjectedFileSize / 2048 + 1;
                    int OriginalFileSize;

                    int i = (int) ImportedItem.SubItems[3].Tag - 1;
                    if (listArch.Items.Count == (int) ImportedItem.SubItems[3].Tag)
                        OriginalFileSize = Extensions.GetListItem(listArch, i, 4);
                    else
                        OriginalFileSize = Extensions.GetListItem(listArch, i + 1, 2) -
                                           Extensions.GetListItem(listArch, i, 2);


                    int DifferenceInSize = 2048 * Multiplier - OriginalFileSize;

                    ChangedHeaderImport(DifferenceInSize, i, InterOpenedArchive, InjectedFileSize);

                    ParseSourceArray(InterOpenedArchive);
                }

            Enabled = true;
            listArch.Visible = true;
        }
    }
}