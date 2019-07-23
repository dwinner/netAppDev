namespace DelegateTest
{
    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.WinForms;
    using System.Data;

    /// <summary>
    ///    Summary description for Form1.
    /// </summary>
    public class Form1 : System.WinForms.Form
    {
        /// <summary>
        ///    Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components;
		private System.WinForms.Button AngryButton;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        ///    Clean up any resources being used.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            components.Dispose();
        }

        /// <summary>
        ///    Required method for Designer support - do not modify
        ///    the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container ();
			this.AngryButton = new System.WinForms.Button ();
			//@this.TrayHeight = 0;
			//@this.TrayLargeIcon = false;
			//@this.TrayAutoArrange = true;
			AngryButton.Location = new System.Drawing.Point (8, 16);
			AngryButton.Size = new System.Drawing.Size (112, 40);
			AngryButton.TabIndex = 0;
			AngryButton.Text = "MyButton";
			AngryButton.Click += new System.EventHandler(this.AngryButton_Click);
			this.Text = "Form1";
			this.AutoScaleBaseSize = new System.Drawing.Size (5, 13);
			this.ClientSize = new System.Drawing.Size (136, 77);
			this.Controls.Add(this.AngryButton);
		}

		protected void AngryButton_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("Please stop clicking me !!");
		}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args) 
        {
            Application.Run(new Form1());
        }
    }
}
