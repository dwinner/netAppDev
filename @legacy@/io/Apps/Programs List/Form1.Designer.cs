﻿namespace Programs_List
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
            this.action_btn_get = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DisplayVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InstallDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Publisher = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InstallLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VersionMinor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VersionMajor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EstimatedSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UninstallString = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HelpLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstDisplayHardware = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // action_btn_get
            // 
            this.action_btn_get.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.action_btn_get.ForeColor = System.Drawing.Color.Green;
            this.action_btn_get.Location = new System.Drawing.Point(697, 16);
            this.action_btn_get.Name = "action_btn_get";
            this.action_btn_get.Size = new System.Drawing.Size(136, 29);
            this.action_btn_get.TabIndex = 0;
            this.action_btn_get.Text = "GET The List";
            this.action_btn_get.UseVisualStyleBackColor = true;
            this.action_btn_get.Click += new System.EventHandler(this.OnGetInstalledPrograms);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "RESULT";
            // 
            // DisplayName
            // 
            this.DisplayName.Text = "DisplayName";
            this.DisplayName.Width = 72;
            // 
            // DisplayVersion
            // 
            this.DisplayVersion.Text = "DisplayVersion";
            this.DisplayVersion.Width = 73;
            // 
            // InstallDate
            // 
            this.InstallDate.Text = "InstallDate";
            this.InstallDate.Width = 70;
            // 
            // Publisher
            // 
            this.Publisher.DisplayIndex = 2;
            this.Publisher.Text = "Publisher";
            // 
            // InstallLocation
            // 
            this.InstallLocation.Text = "InstallLocation";
            // 
            // Version
            // 
            this.Version.Text = "Version";
            // 
            // VersionMinor
            // 
            this.VersionMinor.DisplayIndex = 3;
            this.VersionMinor.Text = "VersionMinor";
            // 
            // VersionMajor
            // 
            this.VersionMajor.DisplayIndex = 4;
            this.VersionMajor.Text = "VersionMajor";
            // 
            // EstimatedSize
            // 
            this.EstimatedSize.Text = "EstimatedSize";
            // 
            // UninstallString
            // 
            this.UninstallString.Text = "UninstallString";
            // 
            // HelpLink
            // 
            this.HelpLink.DisplayIndex = 6;
            this.HelpLink.Text = "HelpLink";
            // 
            // lstDisplayHardware
            // 
            this.lstDisplayHardware.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DisplayName,
            this.EstimatedSize,
            this.DisplayVersion,
            this.InstallDate,
            this.Publisher,
            this.InstallLocation,
            this.Version,
            this.VersionMinor,
            this.VersionMajor,
            this.UninstallString,
            this.HelpLink});
            this.lstDisplayHardware.Location = new System.Drawing.Point(17, 53);
            this.lstDisplayHardware.Name = "lstDisplayHardware";
            this.lstDisplayHardware.Size = new System.Drawing.Size(816, 493);
            this.lstDisplayHardware.TabIndex = 17;
            this.lstDisplayHardware.UseCompatibleStateImageBehavior = false;
            this.lstDisplayHardware.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(845, 561);
            this.Controls.Add(this.lstDisplayHardware);
            this.Controls.Add(this.action_btn_get);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "List of Programs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button action_btn_get;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader DisplayName;
        private System.Windows.Forms.ColumnHeader DisplayVersion;
        private System.Windows.Forms.ColumnHeader InstallDate;
        private System.Windows.Forms.ColumnHeader Publisher;
        private System.Windows.Forms.ColumnHeader InstallLocation;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ColumnHeader VersionMinor;
        private System.Windows.Forms.ColumnHeader VersionMajor;
        private System.Windows.Forms.ColumnHeader EstimatedSize;
        private System.Windows.Forms.ColumnHeader UninstallString;
        private System.Windows.Forms.ColumnHeader HelpLink;
        private System.Windows.Forms.ListView lstDisplayHardware;
    }
}

