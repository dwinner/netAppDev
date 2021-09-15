using System;
using System.IO;

namespace CsCsLang
{
   internal class MoveFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var source = Utils.GetStringOrVarValue(script);
         script.MoveForwardIf(Constants.NextArg, Constants.Space);
         var dest = Utils.GetStringOrVarValue(script);

         var src = Path.GetFullPath(source);
         var dst = Path.GetFullPath(dest);

         var isFile = File.Exists(src);
         var isDir = Directory.Exists(src);
         if (!isFile && !isDir) throw new ArgumentException("[" + src + "] doesn't exist");

         if (isFile && Directory.Exists(dst)) dst = Path.Combine(dst, Path.GetFileName(src));

         try
         {
            if (isFile) File.Move(src, dst);
            else Directory.Move(src, dst);
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't copy: " + exc.Message);
         }

         return Variable.EmptyInstance;
      }
   }
}