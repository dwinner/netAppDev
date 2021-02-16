using System.Windows.Forms;

namespace PrimerInteroperability
{
	public partial class HostWinFormControl
	{
		public HostWinFormControl()
		{
			InitializeComponent();
		}

		private void OnRejected(object sender, MaskInputRejectedEventArgs e)
			=> ErrorTextLabel.Content = $"Error: {e.RejectionHint}";
	}
}