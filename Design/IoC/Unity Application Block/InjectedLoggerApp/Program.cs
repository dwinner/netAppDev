using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace InjectedLoggerApp
{
	internal static class Program
	{
		private static void Main()
		{
			IUnityContainer container = new UnityContainer();
			var unityConfig = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
			unityConfig?.Configure(container);
			var logger = container.Resolve<DummyLogger>();
			logger.SelectedWriter.Write("Hello DI");
		}
	}
}