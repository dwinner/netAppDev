/**
 * Сборки ср строгим именем
 */

using System;
using System.IO;
using System.Reflection;

namespace SharedDemo
{
   public class SharedDemo
   {
      private readonly string[] _quotes;
      private readonly Random _random;

      public string FullName
      {
         get
         {
            return Assembly.GetExecutingAssembly().FullName;
         }
      }

      public SharedDemo(string fileName)
      {
         _quotes = File.ReadAllLines(fileName);
         _random = new Random();
      }

      public string GetQuoteOfTheDay()
      {
         return _quotes[_random.Next(1, _quotes.Length)];
      }
   }
}
