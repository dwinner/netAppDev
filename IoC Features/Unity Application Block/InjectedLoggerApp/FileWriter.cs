using System.IO;

namespace InjectedLoggerApp
{
	public sealed class FileWriter : IWriter
	{
		public void Write(string aMessage)
		{
			using (var streamWriter = new StreamWriter("c:\\TestUnity.txt", true))
			{
				streamWriter.WriteLine(aMessage);
			}
		}
	}
}