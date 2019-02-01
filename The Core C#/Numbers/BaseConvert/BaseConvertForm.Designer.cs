namespace HowToCSharp.Ch05.BaseConvert
{
    partial class BaseConvertForm
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
         this.textBoxSource = new System.Windows.Forms.TextBox();
         this.baseNumberLabel = new System.Windows.Forms.Label();
         this.numericUpDownBase = new System.Windows.Forms.NumericUpDown();
         this.baseLabel = new System.Windows.Forms.Label();
         this.numberLabel = new System.Windows.Forms.Label();
         this.textBoxDest = new System.Windows.Forms.TextBox();
         ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBase)).BeginInit();
         this.SuspendLayout();
         // 
         // textBoxSource
         // 
         this.textBoxSource.Location = new System.Drawing.Point(136, 14);
         this.textBoxSource.Name = "textBoxSource";
         this.textBoxSource.Size = new System.Drawing.Size(235, 20);
         this.textBoxSource.TabIndex = 0;
         this.textBoxSource.TextChanged += new System.EventHandler(this.OnSourceChanged);
         // 
         // baseNumberLabel
         // 
         this.baseNumberLabel.AutoSize = true;
         this.baseNumberLabel.Location = new System.Drawing.Point(4, 18);
         this.baseNumberLabel.Name = "baseNumberLabel";
         this.baseNumberLabel.Size = new System.Drawing.Size(89, 13);
         this.baseNumberLabel.TabIndex = 1;
         this.baseNumberLabel.Text = "Base 10 Number:";
         // 
         // numericUpDownBase
         // 
         this.numericUpDownBase.Location = new System.Drawing.Point(35, 48);
         this.numericUpDownBase.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
         this.numericUpDownBase.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
         this.numericUpDownBase.Name = "numericUpDownBase";
         this.numericUpDownBase.Size = new System.Drawing.Size(46, 20);
         this.numericUpDownBase.TabIndex = 2;
         this.numericUpDownBase.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
         this.numericUpDownBase.ValueChanged += new System.EventHandler(this.OnBaseChanged);
         // 
         // baseLabel
         // 
         this.baseLabel.AutoSize = true;
         this.baseLabel.Location = new System.Drawing.Point(4, 52);
         this.baseLabel.Name = "baseLabel";
         this.baseLabel.Size = new System.Drawing.Size(31, 13);
         this.baseLabel.TabIndex = 3;
         this.baseLabel.Text = "Base";
         // 
         // numberLabel
         // 
         this.numberLabel.AutoSize = true;
         this.numberLabel.Location = new System.Drawing.Point(85, 52);
         this.numberLabel.Name = "numberLabel";
         this.numberLabel.Size = new System.Drawing.Size(47, 13);
         this.numberLabel.TabIndex = 4;
         this.numberLabel.Text = "Number:";
         // 
         // textBoxDest
         // 
         this.textBoxDest.Location = new System.Drawing.Point(136, 48);
         this.textBoxDest.Name = "textBoxDest";
         this.textBoxDest.Size = new System.Drawing.Size(235, 20);
         this.textBoxDest.TabIndex = 5;
         this.textBoxDest.TextChanged += new System.EventHandler(this.OnDestChanged);
         // 
         // BaseConvertForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(383, 87);
         this.Controls.Add(this.textBoxDest);
         this.Controls.Add(this.numberLabel);
         this.Controls.Add(this.baseLabel);
         this.Controls.Add(this.numericUpDownBase);
         this.Controls.Add(this.baseNumberLabel);
         this.Controls.Add(this.textBoxSource);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "BaseConvertForm";
         this.Text = "BaseConvert";
         ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBase)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSource;
        private System.Windows.Forms.Label baseNumberLabel;
        private System.Windows.Forms.NumericUpDown numericUpDownBase;
        private System.Windows.Forms.Label baseLabel;
        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.TextBox textBoxDest;
    }
}

