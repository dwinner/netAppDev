using System;
using System.Windows.Forms;

namespace PrimerInteroperability
{
	public partial class ToHostForm : Form
	{
		public ToHostForm()
		{
			InitializeComponent();
		}

		private void OnShowWpfWindow(object sender, EventArgs e)
		{
			var window = new MainWindow();
			window.Show();
		}
	}
}