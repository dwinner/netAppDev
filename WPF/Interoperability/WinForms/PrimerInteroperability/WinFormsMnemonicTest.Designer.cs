namespace PrimerInteroperability
{
	partial class WinFormsMnemonicTest
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
			this.testButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// testButton
			// 
			this.testButton.Location = new System.Drawing.Point(100, 115);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(75, 23);
			this.testButton.TabIndex = 0;
			this.testButton.Text = "Alt+&C";
			this.testButton.UseVisualStyleBackColor = true;
			this.testButton.Click += new System.EventHandler(this.OnClick);
			// 
			// WinFormsMnemonicTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.testButton);
			this.Name = "WinFormsMnemonicTest";
			this.Text = "WinFormsMnemonicTest";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button testButton;
	}
}