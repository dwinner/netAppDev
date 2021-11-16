using System;
using System.IO;
using System.Linq;

namespace CsCsLang
{
   internal class CopyFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var source = Utils.GetStringOrVarValue(script);
         script.MoveForwardIf(Constants.NextArg, Constants.Space);
         var dest = Utils.GetStringOrVarValue(script);

         var src = Path.GetFullPath(source);
         var dst = Path.GetFullPath(dest);

         var srcPaths = Utils.GetPathnames(src);
         var multipleFiles = srcPaths.Count > 1;
         if (dst.EndsWith("*")) dst = dst.Remove(dst.Count() - 1);
         if ((multipleFiles || Directory.Exists(src)) &&
             !Directory.Exists(dst))
            try
            {
               Directory.CreateDirectory(dst);
            }
            catch (Exception exc)
            {
               throw new ArgumentException("Couldn't create [" + dst + "] :" + exc.Message);
            }

         foreach (var srcPath in srcPaths)
         {
            var filename = Path.GetFileName(srcPath.String);
            //string dstPath  = Path.Combine(dst, filename);
            Utils.Copy(srcPath.String, dst);
         }

         return Variable.EmptyInstance;
      }
   }
}