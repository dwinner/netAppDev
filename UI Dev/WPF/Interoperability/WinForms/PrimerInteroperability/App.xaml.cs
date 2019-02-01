using System.Windows;
using WinFormApp = System.Windows.Forms.Application;

namespace PrimerInteroperability
{
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			WinFormApp.EnableVisualStyles();
		}
	}
}