﻿namespace ListenToEvents
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
         this.BigButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // BigButton
         // 
         this.BigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.BigButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.BigButton.Location = new System.Drawing.Point(12, 12);
         this.BigButton.Name = "BigButton";
         this.BigButton.Size = new System.Drawing.Size(260, 240);
         this.BigButton.TabIndex = 0;
         this.BigButton.Text = "Click Me!";
         this.BigButton.UseVisualStyleBackColor = true;
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 264);
         this.Controls.Add(this.BigButton);
         this.Name = "MainForm";
         this.Text = "Listen for events";
         this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BigButton;
    }
}

