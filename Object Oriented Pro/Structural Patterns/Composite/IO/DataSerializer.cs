using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Composite.Implementation;

namespace Composite.IO
{
   public class DataSerializer
   {
      private const string DefaultStoreFileName = "project.dat";

      public Project<IProjectItem> Project { get; set; }

      public DataSerializer(Project<IProjectItem> project)
      {
         Project = project;
      }

      public void Store()
      {
         var binaryFormatter = new BinaryFormatter();
         using (Stream fileStream = new FileStream(DefaultStoreFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
         {
            binaryFormatter.Serialize(fileStream, Project);
         }
      }

      public Project<IProjectItem> Retrieve()
      {
         var binaryFormatter = new BinaryFormatter();
         using (Stream fileStream = File.OpenRead(DefaultStoreFileName))
         {
            var project = binaryFormatter.Deserialize(fileStream) as Project<IProjectItem>;
            if (project == null)
            {
               throw new NotSupportedException("Not Supported");
            }
            return project;
         }
      }
   }
}