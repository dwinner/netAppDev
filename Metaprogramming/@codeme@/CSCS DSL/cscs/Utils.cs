using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace CsCsLang
{
   public partial class Utils
   {
      private static readonly object s_mutexLock = new object();

      private static readonly Dictionary<string, Func<string, string>> m_compiledCode =
         new Dictionary<string, Func<string, string>>();

      public static void CheckArgs(int args, int expected, string msg)
      {
         if (args < expected)
            throw new ArgumentException("Expecting " + expected +
                                        " arguments but got " + args + " in " + msg);
      }

      public static void CheckPosInt(Variable variable)
      {
         CheckInteger(variable);
         if (variable.Value <= 0)
            throw new ArgumentException("Expected a positive integer instead of [" +
                                        variable.Value + "]");
      }

      public static void CheckPosInt(int number, string name)
      {
         if (number < 0)
            throw new ArgumentException("Expected a positive integer instead of [" +
                                        number + "] in [" + name + "]");
      }

      public static void CheckNonNegativeInt(Variable variable)
      {
         CheckInteger(variable);
         if (variable.Value < 0)
            throw new ArgumentException("Expected a non-negative integer instead of [" +
                                        variable.Value + "]");
      }

      public static void CheckInteger(Variable variable)
      {
         CheckNumber(variable);
         if (variable.Value % 1 != 0.0)
            throw new ArgumentException("Expected an integer instead of [" +
                                        variable.Value + "]");
      }

      public static void CheckNumber(Variable variable)
      {
         if (variable.Type != Variable.VarType.Number)
            throw new ArgumentException("Expected a number instead of [" +
                                        variable.AsString() + "]");
      }

      public static void CheckArray(Variable variable, string name)
      {
         if (variable.Tuple == null)
            throw new ArgumentException("An array expected for variable [" +
                                        name + "]");
      }

      public static void CheckNotEmpty(ParsingScript script, string varName, string name)
      {
         if (!script.StillValid() || string.IsNullOrWhiteSpace(varName))
            throw new ArgumentException("Incomplete arguments for [" + name + "]");
      }

      public static void CheckNotEnd(ParsingScript script, string name)
      {
         if (!script.StillValid()) throw new ArgumentException("Incomplete arguments for [" + name + "]");
      }

      public static void CheckNotNull(object obj, string name)
      {
         if (obj == null) throw new ArgumentException("Invalid argument in function [" + name + "]");
      }

      public static void CheckNotEnd(ParsingScript script)
      {
         if (!script.StillValid()) throw new ArgumentException("Incomplete function definition.");
      }

      public static void CheckNotEmpty(string varName, string name)
      {
         if (string.IsNullOrEmpty(varName)) throw new ArgumentException("Incomplete arguments for [" + name + "]");
      }

      public static void CheckNotNull(string name, ParserFunction func)
      {
         if (func == null) throw new ArgumentException("Variable or function [" + name + "] doesn't exist");
      }

      public static string GetPathDetails(FileSystemInfo fs, string name)
      {
         var pathname = fs.FullName;
         var isDir = (fs.Attributes & FileAttributes.Directory) != 0;

         var d = isDir ? 'd' : '-';
         var last = fs.LastWriteTime.ToString("MMM dd yyyy HH:mm");

         var user = string.Empty;
         var group = string.Empty;
         string links = null;
         var permissions = "rwx";
         long size = 0;

#if __MonoCS__
            Mono.Unix.UnixFileSystemInfo info;
            if (isDir) {
                info = new Mono.Unix.UnixDirectoryInfo(pathname);
            } else {
                info = new Mono.Unix.UnixFileInfo(pathname);
            }

            char ur = (info.FileAccessPermissions & Mono.Unix.FileAccessPermissions.UserRead)     != 0 ? 'r' : '-';
            char uw = (info.FileAccessPermissions & Mono.Unix.FileAccessPermissions.UserWrite)    != 0 ? 'w' : '-';
            char ux = (info.FileAccessPermissions & Mono.Unix.FileAccessPermissions.UserExecute)  != 0 ? 'x' : '-';
            char gr = (info.FileAccessPermissions & Mono.Unix.FileAccessPermissions.GroupRead)    != 0 ? 'r' : '-';
            char gw = (info.FileAccessPermissions & Mono.Unix.FileAccessPermissions.GroupWrite)   != 0 ? 'w' : '-';
            char gx = (info.FileAccessPermissions & Mono.Unix.FileAccessPermissions.GroupExecute) != 0 ? 'x' : '-';
            char or = (info.FileAccessPermissions & Mono.Unix.FileAccessPermissions.OtherRead)    != 0 ? 'r' : '-';
            char ow = (info.FileAccessPermissions & Mono.Unix.FileAccessPermissions.OtherWrite)   != 0 ? 'w' : '-';
            char ox = (info.FileAccessPermissions & Mono.Unix.FileAccessPermissions.OtherExecute) != 0 ? 'x' : '-';

            permissions = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                ur, uw, ux, gr, gw, gx, or, ow, ox);

            user = info.OwnerUser.UserName;
            group = info.OwnerGroup.GroupName;
            links = info.LinkCount.ToString();

            size = info.Length;

            if (info.IsSymbolicLink) {
                d = 's';
            }

#else

         if (isDir)
         {
            user = Directory.GetAccessControl(fs.FullName).GetOwner(
               typeof(NTAccount)).ToString();

            var di = fs as DirectoryInfo;
            size = di.GetFileSystemInfos().Length;
         }
         else
         {
            user = File.GetAccessControl(fs.FullName).GetOwner(
               typeof(NTAccount)).ToString();
            var fi = fs as FileInfo;
            size = fi.Length;

            string[] execs = {"exe", "bat", "msi"};
            var x = execs.Contains(fi.Extension.ToLower()) ? 'x' : '-';
            var w = !fi.IsReadOnly ? 'w' : '-';
            permissions = string.Format("r{0}{1}", w, x);
         }
#endif

         var data = string.Format("{0}{1} {2,4} {3,8} {4,8} {5,9} {6,23} {7}",
            d, permissions, links, user, group, size, last, name);

         return data;
      }

      public static List<Variable> GetPathnames(string path)
      {
         var pathname = Path.GetFullPath(path);
         var index = pathname.IndexOf('*');
         if (index < 0 && !Directory.Exists(pathname) && !File.Exists(pathname))
            throw new ArgumentException("Path [" + pathname + "] doesn't exist");

         var results = new List<Variable>();
         if (index < 0)
         {
            results.Add(new Variable(pathname));
            return results;
         }

         var dirName = Path.GetDirectoryName(path);

         try
         {
            var pattern = Path.GetFileName(pathname);

            pathname = index > 0 ? dirName : ".";

            /*if (index > 0) {
              string prefix = pathname.Substring(0, index);
              DirectoryInfo di = Directory.GetParent(prefix);
              pathname = di.FullName;
            } else {
              pathname = ".";
            }
    
            string dir = Path.GetFullPath(pathname);*/
            // First get contents of the directory
            var dirInfo = new DirectoryInfo(dirName);
            var fileNames = dirInfo.GetFiles(pattern);
            foreach (var fi in fileNames)
               try
               {
                  var newPath = Path.Combine(dirName, fi.Name);
                  results.Add(new Variable(newPath));
               }
               catch (Exception)
               {
               }

            // Then get contents of all of the subdirs in the directory
            var dirInfos = dirInfo.GetDirectories(pattern);
            foreach (var di in dirInfos)
               try
               {
                  var newPath = Path.Combine(dirName, di.Name);
                  results.Add(new Variable(newPath));
               }
               catch (Exception)
               {
               }
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't get files from " + path + ": " + exc.Message);
         }
         return results;
      }

      public static void Copy(string src, string dst)
      {
         var isFile = File.Exists(src);
         var isDir = Directory.Exists(src);
         if (!isFile && !isDir) throw new ArgumentException("[" + src + "] doesn't exist");
         try
         {
            if (isFile)
            {
               if (Directory.Exists(dst))
               {
                  var filename = Path.GetFileName(src);
                  dst = Path.Combine(dst, filename);
               }

               File.Copy(src, dst, true);
            }
            else DirectoryCopy(src, dst);
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't copy to [" + dst + "]: " + exc.Message);
         }
      }

      public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = true)
      {
         // Get the subdirectories for the specified directory.
         var dir = new DirectoryInfo(sourceDirName);

         if (!dir.Exists) throw new ArgumentException(sourceDirName + " directory doesn't exist");
         if (sourceDirName.Equals(destDirName, StringComparison.InvariantCultureIgnoreCase))
         {
            //throw new ArgumentException(sourceDirName + ": directories are same");
            var addPath = Path.GetFileName(sourceDirName);
            destDirName = Path.Combine(destDirName, addPath);
         }

         var dirs = dir.GetDirectories();
         // If the destination directory doesn't exist, create it.
         if (!Directory.Exists(destDirName)) Directory.CreateDirectory(destDirName);

         // Get the files in the directory and copy them to the new location.
         var files = dir.GetFiles();
         foreach (var file in files)
         {
            var tempPath = Path.Combine(destDirName, file.Name);
            File.Copy(file.FullName, tempPath, true);
         }

         // If copying subdirectories, copy them and their contents to new location.
         if (copySubDirs)
            foreach (var subdir in dirs)
            {
               var tempPath = Path.Combine(destDirName, subdir.Name);
               DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
            }
      }

      public static List<string> GetFiles(string path, string[] patterns, bool addDirs = true)
      {
         var files = new List<string>();
         GetFiles(path, patterns, ref files, addDirs);
         return files;
      }

      public static string GetFileEntry(string dir, int i, string startsWith)
      {
         var files = new List<string>();
         string[] patterns = {startsWith + "*"};
         GetFiles(dir, patterns, ref files, true, false);

         if (files.Count == 0) return "";
         i = i % files.Count;

         var pathname = files[i];
         if (files.Count == 1)
            pathname += Directory.Exists(Path.Combine(dir, pathname)) ? Path.DirectorySeparatorChar.ToString() : " ";
         return pathname;
      }

      public static void GetFiles(string path, string[] patterns, ref List<string> files,
         bool addDirs = true, bool recursive = true)
      {
         var option = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
         if (string.IsNullOrEmpty(path)) path = Directory.GetCurrentDirectory();

         var dirs = patterns.SelectMany(
            i => Directory.EnumerateDirectories(path, i, option)
         ).ToList();

         var extraFiles = patterns.SelectMany(
            i => Directory.EnumerateFiles(path, i, option)
         ).ToList();

         if (addDirs) files.AddRange(dirs);
         files.AddRange(extraFiles);

         if (!recursive)
         {
            files = files.Select(p => Path.GetFileName(p)).ToList();
            files.Sort();
         }
         /*foreach (string dir in dirs) {
           GetFiles (dir, patterns, addDirs);
         }*/
      }

      public static List<Variable> ConvertToResults(string[] items,
         bool print = false)
      {
         var results = new List<Variable>(items.Length);
         foreach (var item in items)
         {
            results.Add(new Variable(item));
            if (print) Interpreter.Instance.AppendOutput(item);
         }

         return results;
      }

      public static List<string> GetStringInFiles(string path, string search,
         string[] patterns, bool ignoreCase = true)
      {
         var allFiles = GetFiles(path, patterns, false /* no dirs */);
         var results = new List<string>();

         if (allFiles == null && allFiles.Count == 0) return results;

         var caseSense = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
         Parallel.ForEach(allFiles, currentFile =>
         {
            var contents = GetFileText(currentFile);
            if (contents.IndexOf(search, caseSense) >= 0)
               lock (s_mutexLock)
               {
                  results.Add(currentFile);
               }
         });

         return results;
      }

      private static void WriteBlinkingText(string text, int delay, bool visible)
      {
         if (visible) Console.Write(text);
         else Console.Write(new string(' ', text.Length));
         Console.CursorLeft -= text.Length;
         Thread.Sleep(delay);
      }

      public static string GetLine(int chars = 40) => "-".PadRight(chars, '-');

      public static string GetFileText(string filename)
      {
         var fileContents = string.Empty;
         if (File.Exists(filename)) fileContents = File.ReadAllText(filename);
         return fileContents;
      }

      public static string[] GetFileLines(string filename)
      {
         try
         {
            var lines = File.ReadAllLines(filename);
            return lines;
         }
         catch (Exception ex)
         {
            throw new ArgumentException("Couldn't read file from disk: " + ex.Message);
         }
      }

      public static string[] GetFileLines(string filename, int from, int count)
      {
         try
         {
            var allLines = File.ReadLines(filename).ToArray();
            if (allLines.Length <= count) return allLines;

            if (from < 0) from = allLines.Length - count;

            var lines = allLines.Skip(from).Take(count).ToArray();
            return lines;
         }
         catch (Exception ex)
         {
            throw new ArgumentException("Couldn't read file from disk: " + ex.Message);
         }
      }

      public static void WriteFileText(string filename, string text)
      {
         try
         {
            File.WriteAllText(filename, text);
         }
         catch (Exception ex)
         {
            throw new ArgumentException("Couldn't write file to disk: " + ex.Message);
         }
      }

      public static void AppendFileText(string filename, string text)
      {
         try
         {
            File.AppendAllText(filename, text);
         }
         catch (Exception ex)
         {
            throw new ArgumentException("Couldn't write file to disk: " + ex.Message);
         }
      }

      public static void ThrowException(ParsingScript script, string excName1,
         string errorToken = "", string excName2 = "")
      {
         var msg = Translation.GetErrorString(excName1);

         if (!string.IsNullOrWhiteSpace(errorToken))
         {
            msg = string.Format(msg, errorToken);
            var candidate = Translation.TryFindError(errorToken, script);

            if (!string.IsNullOrWhiteSpace(candidate) &&
                !string.IsNullOrWhiteSpace(excName2))
            {
               var extra = Translation.GetErrorString(excName2);
               msg += " " + string.Format(extra, candidate);
            }
         }

         if (!string.IsNullOrWhiteSpace(script.Filename))
         {
            var fileMsg = Translation.GetErrorString("errorFile");
            msg += Environment.NewLine + string.Format(fileMsg, script.Filename);
         }

         var lineNumber = -1;
         var line = script.GetOriginalLine(out lineNumber);
         if (lineNumber >= 0)
         {
            var lineMsg = Translation.GetErrorString("errorLine");
            msg += string.IsNullOrWhiteSpace(script.Filename) ? Environment.NewLine : " ";
            msg += string.Format(lineMsg, lineNumber + 1, line.Trim());
         }
         throw new ArgumentException(msg);
      }

      public static void PrintList(List<Variable> list, int from)
      {
         Console.Write("Merging list:");
         for (var i = from; i < list.Count; i++) Console.Write(" ({0}, '{1}')", list[i].Value, list[i].Action);
         Console.WriteLine();
      }

      public static void PrintColor(string output, ConsoleColor fgcolor)
      {
         var currentForeground = Console.ForegroundColor;
         Console.ForegroundColor = fgcolor;

         Interpreter.Instance.AppendOutput(output);
         //Console.Write(output);

         Console.ForegroundColor = currentForeground;
      }

      public static int GetSafeInt(List<Variable> args, int index, int defaultValue = 0)
      {
         if (args.Count <= index) return defaultValue;
         CheckNumber(args[index]);
         return args[index].AsInt();
      }

      public static double GetSafeDouble(List<Variable> args, int index, double defaultValue = 0.0)
      {
         if (args.Count <= index) return defaultValue;
         CheckNumber(args[index]);
         return args[index].AsDouble();
      }

      public static string GetSafeString(List<Variable> args, int index, string defaultValue = "")
      {
         if (args.Count <= index) return defaultValue;
         return args[index].AsString();
      }

      public static Variable GetSafeVariable(List<Variable> args, int index, Variable defaultValue = null)
      {
         if (args.Count <= index) return defaultValue;
         return args[index];
      }

      public static Variable GetVariable(string varName, ParsingScript script)
      {
         var func = ParserFunction.GetFunction(varName);
         CheckNotNull(varName, func);
         var varValue = func.GetValue(script);
         return varValue;
      }

      public static Variable InvokeCall(Type type, string methodName, string paramName,
         string paramValue, object master = null)
      {
         var key = type + "_" + methodName + "_" + paramName;
         Func<string, string> func = null;

         // Cache compiled function:
         if (!m_compiledCode.TryGetValue(key, out func))
         {
            var methodInfo = type.GetMethod(methodName, new[] {typeof(string)});
            var param = Expression.Parameter(typeof(string), paramName);

            var methodCall = master == null
               ? Expression.Call(methodInfo, param)
               : Expression.Call(Expression.Constant(master), methodInfo, param);
            var lambda =
               Expression.Lambda<Func<string, string>>(methodCall, param);
            func = lambda.Compile();
            m_compiledCode[key] = func;
         }

         var result = func(paramValue);
         return new Variable(result);
      }
   }
}