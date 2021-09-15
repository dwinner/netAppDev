using System;
using System.IO;

namespace CsCsLang
{
   internal class Cd__Function : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         string newDir = null;

         try
         {
            var pwd = Directory.GetCurrentDirectory();
            var parent = Directory.GetParent(pwd);
            if (parent == null) throw new ArgumentException("No parent exists.");
            newDir = parent.FullName;
            Directory.SetCurrentDirectory(newDir);
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't change directory: " + exc.Message);
         }

         return new Variable(newDir);
      }
   }
}