using System;
using System.IO;
using System.Windows;

namespace LoadAppFromFileSample
{
	public partial class App
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var window = new MainWindow();
			string fileToLoad = null;

			// Если явно были переданы аргументы
			if (e.Args.Length > 0)
			{
				var arg = e.Args[0];
				if (File.Exists(arg) && Path.GetExtension(arg) == ".appdevunited")
					fileToLoad = arg;
			}
			else
			{
				// Приложение было вызвано по запросу файла на открытие
				var activationData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
				if (activationData != null && activationData.Length >= 1)
				{
					var arg = activationData[0];
					if (arg != null)
					{
						var fileUri = new Uri(arg);
						fileToLoad = Uri.UnescapeDataString(fileUri.AbsolutePath);
						if (File.Exists(arg) && Path.GetExtension(arg) == ".appdevunited")
							fileToLoad = arg;
					}
				}
			}

			if (!string.IsNullOrEmpty(fileToLoad))
				window.LoadFile(fileToLoad);

			window.Show();
		}
	}
}