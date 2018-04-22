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
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
