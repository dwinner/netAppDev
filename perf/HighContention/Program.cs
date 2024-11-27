using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace HighContention
{
   internal static class Program
   {
      private const int NumTasks = 5;
      private static readonly object CollectionLock = new object();
      private static readonly List<string> Collection = new List<string>();

      private static void Main(string[] args)
      {
         var allTasks = new Task[NumTasks];
         for (var i = 0; i < NumTasks; i++)
         {
            allTasks[i] = Task.Run(() =>
            {
               while (true)
               {
                  lock (CollectionLock)
                  {
                     if (Collection.Count > 1000000)
                     {
                        Collection.RemoveAt(Collection.Count - 1);
                     }
                     else
                     {
                        Collection.Add(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                     }
                  }
               }
            });
         }

         Task.WaitAll(allTasks);
      }
   }
}