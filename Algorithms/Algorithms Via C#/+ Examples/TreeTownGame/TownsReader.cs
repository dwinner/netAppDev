using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TreeTownGame
{
   /// <summary>
   /// Вспомогательный класс для чтения городов в связный список
   /// </summary>
   public static class TownsReader
   {
      /// <summary>
      /// Чтение городов в связный список
      /// </summary>
      /// <param name="fileName">Путь к файлу</param>
      /// <returns>Связный список городов</returns>
      public static IEnumerable<string> ReadTowns(string fileName)
      {
         var townLinkedList = new LinkedList<string>();
         if (!File.Exists(fileName))         
            return townLinkedList;         

         using (var reader = new StreamReader(fileName, Encoding.GetEncoding(1251)))
         {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
               string trimLine = line.Trim();
               if (trimLine.Length > 0)               
                  townLinkedList.AddLast(trimLine);               
            }
         }

         return townLinkedList;
      }
   }
}
