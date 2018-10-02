using System;
using System.IO;
using IoPath = System.IO.Path;

namespace PhotoGallery
{
   public class Photo
   {
      public Photo(string filename)
      {
         var info = new FileInfo(filename);
         Size = string.Format("{0:N0} KB", info.Length / 1024);
         DateTime = info.LastWriteTime;
         Name = info.Name;
         Path = info.DirectoryName;
      }

      public string Name { get; private set; }

      public DateTime DateTime { get; private set; }

      public string Size { get; private set; }

      public string Path { get; private set; }

      public string FullPath
      {
         get { return IoPath.Combine(Path, Name); }
      }

      public override string ToString()
      {
         return FullPath;
      }
   }
}