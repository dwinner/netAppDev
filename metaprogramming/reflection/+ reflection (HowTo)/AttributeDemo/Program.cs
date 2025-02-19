﻿/**
 * Добавление нестандартного атрибута
 */

using System;
using System.Linq;

namespace AttributeDemo
{
   [Culture("en-US")]
   [Culture("en-GB")]
   class Program
   {
      static void Main()
      {
         CultureAttribute[] attributes =
            (CultureAttribute[]) (typeof (Program)).GetCustomAttributes(typeof (CultureAttribute), true);
         //easy comma-separated list
         string list = attributes.Aggregate("",
            (output, next) => (output.Length > 0)
               ? (output + ", " + next.Culture)
               : next.Culture);
         Console.WriteLine("Cultures of Program: {0}", list);

         Console.ReadKey();
      }
   }
}
