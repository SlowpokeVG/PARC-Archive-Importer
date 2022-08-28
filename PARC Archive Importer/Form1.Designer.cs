namespace PARC_Archive_Importer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonImportFiles = new System.Windows.Forms.Button();
            this.buttonOpenArch = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonWiden = new System.Windows.Forms.Button();
            this.buttonInject = new System.Windows.Forms.Button();
            this.listArch = new System.Windows.Forms.ListView();
            this.ListFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileDescStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SizeComp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsCompressed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.folder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listImport = new System.Windows.Forms.ListView();
            this.InjFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileInjectedDescStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileInjectedStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IDinArch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Isinarch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewOriginalReference = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // buttonImportFiles
            // 
            this.buttonImportFiles.Location = new System.Drawing.Point(680, 462);
            this.buttonImportFiles.Name = "buttonImportFiles";
            this.buttonImportFiles.Size = new System.Drawing.Size(75, 23);
            this.buttonImportFiles.TabIndex = 2;
            this.buttonImportFiles.Text = "Open Files";
            this.buttonImportFiles.UseVisualStyleBackColor = true;
            this.buttonImportFiles.Click += new System.EventHandler(this.buttonImportFiles_Click);
            // 
            // buttonOpenArch
            // 
            this.buttonOpenArch.Location = new System.Drawing.Point(13, 462);
            this.buttonOpenArch.Name = "buttonOpenArch";
            this.buttonOpenArch.Size = new System.Drawing.Size(80, 23);
            this.buttonOpenArch.TabIndex = 4;
            this.buttonOpenArch.Text = "Open Archive";
            this.buttonOpenArch.UseVisualStyleBackColor = true;
            this.buttonOpenArch.Click += new System.EventHandler(this.buttonOpenArch_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(1084, 462);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonWiden
            // 
            this.buttonWiden.Enabled = false;
            this.buttonWiden.Location = new System.Drawing.Point(99, 462);
            this.buttonWiden.Name = "buttonWiden";
            this.buttonWiden.Size = new System.Drawing.Size(75, 23);
            this.buttonWiden.TabIndex = 6;
            this.buttonWiden.Text = "Widen";
            this.buttonWiden.UseVisualStyleBackColor = true;
            this.buttonWiden.Click += new System.EventHandler(this.buttonWiden_Click);
            // 
            // buttonInject
            // 
            this.buttonInject.Location = new System.Drawing.Point(1003, 462);
            this.buttonInject.Name = "buttonInject";
            this.buttonInject.Size = new System.Drawing.Size(75, 23);
            this.buttonInject.TabIndex = 7;
            this.buttonInject.Text = "Inject";
            this.buttonInject.UseVisualStyleBackColor = true;
            this.buttonInject.Click += new System.EventHandler(this.buttonInject_Click);
            // 
            // listArch
            // 
            this.listArch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListFileName,
            this.FileDescStart,
            this.StartFile,
            this.fSize,
            this.SizeComp,
            this.IsCompressed,
            this.ID,
            this.folder});
            this.listArch.HideSelection = false;
            this.listArch.Location = new System.Drawing.Point(13, 12);
            this.listArch.Name = "listArch";
            this.listArch.Size = new System.Drawing.Size(661, 444);
            this.listArch.TabIndex = 9;
            this.listArch.UseCompatibleStateImageBehavior = false;
            this.listArch.View = System.Windows.Forms.View.Details;
            // 
            // ListFileName
            // 
            this.ListFileName.DisplayIndex = 1;
            this.ListFileName.Text = "File Name";
            this.ListFileName.Width = 175;
            // 
            // FileDescStart
            // 
            this.FileDescStart.DisplayIndex = 2;
            this.FileDescStart.Text = "Descr Start";
            this.FileDescStart.Width = 71;
            // 
            // StartFile
            // 
            this.StartFile.DisplayIndex = 3;
            this.StartFile.Text = "File Start";
            this.StartFile.Width = 70;
            // 
            // fSize
            // 
            this.fSize.DisplayIndex = 4;
            this.fSize.Text = "Size";
            this.fSize.Width = 78;
            // 
            // SizeComp
            // 
            this.SizeComp.DisplayIndex = 5;
            this.SizeComp.Text = "Size comp";
            this.SizeComp.Width = 85;
            // 
            // IsCompressed
            // 
            this.IsCompressed.DisplayIndex = 6;
            this.IsCompressed.Text = "Compressed";
            this.IsCompressed.Width = 43;
            // 
            // ID
            // 
            this.ID.DisplayIndex = 0;
            this.ID.Text = "ID";
            this.ID.Width = 41;
            // 
            // folder
            // 
            this.folder.Text = "Folder";
            // 
            // listImport
            // 
            this.listImport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.InjFileName,
            this.FileInjectedDescStart,
            this.FileInjectedStart,
            this.IDinArch,
            this.Isinarch,
            this.columnHeader9,
            this.columnHeader10});
            this.listImport.HideSelection = false;
            this.listImport.Location = new System.Drawing.Point(680, 12);
            this.listImport.Name = "listImport";
            this.listImport.Size = new System.Drawing.Size(479, 444);
            this.listImport.TabIndex = 10;
            this.listImport.UseCompatibleStateImageBehavior = false;
            this.listImport.View = System.Windows.Forms.View.Details;
            // 
            // InjFileName
            // 
            this.InjFileName.Text = "File Name";
            this.InjFileName.Width = 178;
            // 
            // FileInjectedDescStart
            // 
            this.FileInjectedDescStart.DisplayIndex = 2;
            this.FileInjectedDescStart.Text = "FileInjectedDescStart";
            this.FileInjectedDescStart.Width = 68;
            // 
            // FileInjectedStart
            // 
            this.FileInjectedStart.DisplayIndex = 3;
            this.FileInjectedStart.Text = "FileInjectedStart";
            this.FileInjectedStart.Width = 85;
            // 
            // IDinArch
            // 
            this.IDinArch.DisplayIndex = 4;
            this.IDinArch.Text = "ID";
            this.IDinArch.Width = 45;
            // 
            // Isinarch
            // 
            this.Isinarch.DisplayIndex = 1;
            this.Isinarch.Text = "Exists";
            this.Isinarch.Width = 57;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "";
            // 
            // listViewOriginalReference
            // 
            this.listViewOriginalReference.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listViewOriginalReference.Enabled = false;
            this.listViewOriginalReference.HideSelection = false;
            this.listViewOriginalReference.Location = new System.Drawing.Point(13, 12);
            this.listViewOriginalReference.Name = "listViewOriginalReference";
            this.listViewOriginalReference.Size = new System.Drawing.Size(483, 444);
            this.listViewOriginalReference.TabIndex = 12;
            this.listViewOriginalReference.UseCompatibleStateImageBehavior = false;
            this.listViewOriginalReference.View = System.Windows.Forms.View.Details;
            this.listViewOriginalReference.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 1;
            this.columnHeader1.Text = "File Name";
            this.columnHeader1.Width = 175;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 2;
            this.columnHeader2.Text = "Descr Start";
            this.columnHeader2.Width = 71;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 3;
            this.columnHeader3.Text = "File Start";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.DisplayIndex = 4;
            this.columnHeader4.Text = "Size";
            this.columnHeader4.Width = 78;
            // 
            // columnHeader5
            // 
            this.columnHeader5.DisplayIndex = 5;
            this.columnHeader5.Text = "Size comp";
            this.columnHeader5.Width = 85;
            // 
            // columnHeader6
            // 
            this.columnHeader6.DisplayIndex = 6;
            this.columnHeader6.Text = "Compressed";
            this.columnHeader6.Width = 43;
            // 
            // columnHeader7
            // 
            this.columnHeader7.DisplayIndex = 0;
            this.columnHeader7.Text = "ID";
            this.columnHeader7.Width = 41;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Folder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 496);
            this.Controls.Add(this.listViewOriginalReference);
            this.Controls.Add(this.listImport);
            this.Controls.Add(this.listArch);
            this.Controls.Add(this.buttonInject);
            this.Controls.Add(this.buttonWiden);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonOpenArch);
            this.Controls.Add(this.buttonImportFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PARC Archive Importer";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonImportFiles;
        private System.Windows.Forms.Button buttonOpenArch;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonWiden;
        private System.Windows.Forms.Button buttonInject;
        private System.Windows.Forms.ListView listArch;
        private System.Windows.Forms.ColumnHeader ListFileName;
        private System.Windows.Forms.ColumnHeader FileDescStart;
        private System.Windows.Forms.ColumnHeader StartFile;
        private System.Windows.Forms.ColumnHeader fSize;
        private System.Windows.Forms.ColumnHeader SizeComp;
        private System.Windows.Forms.ColumnHeader IsCompressed;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ListView listImport;
        private System.Windows.Forms.ColumnHeader InjFileName;
        private System.Windows.Forms.ColumnHeader FileInjectedDescStart;
        private System.Windows.Forms.ColumnHeader FileInjectedStart;
        private System.Windows.Forms.ColumnHeader IDinArch;
        private System.Windows.Forms.ColumnHeader Isinarch;
        private System.Windows.Forms.ColumnHeader folder;
        private System.Windows.Forms.ListView listViewOriginalReference;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}

