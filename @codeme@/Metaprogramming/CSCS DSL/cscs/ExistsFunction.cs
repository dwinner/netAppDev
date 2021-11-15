using System;
using System.IO;

namespace CsCsLang
{
   internal class ExistsFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var pathname = Utils.GetStringOrVarValue(script);

         var isFile = File.Exists(pathname);
         var isDir = Directory.Exists(pathname);
         if (!isFile && !isDir) throw new ArgumentException("[" + pathname + "] doesn't exist");
         var exists = false;
         try
         {
            if (isFile) exists = File.Exists(pathname);
            else exists = Directory.Exists(pathname);
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't delete [" + pathname + "] :" + exc.Message);
         }

         return new Variable(Convert.ToDouble(exists));
      }
   }
}