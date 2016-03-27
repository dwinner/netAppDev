namespace TaskDialogDemo
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
            this.btnShow = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.rdError = new System.Windows.Forms.RadioButton();
            this.rdInfo = new System.Windows.Forms.RadioButton();
            this.rdShild = new System.Windows.Forms.RadioButton();
            this.rdWarn = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCollapseText = new System.Windows.Forms.TextBox();
            this.chkProgress = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(62, 257);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(73, 23);
            this.btnShow.TabIndex = 0;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 32);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(75, 13);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Your Message";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(105, 29);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(174, 20);
            this.txtMessage.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(143, 257);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // rdError
            // 
            this.rdError.AutoSize = true;
            this.rdError.Location = new System.Drawing.Point(114, 32);
            this.rdError.Name = "rdError";
            this.rdError.Size = new System.Drawing.Size(47, 17);
            this.rdError.TabIndex = 3;
            this.rdError.Text = "Error";
            this.rdError.UseVisualStyleBackColor = true;
            // 
            // rdInfo
            // 
            this.rdInfo.AutoSize = true;
            this.rdInfo.Checked = true;
            this.rdInfo.Location = new System.Drawing.Point(22, 31);
            this.rdInfo.Name = "rdInfo";
            this.rdInfo.Size = new System.Drawing.Size(77, 17);
            this.rdInfo.TabIndex = 4;
            this.rdInfo.TabStop = true;
            this.rdInfo.Text = "Information";
            this.rdInfo.UseVisualStyleBackColor = true;
            // 
            // rdShild
            // 
            this.rdShild.AutoSize = true;
            this.rdShild.Location = new System.Drawing.Point(22, 54);
            this.rdShild.Name = "rdShild";
            this.rdShild.Size = new System.Drawing.Size(48, 17);
            this.rdShild.TabIndex = 5;
            this.rdShild.Text = "Shild";
            this.rdShild.UseVisualStyleBackColor = true;
            // 
            // rdWarn
            // 
            this.rdWarn.AutoSize = true;
            this.rdWarn.Location = new System.Drawing.Point(114, 55);
            this.rdWarn.Name = "rdWarn";
            this.rdWarn.Size = new System.Drawing.Size(65, 17);
            this.rdWarn.TabIndex = 6;
            this.rdWarn.Text = "Warning";
            this.rdWarn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdInfo);
            this.groupBox1.Controls.Add(this.rdError);
            this.groupBox1.Controls.Add(this.rdWarn);
            this.groupBox1.Controls.Add(this.rdShild);
            this.groupBox1.Location = new System.Drawing.Point(27, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Warning Icon";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Content";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(107, 62);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(174, 20);
            this.txtContent.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Collapse Text";
            // 
            // txtCollapseText
            // 
            this.txtCollapseText.Location = new System.Drawing.Point(105, 89);
            this.txtCollapseText.Name = "txtCollapseText";
            this.txtCollapseText.Size = new System.Drawing.Size(174, 20);
            this.txtCollapseText.TabIndex = 10;
            // 
            // chkProgress
            // 
            this.chkProgress.AutoSize = true;
            this.chkProgress.Location = new System.Drawing.Point(27, 234);
            this.chkProgress.Name = "chkProgress";
            this.chkProgress.Size = new System.Drawing.Size(113, 17);
            this.chkProgress.TabIndex = 11;
            this.chkProgress.Text = "Show ProgressBar";
            this.chkProgress.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 303);
            this.Controls.Add(this.chkProgress);
            this.Controls.Add(this.txtCollapseText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnShow);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton rdError;
        private System.Windows.Forms.RadioButton rdInfo;
        private System.Windows.Forms.RadioButton rdShild;
        private System.Windows.Forms.RadioButton rdWarn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCollapseText;
        private System.Windows.Forms.CheckBox chkProgress;
    }
}

