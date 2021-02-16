using System.IO;

namespace LoadAppFromFileSample
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public void LoadFile(string fileToLoad)
		{
			Content = File.ReadAllText(fileToLoad);
			Title = fileToLoad;
		}
	}
}