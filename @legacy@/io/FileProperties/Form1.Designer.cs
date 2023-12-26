namespace FileProperties
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
         this.FoldersLabel = new System.Windows.Forms.Label();
         this.DisplayButton = new System.Windows.Forms.Button();
         this.UpButton = new System.Windows.Forms.Button();
         this.LastWriteTimeTextBox = new System.Windows.Forms.TextBox();
         this.FilesLabel = new System.Windows.Forms.Label();
         this.InputTextBox = new System.Windows.Forms.TextBox();
         this.FolderNameLabel = new System.Windows.Forms.Label();
         this.CreationTimeTextBox = new System.Windows.Forms.TextBox();
         this.FilesListBox = new System.Windows.Forms.ListBox();
         this.FoldersListBox = new System.Windows.Forms.ListBox();
         this.ContentGroupBox = new System.Windows.Forms.GroupBox();
         this.LastModificationLabel = new System.Windows.Forms.Label();
         this.LastAccessTimeTextBox = new System.Windows.Forms.TextBox();
         this.LastAccessLabel = new System.Windows.Forms.Label();
         this.CreationTimeLabel = new System.Windows.Forms.Label();
         this.FileSizeTextBox = new System.Windows.Forms.TextBox();
         this.FileSizeLabel = new System.Windows.Forms.Label();
         this.FileNameTextBox = new System.Windows.Forms.TextBox();
         this.FileNameLabel = new System.Windows.Forms.Label();
         this.FolderTextBox = new System.Windows.Forms.TextBox();
         this.DetailsGroupBox = new System.Windows.Forms.GroupBox();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.label9 = new System.Windows.Forms.Label();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.label10 = new System.Windows.Forms.Label();
         this.label11 = new System.Windows.Forms.Label();
         this.textBox2 = new System.Windows.Forms.TextBox();
         this.label12 = new System.Windows.Forms.Label();
         this.textBox3 = new System.Windows.Forms.TextBox();
         this.label13 = new System.Windows.Forms.Label();
         this.textBox4 = new System.Windows.Forms.TextBox();
         this.groupBox4 = new System.Windows.Forms.GroupBox();
         this.button1 = new System.Windows.Forms.Button();
         this.textBox5 = new System.Windows.Forms.TextBox();
         this.ContentGroupBox.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.SuspendLayout();
         // 
         // FoldersLabel
         // 
         this.FoldersLabel.Location = new System.Drawing.Point(266, 109);
         this.FoldersLabel.Name = "FoldersLabel";
         this.FoldersLabel.Size = new System.Drawing.Size(100, 16);
         this.FoldersLabel.TabIndex = 30;
         this.FoldersLabel.Text = "Folders";
         // 
         // DisplayButton
         // 
         this.DisplayButton.Location = new System.Drawing.Point(442, 29);
         this.DisplayButton.Name = "DisplayButton";
         this.DisplayButton.Size = new System.Drawing.Size(75, 23);
         this.DisplayButton.TabIndex = 40;
         this.DisplayButton.Text = "Display";
         this.DisplayButton.Click += new System.EventHandler(this.OnDisplayButtonClick);
         // 
         // UpButton
         // 
         this.UpButton.Location = new System.Drawing.Point(418, 77);
         this.UpButton.Name = "UpButton";
         this.UpButton.Size = new System.Drawing.Size(72, 24);
         this.UpButton.TabIndex = 39;
         this.UpButton.Text = "Up";
         this.UpButton.Click += new System.EventHandler(this.OnUpButtonClick);
         // 
         // LastWriteTimeTextBox
         // 
         this.LastWriteTimeTextBox.Enabled = false;
         this.LastWriteTimeTextBox.Location = new System.Drawing.Point(42, 453);
         this.LastWriteTimeTextBox.Name = "LastWriteTimeTextBox";
         this.LastWriteTimeTextBox.Size = new System.Drawing.Size(184, 20);
         this.LastWriteTimeTextBox.TabIndex = 32;
         // 
         // FilesLabel
         // 
         this.FilesLabel.Location = new System.Drawing.Point(26, 109);
         this.FilesLabel.Name = "FilesLabel";
         this.FilesLabel.Size = new System.Drawing.Size(136, 16);
         this.FilesLabel.TabIndex = 37;
         this.FilesLabel.Text = "Files";
         // 
         // InputTextBox
         // 
         this.InputTextBox.Location = new System.Drawing.Point(26, 29);
         this.InputTextBox.Name = "InputTextBox";
         this.InputTextBox.Size = new System.Drawing.Size(408, 20);
         this.InputTextBox.TabIndex = 35;
         // 
         // FolderNameLabel
         // 
         this.FolderNameLabel.Location = new System.Drawing.Point(26, 13);
         this.FolderNameLabel.Name = "FolderNameLabel";
         this.FolderNameLabel.Size = new System.Drawing.Size(280, 16);
         this.FolderNameLabel.TabIndex = 36;
         this.FolderNameLabel.Text = "Enter name of folder to be examined and click Display";
         // 
         // CreationTimeTextBox
         // 
         this.CreationTimeTextBox.Enabled = false;
         this.CreationTimeTextBox.Location = new System.Drawing.Point(258, 405);
         this.CreationTimeTextBox.Name = "CreationTimeTextBox";
         this.CreationTimeTextBox.Size = new System.Drawing.Size(184, 20);
         this.CreationTimeTextBox.TabIndex = 31;
         // 
         // FilesListBox
         // 
         this.FilesListBox.Location = new System.Drawing.Point(26, 125);
         this.FilesListBox.Name = "FilesListBox";
         this.FilesListBox.Size = new System.Drawing.Size(240, 199);
         this.FilesListBox.TabIndex = 29;
         this.FilesListBox.SelectedIndexChanged += new System.EventHandler(this.OnListBoxFilesSelected);
         // 
         // FoldersListBox
         // 
         this.FoldersListBox.Location = new System.Drawing.Point(274, 125);
         this.FoldersListBox.Name = "FoldersListBox";
         this.FoldersListBox.Size = new System.Drawing.Size(240, 199);
         this.FoldersListBox.TabIndex = 33;
         this.FoldersListBox.SelectedIndexChanged += new System.EventHandler(this.OnListBoxFoldersSelected);
         // 
         // ContentGroupBox
         // 
         this.ContentGroupBox.Controls.Add(this.LastModificationLabel);
         this.ContentGroupBox.Controls.Add(this.LastAccessTimeTextBox);
         this.ContentGroupBox.Controls.Add(this.LastAccessLabel);
         this.ContentGroupBox.Controls.Add(this.CreationTimeLabel);
         this.ContentGroupBox.Controls.Add(this.FileSizeTextBox);
         this.ContentGroupBox.Controls.Add(this.FileSizeLabel);
         this.ContentGroupBox.Controls.Add(this.FileNameTextBox);
         this.ContentGroupBox.Controls.Add(this.FileNameLabel);
         this.ContentGroupBox.Controls.Add(this.FolderTextBox);
         this.ContentGroupBox.Controls.Add(this.DetailsGroupBox);
         this.ContentGroupBox.Location = new System.Drawing.Point(18, 53);
         this.ContentGroupBox.Name = "ContentGroupBox";
         this.ContentGroupBox.Size = new System.Drawing.Size(496, 432);
         this.ContentGroupBox.TabIndex = 41;
         this.ContentGroupBox.TabStop = false;
         this.ContentGroupBox.Text = "Contents of folder";
         // 
         // LastModificationLabel
         // 
         this.LastModificationLabel.Location = new System.Drawing.Point(24, 384);
         this.LastModificationLabel.Name = "LastModificationLabel";
         this.LastModificationLabel.Size = new System.Drawing.Size(120, 16);
         this.LastModificationLabel.TabIndex = 8;
         this.LastModificationLabel.Text = "Last modification time";
         // 
         // LastAccessTimeTextBox
         // 
         this.LastAccessTimeTextBox.Enabled = false;
         this.LastAccessTimeTextBox.Location = new System.Drawing.Point(240, 400);
         this.LastAccessTimeTextBox.Name = "LastAccessTimeTextBox";
         this.LastAccessTimeTextBox.Size = new System.Drawing.Size(184, 20);
         this.LastAccessTimeTextBox.TabIndex = 1;
         // 
         // LastAccessLabel
         // 
         this.LastAccessLabel.Location = new System.Drawing.Point(240, 376);
         this.LastAccessLabel.Name = "LastAccessLabel";
         this.LastAccessLabel.Size = new System.Drawing.Size(100, 16);
         this.LastAccessLabel.TabIndex = 7;
         this.LastAccessLabel.Text = "Last access time";
         // 
         // CreationTimeLabel
         // 
         this.CreationTimeLabel.Location = new System.Drawing.Point(240, 328);
         this.CreationTimeLabel.Name = "CreationTimeLabel";
         this.CreationTimeLabel.Size = new System.Drawing.Size(100, 16);
         this.CreationTimeLabel.TabIndex = 5;
         this.CreationTimeLabel.Text = "Creation time";
         // 
         // FileSizeTextBox
         // 
         this.FileSizeTextBox.Enabled = false;
         this.FileSizeTextBox.Location = new System.Drawing.Point(24, 352);
         this.FileSizeTextBox.Name = "FileSizeTextBox";
         this.FileSizeTextBox.Size = new System.Drawing.Size(184, 20);
         this.FileSizeTextBox.TabIndex = 1;
         // 
         // FileSizeLabel
         // 
         this.FileSizeLabel.Location = new System.Drawing.Point(24, 328);
         this.FileSizeLabel.Name = "FileSizeLabel";
         this.FileSizeLabel.Size = new System.Drawing.Size(100, 16);
         this.FileSizeLabel.TabIndex = 10;
         this.FileSizeLabel.Text = "File Size";
         // 
         // FileNameTextBox
         // 
         this.FileNameTextBox.Enabled = false;
         this.FileNameTextBox.Location = new System.Drawing.Point(80, 304);
         this.FileNameTextBox.Name = "FileNameTextBox";
         this.FileNameTextBox.Size = new System.Drawing.Size(400, 20);
         this.FileNameTextBox.TabIndex = 1;
         // 
         // FileNameLabel
         // 
         this.FileNameLabel.Location = new System.Drawing.Point(16, 304);
         this.FileNameLabel.Name = "FileNameLabel";
         this.FileNameLabel.Size = new System.Drawing.Size(64, 16);
         this.FileNameLabel.TabIndex = 6;
         this.FileNameLabel.Text = "File name";
         // 
         // FolderTextBox
         // 
         this.FolderTextBox.Enabled = false;
         this.FolderTextBox.Location = new System.Drawing.Point(8, 24);
         this.FolderTextBox.Name = "FolderTextBox";
         this.FolderTextBox.Size = new System.Drawing.Size(384, 20);
         this.FolderTextBox.TabIndex = 2;
         // 
         // DetailsGroupBox
         // 
         this.DetailsGroupBox.Location = new System.Drawing.Point(8, 280);
         this.DetailsGroupBox.Name = "DetailsGroupBox";
         this.DetailsGroupBox.Size = new System.Drawing.Size(480, 144);
         this.DetailsGroupBox.TabIndex = 11;
         this.DetailsGroupBox.TabStop = false;
         this.DetailsGroupBox.Text = "Details of Selected File";
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.label9);
         this.groupBox3.Controls.Add(this.textBox1);
         this.groupBox3.Controls.Add(this.label10);
         this.groupBox3.Controls.Add(this.label11);
         this.groupBox3.Controls.Add(this.textBox2);
         this.groupBox3.Controls.Add(this.label12);
         this.groupBox3.Controls.Add(this.textBox3);
         this.groupBox3.Controls.Add(this.label13);
         this.groupBox3.Controls.Add(this.textBox4);
         this.groupBox3.Controls.Add(this.groupBox4);
         this.groupBox3.Location = new System.Drawing.Point(18, 53);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(496, 432);
         this.groupBox3.TabIndex = 42;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Contents of folder";
         // 
         // label9
         // 
         this.label9.Location = new System.Drawing.Point(24, 384);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(120, 16);
         this.label9.TabIndex = 8;
         this.label9.Text = "Last modification time";
         // 
         // textBox1
         // 
         this.textBox1.Enabled = false;
         this.textBox1.Location = new System.Drawing.Point(240, 400);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(184, 20);
         this.textBox1.TabIndex = 1;
         // 
         // label10
         // 
         this.label10.Location = new System.Drawing.Point(240, 376);
         this.label10.Name = "label10";
         this.label10.Size = new System.Drawing.Size(100, 16);
         this.label10.TabIndex = 7;
         this.label10.Text = "Last access time";
         // 
         // label11
         // 
         this.label11.Location = new System.Drawing.Point(240, 328);
         this.label11.Name = "label11";
         this.label11.Size = new System.Drawing.Size(100, 16);
         this.label11.TabIndex = 5;
         this.label11.Text = "Creation time";
         // 
         // textBox2
         // 
         this.textBox2.Enabled = false;
         this.textBox2.Location = new System.Drawing.Point(24, 352);
         this.textBox2.Name = "textBox2";
         this.textBox2.Size = new System.Drawing.Size(184, 20);
         this.textBox2.TabIndex = 1;
         // 
         // label12
         // 
         this.label12.Location = new System.Drawing.Point(24, 328);
         this.label12.Name = "label12";
         this.label12.Size = new System.Drawing.Size(100, 16);
         this.label12.TabIndex = 10;
         this.label12.Text = "File Size";
         // 
         // textBox3
         // 
         this.textBox3.Enabled = false;
         this.textBox3.Location = new System.Drawing.Point(80, 304);
         this.textBox3.Name = "textBox3";
         this.textBox3.Size = new System.Drawing.Size(400, 20);
         this.textBox3.TabIndex = 1;
         // 
         // label13
         // 
         this.label13.Location = new System.Drawing.Point(16, 304);
         this.label13.Name = "label13";
         this.label13.Size = new System.Drawing.Size(64, 16);
         this.label13.TabIndex = 6;
         this.label13.Text = "File name";
         // 
         // textBox4
         // 
         this.textBox4.Enabled = false;
         this.textBox4.Location = new System.Drawing.Point(8, 24);
         this.textBox4.Name = "textBox4";
         this.textBox4.Size = new System.Drawing.Size(384, 20);
         this.textBox4.TabIndex = 2;
         // 
         // groupBox4
         // 
         this.groupBox4.Location = new System.Drawing.Point(8, 280);
         this.groupBox4.Name = "groupBox4";
         this.groupBox4.Size = new System.Drawing.Size(480, 144);
         this.groupBox4.TabIndex = 11;
         this.groupBox4.TabStop = false;
         this.groupBox4.Text = "If file selected ...";
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(410, 77);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(72, 24);
         this.button1.TabIndex = 38;
         this.button1.Text = "Up";
         // 
         // textBox5
         // 
         this.textBox5.Location = new System.Drawing.Point(26, 29);
         this.textBox5.Name = "textBox5";
         this.textBox5.Size = new System.Drawing.Size(408, 20);
         this.textBox5.TabIndex = 34;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(534, 512);
         this.Controls.Add(this.FoldersLabel);
         this.Controls.Add(this.DisplayButton);
         this.Controls.Add(this.UpButton);
         this.Controls.Add(this.LastWriteTimeTextBox);
         this.Controls.Add(this.FilesLabel);
         this.Controls.Add(this.InputTextBox);
         this.Controls.Add(this.FolderNameLabel);
         this.Controls.Add(this.CreationTimeTextBox);
         this.Controls.Add(this.FilesListBox);
         this.Controls.Add(this.FoldersListBox);
         this.Controls.Add(this.ContentGroupBox);
         this.Controls.Add(this.groupBox3);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.textBox5);
         this.Name = "Form1";
         this.Text = "FileProperties Sample";
         this.ContentGroupBox.ResumeLayout(false);
         this.ContentGroupBox.PerformLayout();
         this.groupBox3.ResumeLayout(false);
         this.groupBox3.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FoldersLabel;
        private System.Windows.Forms.Button DisplayButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.TextBox LastWriteTimeTextBox;
        private System.Windows.Forms.Label FilesLabel;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.Label FolderNameLabel;
        private System.Windows.Forms.TextBox CreationTimeTextBox;
        private System.Windows.Forms.ListBox FilesListBox;
        private System.Windows.Forms.ListBox FoldersListBox;
        private System.Windows.Forms.GroupBox ContentGroupBox;
        private System.Windows.Forms.Label LastModificationLabel;
        private System.Windows.Forms.TextBox LastAccessTimeTextBox;
        private System.Windows.Forms.Label LastAccessLabel;
        private System.Windows.Forms.Label CreationTimeLabel;
        private System.Windows.Forms.TextBox FileSizeTextBox;
        private System.Windows.Forms.Label FileSizeLabel;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.TextBox FolderTextBox;
        private System.Windows.Forms.GroupBox DetailsGroupBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox5;
    }
}

