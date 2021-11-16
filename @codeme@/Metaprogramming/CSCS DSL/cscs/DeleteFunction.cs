using System;
using System.IO;

namespace CsCsLang
{
   internal class DeleteFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var pathname = Utils.GetStringOrVarValue(script);

         var isFile = File.Exists(pathname);
         var isDir = Directory.Exists(pathname);
         if (!isFile && !isDir) throw new ArgumentException("[" + pathname + "] doesn't exist");
         try
         {
            if (isFile) File.Delete(pathname);
            else Directory.Delete(pathname, true);
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't delete [" + pathname + "] :" + exc.Message);
         }

         return Variable.EmptyInstance;
      }
   }
}