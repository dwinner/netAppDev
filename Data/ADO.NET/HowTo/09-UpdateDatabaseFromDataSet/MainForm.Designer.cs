﻿namespace UpdateDatabaseFromDataSet
{
    partial class MainForm
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
         this.dataGridView = new System.Windows.Forms.DataGridView();
         this.buttonUpdate = new System.Windows.Forms.Button();
         this.label1 = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
         this.SuspendLayout();
         // 
         // dataGridView
         // 
         this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dataGridView.Location = new System.Drawing.Point(12, 12);
         this.dataGridView.Name = "dataGridView";
         this.dataGridView.Size = new System.Drawing.Size(459, 211);
         this.dataGridView.TabIndex = 0;
         // 
         // buttonUpdate
         // 
         this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonUpdate.Location = new System.Drawing.Point(396, 229);
         this.buttonUpdate.Name = "buttonUpdate";
         this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
         this.buttonUpdate.TabIndex = 1;
         this.buttonUpdate.Text = "&Update";
         this.buttonUpdate.UseVisualStyleBackColor = true;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(12, 229);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(126, 13);
         this.label1.TabIndex = 2;
         this.label1.Text = "Double-click to edit a cell";
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(483, 264);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.buttonUpdate);
         this.Controls.Add(this.dataGridView);
         this.Name = "MainForm";
         this.Text = "My form";
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label label1;
    }
}

