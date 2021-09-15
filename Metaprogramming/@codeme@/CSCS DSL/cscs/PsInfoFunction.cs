using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace CsCsLang
{
   // Returns process info
   internal class PsInfoFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var pattern = Utils.GetItem(script).String;
         if (string.IsNullOrWhiteSpace(pattern))
            throw new ArgumentException("Couldn't extract process name");

         const int maxProcName = 26;
         Interpreter.Instance.AppendOutput(Utils.GetLine(), true);
         Interpreter.Instance.AppendOutput(
            $"{"Process Id".PadRight(15)} {"Process Name".PadRight(maxProcName)} {"Working Set".PadRight(15)} {"Virt Mem".PadRight(15)} {"Start Time".PadRight(15)} {"CPU Time".PadRight(25)}",
            true);

         var processes = Process.GetProcessesByName(pattern);
         var results = new List<Variable>(processes.Length);
         foreach (var pr in processes)
         {
            var workingSet = (int) (pr.WorkingSet64 / 1000000.0);
            var virtMemory = (int) (pr.VirtualMemorySize64 / 1000000.0);
            var procTitle = $"{pr.ProcessName} {pr.MainWindowTitle.Split(null)[0]}";
            var startTime = pr.StartTime.ToString(CultureInfo.InvariantCulture);
            if (procTitle.Length > maxProcName) procTitle = procTitle.Substring(0, maxProcName);
            var procTime = string.Empty;
            try
            {
               procTime = pr.TotalProcessorTime.ToString().Substring(0, 11);
            }
            catch (Exception)
            {
            }

            results.Add(new Variable(
               string.Format("{0,15} {1,{6}} {2,15} {3,15} {4,15} {5,25}",
                  pr.Id, procTitle,
                  workingSet, virtMemory, startTime, procTime, maxProcName)));
            Interpreter.Instance.AppendOutput(results.Last().String, true);
         }

         Interpreter.Instance.AppendOutput(Utils.GetLine(), true);

         if (script.TryCurrent() == Constants.NextArg) script.Forward(); // eat end of statement semicolon

         return new Variable(results);
      }
   }

   // Kills a process with specified process id

   // Starts running a new process, returning its ID

   // Starts running an "echo" server

   // Starts running an "echo" client

   // Returns an environment variable

   // Sets an environment variable

   // Prints passed list of arguments

   // Reads either a string or a number from the Console.

   // Returns how much processor time has been spent on the current process

   // Returns current directory name

   // Equivalent to cd.. on Windows: one directory up

   // Changes directory to the passed one

   // Reads a file and returns all lines of that file as a "tuple" (list)

   // View the contents of a text file

   // View the last Constants.DEFAULT_FILE_LINES lines of a file

   // Append a line to a file

   // Apend a list of lines to a file

   // Write a line to a file

   // Write a list of lines to a file

   // Find a string in files

   // Find files having a given pattern

   // Copy a file or a directiry

   // Move a file or a directiry

   // Make a directory

   // Delete a file or a directory

   // Checks if a directory or a file exists

   // List files in a directory

   // Append a string to another string

   // Convert a string to the upper case

   // Convert a string to the lower case

   // Get a substring of a string

   // Get an index of a substring in a string. Return -1 if not found.
}