using System.Diagnostics;
using System.Text;

var test1 = Run("ipconfig.exe");
var output = test1.output;
var errors = test1.errors;

Console.WriteLine(output);
Console.WriteLine(errors);

return;

(string output, string errors) Run(string exePath, string args = "")
{
   using var p = Process.Start(new ProcessStartInfo(exePath, args)
   {
      RedirectStandardOutput = true,
      RedirectStandardError = true,
      UseShellExecute = false
   });

   var errors = new StringBuilder();

   // Read from the error stream asynchronously...
   p.ErrorDataReceived += (sender, errorArgs) =>
   {
      if (errorArgs.Data != null)
      {
         errors.AppendLine(errorArgs.Data);
      }
   };
   p.BeginErrorReadLine();

   // ...while we read from the output stream synchronously:
   var output = p.StandardOutput.ReadToEnd();

   p.WaitForExit();
   return (output, errors.ToString());
}