namespace CallingDifferentWindows
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
			this.wrongModelessWpfButton = new System.Windows.Forms.Button();
			this.rightModelessButton = new System.Windows.Forms.Button();
			this.mixedFormButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// wrongModelessWpfButton
			// 
			this.wrongModelessWpfButton.Location = new System.Drawing.Point(12, 12);
			this.wrongModelessWpfButton.Name = "wrongModelessWpfButton";
			this.wrongModelessWpfButton.Size = new System.Drawing.Size(260, 44);
			this.wrongModelessWpfButton.TabIndex = 0;
			this.wrongModelessWpfButton.Text = "Show a modeless WPF window (the wrong way)";
			this.wrongModelessWpfButton.UseVisualStyleBackColor = true;
			this.wrongModelessWpfButton.Click += new System.EventHandler(this.wrongModelessWpfButton_Click);
			// 
			// rightModelessButton
			// 
			this.rightModelessButton.Location = new System.Drawing.Point(12, 62);
			this.rightModelessButton.Name = "rightModelessButton";
			this.rightModelessButton.Size = new System.Drawing.Size(260, 39);
			this.rightModelessButton.TabIndex = 1;
			this.rightModelessButton.Text = "Show a modeless WPF window (the right way)";
			this.rightModelessButton.UseVisualStyleBackColor = true;
			this.rightModelessButton.Click += new System.EventHandler(this.rightModelessButton_Click);
			// 
			// mixedFormButton
			// 
			this.mixedFormButton.Location = new System.Drawing.Point(12, 107);
			this.mixedFormButton.Name = "mixedFormButton";
			this.mixedFormButton.Size = new System.Drawing.Size(260, 42);
			this.mixedFormButton.TabIndex = 2;
			this.mixedFormButton.Text = "Show a form with mixed content";
			this.mixedFormButton.UseVisualStyleBackColor = true;
			this.mixedFormButton.Click += new System.EventHandler(this.mixedFormButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.mixedFormButton);
			this.Controls.Add(this.rightModelessButton);
			this.Controls.Add(this.wrongModelessWpfButton);
			this.Name = "MainForm";
			this.Text = "Main form";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button wrongModelessWpfButton;
		private System.Windows.Forms.Button rightModelessButton;
		private System.Windows.Forms.Button mixedFormButton;
	}
}