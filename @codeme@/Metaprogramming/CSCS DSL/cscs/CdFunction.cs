using System;
using System.IO;

namespace CsCsLang
{
   internal class CdFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         if (script.Substr().StartsWith(" ..")) script.Forward();
         var newDir = Utils.GetStringOrVarValue(script);

         try
         {
            if (newDir == "..")
            {
               var pwd = Directory.GetCurrentDirectory();
               var parent = Directory.GetParent(pwd);
               if (parent == null) throw new ArgumentException("No parent exists.");
               newDir = parent.FullName;
            }
            if (newDir.Length == 0) newDir = Environment.GetEnvironmentVariable("HOME");
            Directory.SetCurrentDirectory(newDir);

            newDir = Directory.GetCurrentDirectory();
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't change directory: " + exc.Message);
         }

         return new Variable(newDir);
      }
   }
}