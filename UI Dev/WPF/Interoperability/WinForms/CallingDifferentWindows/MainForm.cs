using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WpfWindowsLibrary;

namespace CallingDifferentWindows
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void wrongModelessWpfButton_Click(object sender, EventArgs e)
		{
			var wpfWindow = new SimpleWindow();
			wpfWindow.Show();
		}

		private void rightModelessButton_Click(object sender, EventArgs e)
		{
			var wpfWindow = new SimpleWindow();
			ElementHost.EnableModelessKeyboardInterop(wpfWindow);
			wpfWindow.Show();
		}

		private void mixedFormButton_Click(object sender, EventArgs e)
		{
			var mixedForm = new MixedForm();
			mixedForm.Show();
		}
	}
}