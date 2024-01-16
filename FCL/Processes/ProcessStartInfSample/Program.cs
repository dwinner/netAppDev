using System.Diagnostics;

var psi = new ProcessStartInfo
{
   FileName = "cmd.exe",
   Arguments = "/c ipconfig /all",
   RedirectStandardOutput = true,
   UseShellExecute = false
};
var p = Process.Start(psi);
var result = p?.StandardOutput.ReadToEnd();
Console.WriteLine(result);