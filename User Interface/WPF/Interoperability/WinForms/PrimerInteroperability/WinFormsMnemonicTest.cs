using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WPFButton = System.Windows.Controls.Button;
using GdiPoint = System.Drawing.Point;
using GdiSize = System.Drawing.Size;
using RoutedKeyEventArgs = System.Windows.Input.KeyEventArgs;
using WinFormMessageBox = System.Windows.Forms.MessageBox;

namespace PrimerInteroperability
{
	public partial class WinFormsMnemonicTest : Form
	{
		public WinFormsMnemonicTest()
		{
			InitializeComponent();
		}

		private void OnLoad(object sender, EventArgs e)
		{
			var button = new WPFButton {Content = "Alt+_A"};
			var host = new ElementHost
			{
				Child = button,
				Location = new GdiPoint(10, 10),
				Size = new GdiSize(100, 50)
			};

			button.Click += OnClick;
			button.PreviewKeyDown += OnPreviewKeyDown;

			Controls.Add(host);
		}

		private void OnPreviewKeyDown(object sender, RoutedKeyEventArgs e) => e.Handled = true;

		private void OnClick(object sender, EventArgs e) => WinFormMessageBox.Show(sender.ToString());
	}
}