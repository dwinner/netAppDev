using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace DynamicFileReader
{
   public static class DynamicFileHelper
   {
      public static IEnumerable<dynamic> ParseFile(string fileName)
      {
         List<dynamic> retList = new();
         using var reader = OpenFile(fileName);
         if (reader == null)
         {
            return Enumerable.Empty<dynamic>();
         }

         string[] headerLine = reader.ReadLine()?.Split(',').Select(str => str.Trim()).ToArray()
                               ?? throw new InvalidOperationException("reader.ReadLine returned null");
         while (reader.Peek() > 0)
         {
            string[] dataLine = reader.ReadLine()?.Split(',')
                                ?? throw new InvalidOperationException("reader.Readline returned null");
            dynamic dynamicEntity = new ExpandoObject();
            for (var i = 0; i < headerLine.Length; i++)
            {
               ((IDictionary<string, object>)dynamicEntity).Add(headerLine[i], dataLine[i]);
            }

            retList.Add(dynamicEntity);
         }

         return retList;
      }

      private static StreamReader? OpenFile(string fileName) =>
         File.Exists(fileName) ? new StreamReader(File.OpenRead(fileName)) : null;
   }
}