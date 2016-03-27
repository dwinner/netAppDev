using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace DynamicFileReader
{
   public class DynamicFileHelper
   {
      public IList<dynamic> ParseCsvFile(string fileName)
      {
         var retList = new List<dynamic>();
         var fileStream = OpenFile(fileName);
         if (fileStream == null)
         {
            return retList;
         }

         string line = fileStream.ReadLine();
         if (line == null)
         {
            return retList;
         }

         string[] headerLine = line.Split(',');
         while (fileStream.Peek() > 0)
         {
            string currentDataLine = fileStream.ReadLine();
            if (currentDataLine == null)
            {
               continue;
            }

            string[] dataLine = currentDataLine.Split(',');
            dynamic dynamicEntity = new ExpandoObject();
            for (int i = 0; i < headerLine.Length; i++)
            {
               ((IDictionary<string, object>)dynamicEntity).Add(headerLine[i], dataLine[i]);
            }

            retList.Add(dynamicEntity);
         }

         return retList;
      }

      public StreamReader OpenFile(string fileName)
      {
         return File.Exists(fileName) ? new StreamReader(fileName) : null;
      }
   }
}