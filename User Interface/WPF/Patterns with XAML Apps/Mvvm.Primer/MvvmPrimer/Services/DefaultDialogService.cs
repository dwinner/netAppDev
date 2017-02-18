using System.Windows;
using Microsoft.Win32;

namespace MvvmPrimer.Services
{
	public sealed class DefaultDialogService : IDialogService
	{
		public void Show(string aMessage) => MessageBox.Show(aMessage);

		public string FilePath { get; private set; }

		public bool OpenFile()
		{
			var openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
			{
				FilePath = openFileDialog.FileName;
				return true;
			}

			return false;
		}

		public bool SaveFile()
		{
			var saveFileDialog = new SaveFileDialog();
			if (saveFileDialog.ShowDialog() == true)
			{
				FilePath = saveFileDialog.FileName;
				return true;
			}

			return false;
		}
	}
}