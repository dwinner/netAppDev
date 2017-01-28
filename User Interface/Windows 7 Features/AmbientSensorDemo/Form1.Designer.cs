namespace AmbientSensorDemo
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
            this.prgSensorProgress = new System.Windows.Forms.ProgressBar();
            this.lblSensor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prgSensorProgress
            // 
            this.prgSensorProgress.Location = new System.Drawing.Point(31, 56);
            this.prgSensorProgress.Maximum = 200;
            this.prgSensorProgress.Name = "prgSensorProgress";
            this.prgSensorProgress.Size = new System.Drawing.Size(362, 23);
            this.prgSensorProgress.TabIndex = 0;
            this.prgSensorProgress.Value = 100;
            // 
            // lblSensor
            // 
            this.lblSensor.AutoSize = true;
            this.lblSensor.Location = new System.Drawing.Point(31, 37);
            this.lblSensor.Name = "lblSensor";
            this.lblSensor.Size = new System.Drawing.Size(0, 13);
            this.lblSensor.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 99);
            this.Controls.Add(this.lblSensor);
            this.Controls.Add(this.prgSensorProgress);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgSensorProgress;
        private System.Windows.Forms.Label lblSensor;
    }
}

