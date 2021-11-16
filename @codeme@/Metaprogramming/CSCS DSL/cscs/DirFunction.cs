using System;
using System.Collections.Generic;
using System.IO;

namespace CsCsLang
{
   internal class DirFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var dirname = !script.StillValid() || script.Current == Constants.EndStatement
            ? Directory.GetCurrentDirectory()
            : Utils.GetToken(script, Constants.NextOrEndArray);

         //List<Variable> results = Utils.GetPathnames(dirname);
         var results = new List<Variable>();

         var index = dirname.IndexOf('*');
         if (index < 0 && !Directory.Exists(dirname) && !File.Exists(dirname))
            throw new ArgumentException("Directory [" + dirname + "] doesn't exist");

         var pattern = Constants.AllFiles;

         try
         {
            var dir = index < 0 ? Path.GetFullPath(dirname) : dirname;
            if (File.Exists(dir))
            {
               var fi = new FileInfo(dir);
               Interpreter.Instance.AppendOutput(Utils.GetPathDetails(fi, fi.Name), true);
               results.Add(new Variable(fi.Name));
               return new Variable(results);
            }
            // Special dealing if there is a pattern (only * is supported at the moment)
            if (index >= 0)
            {
               pattern = Path.GetFileName(dirname);

               if (index > 0)
               {
                  var prefix = dirname.Substring(0, index);
                  var di = Directory.GetParent(prefix);
                  dirname = di.FullName;
               }
               else dirname = ".";
            }
            dir = Path.GetFullPath(dirname);
            // First get contents of the directory (unless there is a pattern)
            var dirInfo = new DirectoryInfo(dir);

            if (pattern == Constants.AllFiles)
            {
               Interpreter.Instance.AppendOutput(Utils.GetPathDetails(dirInfo, "."), true);
               if (dirInfo.Parent != null)
                  Interpreter.Instance.AppendOutput(Utils.GetPathDetails(dirInfo.Parent, ".."), true);
            }

            // Then get contents of all of the files in the directory
            var fileNames = dirInfo.GetFiles(pattern);
            foreach (var fi in fileNames)
               try
               {
                  Interpreter.Instance.AppendOutput(Utils.GetPathDetails(fi, fi.Name), true);
                  results.Add(new Variable(fi.Name));
               }
               catch (Exception)
               {
               }

            // Then get contents of all of the subdirs in the directory
            var dirInfos = dirInfo.GetDirectories(pattern);
            foreach (var di in dirInfos)
               try
               {
                  Interpreter.Instance.AppendOutput(Utils.GetPathDetails(di, di.Name), true);
                  results.Add(new Variable(di.Name));
               }
               catch (Exception)
               {
               }
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't list directory: " + exc.Message);
         }

         return new Variable(results);
      }
   }
}