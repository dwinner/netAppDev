using System;
using System.Windows;

namespace PrimerInteroperability
{
	public partial class WpfMnemonicTest
	{
		public WpfMnemonicTest()
		{
			InitializeComponent();
		}

		private void OnClicked(object sender, EventArgs e) => MessageBox.Show(sender.ToString());		
	}
}