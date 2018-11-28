using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PARC_Archive_Importer
{
    public partial class Form1 : Form
    {

        public static void AppendAllBytes(string path, byte[] bytes)
        {
            //argument-checking here.

            using (var stream = new FileStream(path, FileMode.Append))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
        public static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
        int IntPow(int x, int pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }
        static int ParseSingleByte(byte[] input, int adr)
        {
            string first = input[adr].ToString("X");
            int firstint = Convert.ToInt32(input[adr]);
            string resultparse = first;
            return int.Parse(resultparse, System.Globalization.NumberStyles.HexNumber);
        }
        static int ParseOffset(byte[] input, int adr)
        {
            string first = input[adr].ToString("X");
            string second = input[adr + 1].ToString("X");
            string third = input[adr + 2].ToString("X");
            string forth = input[adr + 3].ToString("X");

            int firstint = Convert.ToInt32(input[adr]);
            int secondint = Convert.ToInt32(input[adr + 1]);
            int thirdint = Convert.ToInt32(input[adr + 2]);
            int forthint = Convert.ToInt32(input[adr + 3]);

            string resultparse = first;

            if (secondint < 16) { resultparse += 0; resultparse += second; } else { resultparse += second; }
            if (thirdint < 16) { resultparse += 0; resultparse += third; } else { resultparse += third; }
            if (forthint < 16) { resultparse += 0; resultparse += forth; } else { resultparse += forth; }

            return int.Parse(resultparse, System.Globalization.NumberStyles.HexNumber);
        }
        static byte[] ReturnToSender(string Hex)
        {
            //string Hex = dec.ToString("X");
            string Hex4 = null;
            if (Hex.Length % 2 == 1)
            {
                Hex4 += "0";
                Hex4 += Hex;
                Hex = null;
                Hex = Hex4;
            }

            byte[] HexArr = new byte[4];
            int m = 0;
            if (Hex.Length < 8)
            {
                m = (8 - Hex.Length) / 2;
            }

            for (int i = 0; i < Hex.Length; i = i + 2)
            {
                HexArr[m] = Convert.ToByte(Hex.Substring(i, 2), 16);
                m++;
            }
            return HexArr;
        }
        static byte[] TempArrayHex(int bytecount, byte[] importarray, int offsetinarray)
        {
            byte[] tempbytearr = new byte[bytecount];
            for (int i = 0; i <= bytecount - 1; i++)
            {
                tempbytearr[i] = importarray[offsetinarray];
                offsetinarray++;
            }
            return tempbytearr;
        }
        void ChngInArray(int howmany, int address, byte[] from, byte[] where)
        {
            for (int i = 0; i <= howmany - 1; i++)
            {
                where[address + i] = from[i];
            }
        }

        string OpenedArchName;
        string OpenedArchPath;
        byte[] InterOpenedArchive;
        int Filecount;
        int Foldercount;
        int Filestartoffset;
        int Folderstartoffset;
        //int DifferenceAfterInj;
        public string[] folderlist;
        public string namecurrentimport;
        public Form1()
        {
            InitializeComponent();
        }

        void ParseSourceArray(byte[] OrigArchive)
        {
            listArch.Items.Clear();
            listArch.Update();
            listArch.Refresh();

            buttonWiden.Enabled = false;
            buttonWiden.Text = "Widen";
            Filecount = ParseOffset(OrigArchive, 24);
            Foldercount = ParseOffset(OrigArchive, 16);
            Filestartoffset = ParseOffset(OrigArchive, 28);
            Folderstartoffset = ParseOffset(OrigArchive, 20);
            //DifferenceAfterInj = 0;
            List<string> filelist = new List<string>();
            filelist.Clear();
            string[] foldernames = new string[Filecount];
            int foldercounter = 0;
            for (int folder = 0; folder < Foldercount; folder++)
            {
                int filesinfolder = ParseOffset(OrigArchive, Folderstartoffset + 8 + folder * 32);
                for (int m = 0; m < filesinfolder; m++)
                    foldernames[foldercounter+m] = Encoding.ASCII.GetString(OrigArchive, folder * 64 + 32, 64).TrimEnd(new char[] { (char)0 }); ;
                foldercounter += filesinfolder;
            }

            for (int ID = 1; ID <= Filecount; ID++)
            {
                    string nameoffile = Encoding.ASCII.GetString(OrigArchive, 32 + Foldercount * 64 + (ID - 1) * 64, 64).TrimEnd(new char[] { (char)0 }); ;
                    filelist.Add(nameoffile);
                    int filedescoff = Filestartoffset + (ID - 1) * 32;
                    int filestart = ParseOffset(OrigArchive, 12 + filedescoff);
                    int filesizeoff = ParseOffset(OrigArchive, 4 + filedescoff);
                    int filesizecompoff = ParseOffset(OrigArchive, 8 + filedescoff);
                    string iscompressed = "No";
                    if (ParseSingleByte(OrigArchive, filedescoff) != 0)
                    {
                        iscompressed = "Yes";
                    }
                    ListViewItem item = new ListViewItem();
                    item.Text = nameoffile;
                    item.SubItems.Add(filedescoff.ToString());
                    item.SubItems.Add(filestart.ToString());
                    item.SubItems.Add(filesizeoff.ToString());
                    item.SubItems.Add(filesizecompoff.ToString());
                    item.SubItems.Add(iscompressed);
                    item.SubItems.Add(ID.ToString());
                    item.SubItems.Add(foldernames[ID - 1]);
                    listArch.Items.Add(item);

            }
            

        }

        bool iswide()
        {
            bool isitwide = true;
            for (int i = 0; i < listArch.Items.Count - 2; i++)
            {
                if ((int.Parse(listArch.Items[i + 1].SubItems[2].Text) - int.Parse(listArch.Items[i].SubItems[2].Text)) % 2048 != 0)
                {
                    isitwide = false;
                    break;
                }
            }
            return isitwide;
        }

        private void buttonOpenArch_Click(object sender, EventArgs e)
        {

                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Open Original File";
                theDialog.Filter = "PARC archives (*.par)|*.par|All files (*.*)|*.*";
                if (theDialog.ShowDialog() == DialogResult.OK)
            {
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
                    listViewdebug.Items.Clear();
                    listViewdebug.Update();
                    listViewdebug.Refresh();

                    foreach (ListViewItem item in listArch.Items)
                    {
                        listViewdebug.Items.Add((ListViewItem)item.Clone());
                    }


                    if (!iswide())
                    {
                        buttonWiden.Enabled = true;
                        buttonWiden.Text = "Widen";
                    }
                    else
                    {
                        buttonInportFiles.Enabled = true;
                        buttonWiden.Text = "Wide";
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

           
        }
        int returnrightstepenparallel(int FileSize)
        {
            int rstepen = 1;

            Parallel.For(0, (InterOpenedArchive.Length / 2048) + 1, (stepen, pls) => 
            {
                int poweredstepen = 2048 * stepen;
                if (FileSize > poweredstepen && FileSize <= 2048 + poweredstepen)
                {
                    rstepen = stepen + 1;
                    pls.Stop();
                }

            });
            return rstepen;
        }
        private void buttonWiden_Click(object sender, EventArgs e)
        {
            Enabled = false;
            listArch.Enabled = false;
            listArch.Visible = false;
            int totaldifference = 0;
            for (int i = 0; i < listArch.Items.Count - 2; i++)
            {
                int originalfilesize = int.Parse(listArch.Items[i + 1].SubItems[2].Text) - int.Parse(listArch.Items[i].SubItems[2].Text);
                if (originalfilesize % 2048 != 0)
                {
                    int Filesize = int.Parse(listArch.Items[i].SubItems[3].Text);

                    int rightstepen = returnrightstepenparallel(Filesize);

                    int differencenew = 2048 * rightstepen - originalfilesize;
                    totaldifference += differencenew;


                    //change startpoints for files after this one
                    for (int m = i + 1; m < listArch.Items.Count; m++)
                    {
                        listArch.Items[m].SubItems[2].Text = (int.Parse(listArch.Items[m].SubItems[2].Text) + differencenew).ToString();
                        ChngInArray(4, 12 + int.Parse(listArch.Items[m].SubItems[1].Text), ReturnToSender((int.Parse(listArch.Items[m].SubItems[2].Text)).ToString("X")), InterOpenedArchive);
                    }
                }
            }
            //change overall size
            ChngInArray(4, 12, ReturnToSender((ParseOffset(InterOpenedArchive, 12) + totaldifference).ToString("X")), InterOpenedArchive);
            listArch.Enabled = true;
            Enabled = true;
            listArch.Visible = true;
            if (!iswide())
            {
                buttonWiden.Enabled = true;
                buttonWiden.Text = "Widen";
            }
            else
            {
                buttonWiden.Enabled = false;
                buttonInportFiles.Enabled = true;
                buttonWiden.Text = "Wide";
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
                FileStream fcreate = File.Open(DialogSave.FileName, FileMode.Create, FileAccess.Write);
                fcreate.Write(InterOpenedArchive, 0, int.Parse(listViewdebug.Items[0].SubItems[2].Text));

                for (int m = 0; m < listArch.Items.Count; m++)
                {

                    bool isimported = false;
                    for (int n = 0; n < listImport.Items.Count; n++)
                    {
                        if (listImport.Items[n].SubItems[4].Text == "Yes")
                            if (int.Parse(listImport.Items[n].SubItems[3].Text)-1 == m)
                            {
                                FileStream import = File.Open(listImport.Items[n].SubItems[5].Text, FileMode.Open);
                                import.CopyTo(fcreate);
                                import.Flush();
                                import.Close();
                                isimported = true;
                                break;
                            }
                        
                    }
                    if (!isimported) fcreate.Write(InterOpenedArchive, int.Parse(listViewdebug.Items[m].SubItems[2].Text), int.Parse(listViewdebug.Items[m].SubItems[4].Text));

                    if (m < listViewdebug.Items.Count - 1)
                    {
                        if (int.Parse(listArch.Items[m + 1].SubItems[2].Text) > (int.Parse(listArch.Items[m].SubItems[4].Text) + int.Parse(listArch.Items[m].SubItems[2].Text)))
                        {
                            byte[] zeroes = new byte[int.Parse(listArch.Items[m + 1].SubItems[2].Text) - (int.Parse(listArch.Items[m].SubItems[4].Text) + int.Parse(listArch.Items[m].SubItems[2].Text))];
                            fcreate.Write(zeroes, 0, zeroes.Length);
                        }

                    }
                    else if (m == listViewdebug.Items.Count - 1)
                    {
                        byte[] zeroes = new byte[returnrightstepenparallel(int.Parse(listArch.Items[m].SubItems[3].Text)) * 2048 - int.Parse(listArch.Items[m].SubItems[3].Text)];
                        fcreate.Write(zeroes, 0, zeroes.Length);
                    }


                }

                fcreate.Flush();
                fcreate.Close();

            }
        }

        private void buttonInportFiles_Click(object sender, EventArgs e)
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
                    List<int> IDinarchive = new List<int>(); 
                    foreach (ListViewItem item in listArch.Items)
                    {
                        if (string.Equals(item.Text, Path.GetFileName(InjectFile), StringComparison.CurrentCultureIgnoreCase))
                        {
                            IDinarchive.Add(int.Parse(item.SubItems[6].Text));
                        }

                    }

                    if (IDinarchive.Count == 1)
                    {
                        ListViewItem injitem = new ListViewItem();
                        injitem.Text = Path.GetFileName(InjectFile);

                        injitem.SubItems.Add(listArch.Items[IDinarchive[0]-1].SubItems[1].Text);
                        injitem.SubItems.Add(listArch.Items[IDinarchive[0]-1].SubItems[2].Text);
                        injitem.SubItems.Add(listArch.Items[IDinarchive[0]-1].SubItems[6].Text);
                        injitem.SubItems.Add("Yes");

                        injitem.SubItems.Add(InjectFile);
                        long length = new FileInfo(InjectFile).Length;
                        injitem.SubItems.Add(length.ToString());
                        listImport.Items.Add(injitem);
                    }
                    else if (IDinarchive.Count > 1)
                    {
                        namecurrentimport = Path.GetFileName(InjectFile);
                        folderlist = new string[listArch.Items.Count];

                        foreach (int ids in IDinarchive)
                        {
                            folderlist[ids-1] = listArch.Items[ids-1].SubItems[7].Text;
                        }


                        using (Form2 form2 = new Form2())
                        {
                            if (folderlist.Length > 0)
                            {
                                form2.ShowingArray(folderlist, namecurrentimport);
                            }
                            if (form2.ShowDialog() == DialogResult.OK)
                            {
                                foreach (int Injfile in form2.IDofarch)
                                { 
                                    ListViewItem injitem = new ListViewItem();
                                    injitem.Text = Path.GetFileName(InjectFile);
                                    long length = new FileInfo(InjectFile).Length;
                                    injitem.SubItems.Add(listArch.Items[Injfile].SubItems[1].Text);
                                    injitem.SubItems.Add(listArch.Items[Injfile].SubItems[2].Text);
                                    injitem.SubItems.Add(listArch.Items[Injfile].SubItems[6].Text);
                                    injitem.SubItems.Add("Yes");
                                    injitem.SubItems.Add(InjectFile);
                                    // FileInfo fInfo = new FileInfo(InjectFile);

                                    injitem.SubItems.Add(length.ToString());

                                    listImport.Items.Add(injitem);
                                }
                            }
                        }
                    }
                    else
                    {
                        ListViewItem injitem = new ListViewItem();
                        injitem.Text = Path.GetFileName(InjectFile);

                        injitem.SubItems.Add("");
                        injitem.SubItems.Add("");
                        injitem.SubItems.Add("");
                        injitem.SubItems.Add("No");

                        injitem.SubItems.Add(InjectFile);
                        listImport.Items.Add(injitem);
                    }



                }
            }
        }

        byte[] ChangedHeaderImport(int differencewd, int filenumber, byte[] headerwd, long injectedfilesize)
        {
            //change overall size
            ChngInArray(4, 12, ReturnToSender((ParseOffset(headerwd, 12) + differencewd).ToString("X")), headerwd);

            //change startpoints for files after this one
            for (int i = filenumber + 1; i < listArch.Items.Count; i++)
            {
                ChngInArray(4, 12 + int.Parse(listArch.Items[i].SubItems[1].Text), ReturnToSender((ParseOffset(headerwd, 12 + int.Parse(listArch.Items[i].SubItems[1].Text)) + differencewd).ToString("X")), headerwd);
            }
            //change sizes for imported file
            ChngInArray(4, 4 + int.Parse(listArch.Items[filenumber].SubItems[1].Text), ReturnToSender(injectedfilesize.ToString("X")), headerwd);
            ChngInArray(4, 8 + int.Parse(listArch.Items[filenumber].SubItems[1].Text), ReturnToSender(injectedfilesize.ToString("X")), headerwd);
            headerwd[int.Parse(listArch.Items[filenumber].SubItems[1].Text)] = 0x00;

            return headerwd;
        }

        private void buttonInject_Click(object sender, EventArgs e)
        {

            Enabled = false;
            listArch.Visible = false;
            foreach (ListViewItem injectedfile in listImport.Items)
            {
                if (injectedfile.SubItems[4].Text == "Yes")
                {

                    long filesize = long.Parse(injectedfile.SubItems[6].Text);

                    int rstepen = 1;

                    Parallel.For(0, (InterOpenedArchive.Length / 2048) + 1, (stepen, pls) =>
                    {
                        int poweredstepen = 2048 * stepen;
                        if (filesize > poweredstepen && filesize <= 2048 + poweredstepen)
                        {
                            rstepen = stepen + 1;
                            pls.Stop();
                        }
                    });

                    int originalfilesize;
                    int i = int.Parse(injectedfile.SubItems[3].Text) - 1;
                    if (listArch.Items.Count == int.Parse(injectedfile.SubItems[3].Text)) { originalfilesize = int.Parse(listArch.Items[i].SubItems[3].Text); }
                    else { originalfilesize = int.Parse(listArch.Items[i + 1].SubItems[2].Text) - int.Parse(listArch.Items[i].SubItems[2].Text); }

                    
                    int differencenew = 2048 * rstepen - originalfilesize;

                    ChangedHeaderImport(differencenew, i, InterOpenedArchive, filesize);

                    ParseSourceArray(InterOpenedArchive);

                }



            }
            Enabled = true;
            listArch.Visible = true;

        }
    }
}
