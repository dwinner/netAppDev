namespace Libraries
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.prgBatteryLength = new System.Windows.Forms.ProgressBar();
            this.lblPowerSource = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prgBatteryLength
            // 
            this.prgBatteryLength.Location = new System.Drawing.Point(34, 61);
            this.prgBatteryLength.Name = "prgBatteryLength";
            this.prgBatteryLength.Size = new System.Drawing.Size(204, 23);
            this.prgBatteryLength.TabIndex = 0;
            // 
            // lblPowerSource
            // 
            this.lblPowerSource.AutoSize = true;
            this.lblPowerSource.Location = new System.Drawing.Point(34, 42);
            this.lblPowerSource.Name = "lblPowerSource";
            this.lblPowerSource.Size = new System.Drawing.Size(0, 13);
            this.lblPowerSource.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 126);
            this.Controls.Add(this.lblPowerSource);
            this.Controls.Add(this.prgBatteryLength);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgBatteryLength;
        private System.Windows.Forms.Label lblPowerSource;
    }
}

