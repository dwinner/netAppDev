using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WpfWindowsLibrary;

namespace CallingDifferentWindows
{
	public partial class MixedForm : Form
	{
		public MixedForm()
		{
			InitializeComponent();
		}

		private void OnHostWpfContent(object sender, EventArgs e) => MessageBox.Show("Hello from Windows forms");

		private void OnMainFormLoad(object sender, EventArgs e)
		{
			var simpleControl = new SimpleControl();

			// Create the element host wrapper
			var wpfElementHost = new ElementHost
			{
				Child = simpleControl,
				Size = new Size(WpfHostFlowLayoutPanel.Width, WpfHostFlowLayoutPanel.Height)
			};

			WpfHostFlowLayoutPanel.Controls.Add(wpfElementHost);
		}
	}
}