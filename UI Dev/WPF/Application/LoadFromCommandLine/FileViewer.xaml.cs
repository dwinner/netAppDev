using System.IO;

namespace LoadFromCommandLine
{
   public partial class FileViewer
   {
      public FileViewer()
      {
         InitializeComponent();
      }

      public void LoadFile(string path)
      {
         Content = File.ReadAllText(path);
         Title = path;
      }
   }
}