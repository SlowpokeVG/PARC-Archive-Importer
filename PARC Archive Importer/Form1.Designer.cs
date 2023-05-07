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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonImportFiles = new System.Windows.Forms.Button();
            buttonOpenArch = new System.Windows.Forms.Button();
            buttonSave = new System.Windows.Forms.Button();
            buttonWiden = new System.Windows.Forms.Button();
            buttonInject = new System.Windows.Forms.Button();
            listArch = new System.Windows.Forms.ListView();
            ListFileName = new System.Windows.Forms.ColumnHeader();
            FileDescStart = new System.Windows.Forms.ColumnHeader();
            StartFile = new System.Windows.Forms.ColumnHeader();
            fSize = new System.Windows.Forms.ColumnHeader();
            SizeComp = new System.Windows.Forms.ColumnHeader();
            IsCompressed = new System.Windows.Forms.ColumnHeader();
            ID = new System.Windows.Forms.ColumnHeader();
            folder = new System.Windows.Forms.ColumnHeader();
            listImport = new System.Windows.Forms.ListView();
            InjFileName = new System.Windows.Forms.ColumnHeader();
            FileInjectedDescStart = new System.Windows.Forms.ColumnHeader();
            FileInjectedStart = new System.Windows.Forms.ColumnHeader();
            IDinArch = new System.Windows.Forms.ColumnHeader();
            IsinArch = new System.Windows.Forms.ColumnHeader();
            FilePath = new System.Windows.Forms.ColumnHeader();
            FileSize = new System.Windows.Forms.ColumnHeader();
            CompressedSize = new System.Windows.Forms.ColumnHeader();
            InjectedinArch = new System.Windows.Forms.ColumnHeader();
            listArchOriginalReference = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            columnHeader4 = new System.Windows.Forms.ColumnHeader();
            columnHeader5 = new System.Windows.Forms.ColumnHeader();
            columnHeader6 = new System.Windows.Forms.ColumnHeader();
            columnHeader7 = new System.Windows.Forms.ColumnHeader();
            columnHeader8 = new System.Windows.Forms.ColumnHeader();
            comboBoxCompression = new System.Windows.Forms.ComboBox();
            checkBoxMuteWarnings = new System.Windows.Forms.CheckBox();
            buttonClear = new System.Windows.Forms.Button();
            buttonRevert = new System.Windows.Forms.Button();
            radioButtonDecimal = new System.Windows.Forms.RadioButton();
            radioButtonHex = new System.Windows.Forms.RadioButton();
            contextMenuRightClick = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            groupBoxCompressionVersion = new System.Windows.Forms.GroupBox();
            radioButtonCompVer2 = new System.Windows.Forms.RadioButton();
            radioButtonCompVer1 = new System.Windows.Forms.RadioButton();
            checkBoxPadLast = new System.Windows.Forms.CheckBox();
            progressBarInject = new System.Windows.Forms.ProgressBar();
            contextMenuRightClick.SuspendLayout();
            groupBoxCompressionVersion.SuspendLayout();
            SuspendLayout();
            // 
            // buttonImportFiles
            // 
            buttonImportFiles.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonImportFiles.Enabled = false;
            buttonImportFiles.Location = new System.Drawing.Point(763, 885);
            buttonImportFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonImportFiles.Name = "buttonImportFiles";
            buttonImportFiles.Size = new System.Drawing.Size(88, 27);
            buttonImportFiles.TabIndex = 2;
            buttonImportFiles.Text = "Open Files";
            buttonImportFiles.UseVisualStyleBackColor = true;
            buttonImportFiles.Click += buttonImportFiles_Click;
            // 
            // buttonOpenArch
            // 
            buttonOpenArch.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonOpenArch.Location = new System.Drawing.Point(15, 885);
            buttonOpenArch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonOpenArch.Name = "buttonOpenArch";
            buttonOpenArch.Size = new System.Drawing.Size(93, 27);
            buttonOpenArch.TabIndex = 4;
            buttonOpenArch.Text = "Open Archive";
            buttonOpenArch.UseVisualStyleBackColor = true;
            buttonOpenArch.Click += buttonOpenArch_Click;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonSave.Enabled = false;
            buttonSave.Location = new System.Drawing.Point(1396, 885);
            buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(88, 27);
            buttonSave.TabIndex = 5;
            buttonSave.Text = "Save PAR";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonWiden
            // 
            buttonWiden.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonWiden.Enabled = false;
            buttonWiden.Location = new System.Drawing.Point(115, 885);
            buttonWiden.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonWiden.Name = "buttonWiden";
            buttonWiden.Size = new System.Drawing.Size(88, 27);
            buttonWiden.TabIndex = 6;
            buttonWiden.Text = "Widen";
            buttonWiden.UseVisualStyleBackColor = true;
            buttonWiden.Click += buttonWiden_Click;
            // 
            // buttonInject
            // 
            buttonInject.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonInject.Enabled = false;
            buttonInject.Location = new System.Drawing.Point(1302, 885);
            buttonInject.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonInject.Name = "buttonInject";
            buttonInject.Size = new System.Drawing.Size(88, 27);
            buttonInject.TabIndex = 7;
            buttonInject.Text = "Inject";
            buttonInject.UseVisualStyleBackColor = true;
            buttonInject.Click += buttonInject_Click;
            // 
            // listArch
            // 
            listArch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listArch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listArch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { ListFileName, FileDescStart, StartFile, fSize, SizeComp, IsCompressed, ID, folder });
            listArch.FullRowSelect = true;
            listArch.GridLines = true;
            listArch.Location = new System.Drawing.Point(15, 14);
            listArch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listArch.Name = "listArch";
            listArch.Size = new System.Drawing.Size(740, 836);
            listArch.TabIndex = 9;
            listArch.UseCompatibleStateImageBehavior = false;
            listArch.View = System.Windows.Forms.View.Details;
            // 
            // ListFileName
            // 
            ListFileName.DisplayIndex = 1;
            ListFileName.Text = "File Name";
            ListFileName.Width = 174;
            // 
            // FileDescStart
            // 
            FileDescStart.DisplayIndex = 2;
            FileDescStart.Text = "Desc. Start";
            FileDescStart.Width = 70;
            // 
            // StartFile
            // 
            StartFile.DisplayIndex = 3;
            StartFile.Text = "File Start";
            StartFile.Width = 70;
            // 
            // fSize
            // 
            fSize.DisplayIndex = 4;
            fSize.Text = "File Size";
            fSize.Width = 84;
            // 
            // SizeComp
            // 
            SizeComp.DisplayIndex = 5;
            SizeComp.Text = "Comp. Size";
            SizeComp.Width = 84;
            // 
            // IsCompressed
            // 
            IsCompressed.DisplayIndex = 6;
            IsCompressed.Text = "Compressed";
            IsCompressed.Width = 84;
            // 
            // ID
            // 
            ID.DisplayIndex = 0;
            ID.Text = "ID";
            ID.Width = 42;
            // 
            // folder
            // 
            folder.Text = "Folder";
            folder.Width = 131;
            // 
            // listImport
            // 
            listImport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listImport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listImport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { InjFileName, FileInjectedDescStart, FileInjectedStart, IDinArch, IsinArch, FilePath, FileSize, CompressedSize, InjectedinArch });
            listImport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            listImport.FullRowSelect = true;
            listImport.GridLines = true;
            listImport.Location = new System.Drawing.Point(763, 14);
            listImport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listImport.Name = "listImport";
            listImport.Size = new System.Drawing.Size(720, 836);
            listImport.TabIndex = 10;
            listImport.UseCompatibleStateImageBehavior = false;
            listImport.View = System.Windows.Forms.View.Details;
            // 
            // InjFileName
            // 
            InjFileName.DisplayIndex = 1;
            InjFileName.Text = "File Name";
            InjFileName.Width = 146;
            // 
            // FileInjectedDescStart
            // 
            FileInjectedDescStart.DisplayIndex = 4;
            FileInjectedDescStart.Text = "Desc. Start";
            FileInjectedDescStart.Width = 68;
            // 
            // FileInjectedStart
            // 
            FileInjectedStart.DisplayIndex = 5;
            FileInjectedStart.Text = "File Start";
            FileInjectedStart.Width = 68;
            // 
            // IDinArch
            // 
            IDinArch.DisplayIndex = 0;
            IDinArch.Text = "ID";
            IDinArch.Width = 42;
            // 
            // IsinArch
            // 
            IsinArch.DisplayIndex = 2;
            IsinArch.Text = "Exists";
            IsinArch.Width = 42;
            // 
            // FilePath
            // 
            FilePath.DisplayIndex = 8;
            FilePath.Text = "Path";
            FilePath.Width = 150;
            // 
            // FileSize
            // 
            FileSize.Text = "File Size";
            FileSize.Width = 72;
            // 
            // CompressedSize
            // 
            CompressedSize.Text = "Comp. Size";
            CompressedSize.Width = 72;
            // 
            // InjectedinArch
            // 
            InjectedinArch.DisplayIndex = 3;
            InjectedinArch.Text = "Injected";
            // 
            // listArchOriginalReference
            // 
            listArchOriginalReference.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            listArchOriginalReference.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7, columnHeader8 });
            listArchOriginalReference.Enabled = false;
            listArchOriginalReference.Location = new System.Drawing.Point(15, 14);
            listArchOriginalReference.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listArchOriginalReference.Name = "listArchOriginalReference";
            listArchOriginalReference.Size = new System.Drawing.Size(563, 836);
            listArchOriginalReference.TabIndex = 12;
            listArchOriginalReference.UseCompatibleStateImageBehavior = false;
            listArchOriginalReference.View = System.Windows.Forms.View.Details;
            listArchOriginalReference.Visible = false;
            // 
            // columnHeader1
            // 
            columnHeader1.DisplayIndex = 1;
            columnHeader1.Text = "File Name";
            columnHeader1.Width = 191;
            // 
            // columnHeader2
            // 
            columnHeader2.DisplayIndex = 2;
            columnHeader2.Text = "Desc. Start";
            columnHeader2.Width = 71;
            // 
            // columnHeader3
            // 
            columnHeader3.DisplayIndex = 3;
            columnHeader3.Text = "File Start";
            columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            columnHeader4.DisplayIndex = 4;
            columnHeader4.Text = "File Size";
            columnHeader4.Width = 78;
            // 
            // columnHeader5
            // 
            columnHeader5.DisplayIndex = 5;
            columnHeader5.Text = "Comp. Size";
            columnHeader5.Width = 85;
            // 
            // columnHeader6
            // 
            columnHeader6.DisplayIndex = 6;
            columnHeader6.Text = "Compressed";
            columnHeader6.Width = 43;
            // 
            // columnHeader7
            // 
            columnHeader7.DisplayIndex = 0;
            columnHeader7.Text = "ID";
            columnHeader7.Width = 41;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Folder";
            // 
            // comboBoxCompression
            // 
            comboBoxCompression.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            comboBoxCompression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxCompression.Enabled = false;
            comboBoxCompression.FormattingEnabled = true;
            comboBoxCompression.Items.AddRange(new object[] { "Uncompressed", "Compressed", "Mirror PAR File" });
            comboBoxCompression.Location = new System.Drawing.Point(1154, 887);
            comboBoxCompression.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxCompression.Name = "comboBoxCompression";
            comboBoxCompression.Size = new System.Drawing.Size(140, 23);
            comboBoxCompression.TabIndex = 13;
            comboBoxCompression.SelectionChangeCommitted += comboBoxCompression_Commit;
            // 
            // checkBoxMuteWarnings
            // 
            checkBoxMuteWarnings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkBoxMuteWarnings.AutoSize = true;
            checkBoxMuteWarnings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            checkBoxMuteWarnings.Location = new System.Drawing.Point(764, 860);
            checkBoxMuteWarnings.Name = "checkBoxMuteWarnings";
            checkBoxMuteWarnings.Size = new System.Drawing.Size(107, 19);
            checkBoxMuteWarnings.TabIndex = 14;
            checkBoxMuteWarnings.Text = "Mute Warnings";
            checkBoxMuteWarnings.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            buttonClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonClear.Enabled = false;
            buttonClear.Location = new System.Drawing.Point(858, 885);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new System.Drawing.Size(88, 27);
            buttonClear.TabIndex = 15;
            buttonClear.Text = "Clear Imports";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonRevert
            // 
            buttonRevert.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonRevert.Enabled = false;
            buttonRevert.Location = new System.Drawing.Point(210, 885);
            buttonRevert.Name = "buttonRevert";
            buttonRevert.Size = new System.Drawing.Size(106, 27);
            buttonRevert.TabIndex = 16;
            buttonRevert.Text = "Revert Changes";
            buttonRevert.UseVisualStyleBackColor = true;
            buttonRevert.Click += buttonRevert_Click;
            // 
            // radioButtonDecimal
            // 
            radioButtonDecimal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            radioButtonDecimal.AutoSize = true;
            radioButtonDecimal.Checked = true;
            radioButtonDecimal.Location = new System.Drawing.Point(15, 860);
            radioButtonDecimal.Name = "radioButtonDecimal";
            radioButtonDecimal.Size = new System.Drawing.Size(68, 19);
            radioButtonDecimal.TabIndex = 17;
            radioButtonDecimal.TabStop = true;
            radioButtonDecimal.Text = "Decimal";
            radioButtonDecimal.UseVisualStyleBackColor = true;
            // 
            // radioButtonHex
            // 
            radioButtonHex.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            radioButtonHex.AutoSize = true;
            radioButtonHex.Location = new System.Drawing.Point(89, 859);
            radioButtonHex.Name = "radioButtonHex";
            radioButtonHex.Size = new System.Drawing.Size(46, 19);
            radioButtonHex.TabIndex = 18;
            radioButtonHex.Text = "Hex";
            radioButtonHex.UseVisualStyleBackColor = true;
            radioButtonHex.CheckedChanged += radioButtonHex_CheckedChanged;
            // 
            // contextMenuRightClick
            // 
            contextMenuRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemAbout });
            contextMenuRightClick.Name = "contextMenuRightClick";
            contextMenuRightClick.Size = new System.Drawing.Size(241, 26);
            contextMenuRightClick.Text = "Help";
            // 
            // toolStripMenuItemAbout
            // 
            toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            toolStripMenuItemAbout.Size = new System.Drawing.Size(240, 22);
            toolStripMenuItemAbout.Text = "About PARC Archive Importer...";
            toolStripMenuItemAbout.Click += toolStripMenuItemAbout_Click;
            // 
            // groupBoxCompressionVersion
            // 
            groupBoxCompressionVersion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            groupBoxCompressionVersion.Controls.Add(radioButtonCompVer2);
            groupBoxCompressionVersion.Controls.Add(radioButtonCompVer1);
            groupBoxCompressionVersion.Enabled = false;
            groupBoxCompressionVersion.Location = new System.Drawing.Point(1154, 851);
            groupBoxCompressionVersion.Name = "groupBoxCompressionVersion";
            groupBoxCompressionVersion.Size = new System.Drawing.Size(140, 35);
            groupBoxCompressionVersion.TabIndex = 19;
            groupBoxCompressionVersion.TabStop = false;
            groupBoxCompressionVersion.Text = "SLLZ Version";
            // 
            // radioButtonCompVer2
            // 
            radioButtonCompVer2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            radioButtonCompVer2.AutoSize = true;
            radioButtonCompVer2.Location = new System.Drawing.Point(74, 14);
            radioButtonCompVer2.Name = "radioButtonCompVer2";
            radioButtonCompVer2.Size = new System.Drawing.Size(31, 19);
            radioButtonCompVer2.TabIndex = 1;
            radioButtonCompVer2.Text = "2";
            radioButtonCompVer2.UseVisualStyleBackColor = true;
            // 
            // radioButtonCompVer1
            // 
            radioButtonCompVer1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            radioButtonCompVer1.AutoSize = true;
            radioButtonCompVer1.Checked = true;
            radioButtonCompVer1.Location = new System.Drawing.Point(10, 14);
            radioButtonCompVer1.Name = "radioButtonCompVer1";
            radioButtonCompVer1.Size = new System.Drawing.Size(31, 19);
            radioButtonCompVer1.TabIndex = 0;
            radioButtonCompVer1.TabStop = true;
            radioButtonCompVer1.Text = "1";
            radioButtonCompVer1.UseVisualStyleBackColor = true;
            // 
            // checkBoxPadLast
            // 
            checkBoxPadLast.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkBoxPadLast.AutoSize = true;
            checkBoxPadLast.Checked = true;
            checkBoxPadLast.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxPadLast.Location = new System.Drawing.Point(1397, 859);
            checkBoxPadLast.Name = "checkBoxPadLast";
            checkBoxPadLast.Size = new System.Drawing.Size(91, 19);
            checkBoxPadLast.TabIndex = 20;
            checkBoxPadLast.Text = "Pad Last File";
            checkBoxPadLast.UseVisualStyleBackColor = true;
            // 
            // progressBarInject
            // 
            progressBarInject.ForeColor = System.Drawing.Color.SlateBlue;
            progressBarInject.Location = new System.Drawing.Point(1306, 888);
            progressBarInject.Maximum = 30;
            progressBarInject.Name = "progressBarInject";
            progressBarInject.Size = new System.Drawing.Size(80, 21);
            progressBarInject.Step = 1;
            progressBarInject.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBarInject.TabIndex = 21;
            progressBarInject.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1498, 924);
            ContextMenuStrip = contextMenuRightClick;
            Controls.Add(progressBarInject);
            Controls.Add(checkBoxPadLast);
            Controls.Add(groupBoxCompressionVersion);
            Controls.Add(radioButtonHex);
            Controls.Add(radioButtonDecimal);
            Controls.Add(buttonRevert);
            Controls.Add(buttonClear);
            Controls.Add(checkBoxMuteWarnings);
            Controls.Add(comboBoxCompression);
            Controls.Add(listArchOriginalReference);
            Controls.Add(listImport);
            Controls.Add(listArch);
            Controls.Add(buttonInject);
            Controls.Add(buttonWiden);
            Controls.Add(buttonSave);
            Controls.Add(buttonOpenArch);
            Controls.Add(buttonImportFiles);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(1120, 600);
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PARC Archive Importer";
            Resize += Form1_Resize;
            contextMenuRightClick.ResumeLayout(false);
            groupBoxCompressionVersion.ResumeLayout(false);
            groupBoxCompressionVersion.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.ColumnHeader IsinArch;
        private System.Windows.Forms.ColumnHeader folder;
        private System.Windows.Forms.ListView listArchOriginalReference;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader FilePath;
        private System.Windows.Forms.ColumnHeader FileSize;
        private System.Windows.Forms.ComboBox comboBoxCompression;
        private System.Windows.Forms.CheckBox checkBoxMuteWarnings;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonRevert;
        private System.Windows.Forms.ColumnHeader InjectedinArch;
        private System.Windows.Forms.ColumnHeader CompressedSize;
        private System.Windows.Forms.RadioButton radioButtonDecimal;
        private System.Windows.Forms.RadioButton radioButtonHex;
        private System.Windows.Forms.ContextMenuStrip contextMenuRightClick;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.GroupBox groupBoxCompressionVersion;
        private System.Windows.Forms.RadioButton radioButtonCompVer2;
        private System.Windows.Forms.RadioButton radioButtonCompVer1;
        private System.Windows.Forms.CheckBox checkBoxPadLast;
        private System.Windows.Forms.ProgressBar progressBarInject;
    }
}

