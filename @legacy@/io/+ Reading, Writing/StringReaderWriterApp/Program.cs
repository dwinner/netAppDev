﻿using System;
using System.IO;
using System.Text;

namespace StringReaderWriterApp
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Fun with StringWriter / StringReader *****\n");

         // Create a StringWriter and emit character data to memory.
         using (StringWriter strWriter = new StringWriter())
         {
            strWriter.WriteLine("Don't forget Mother's Day this year...");
            Console.WriteLine("Contents of StringWriter:\n{0}", strWriter);

            // Get the internal StringBuilder.
            StringBuilder sb = strWriter.GetStringBuilder();
            sb.Insert(0, "Hey!! ");
            Console.WriteLine("-> {0}", sb);
            sb.Remove(0, "Hey!! ".Length);
            Console.WriteLine("-> {0}", sb);
         }

         using (StringWriter strWriter = new StringWriter())
         {
            strWriter.WriteLine("Don't forget Mother's Day this year...");
            Console.WriteLine("Contents of StringWriter:\n{0}", strWriter);

            // Read data from the StringWriter.
            using (StringReader strReader = new StringReader(strWriter.ToString()))
            {
               string input;
               while ((input = strReader.ReadLine()) != null)
               {
                  Console.WriteLine(input);
               }
            }
         }

         Console.ReadLine();
      }
   }
}
