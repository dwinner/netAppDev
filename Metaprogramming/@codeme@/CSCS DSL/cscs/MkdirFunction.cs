using System;
using System.IO;

namespace CsCsLang
{
   internal class MkdirFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var dirname = Utils.GetStringOrVarValue(script);
         try
         {
            Directory.CreateDirectory(dirname);
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't create [" + dirname + "] :" + exc.Message);
         }

         return Variable.EmptyInstance;
      }
   }
}