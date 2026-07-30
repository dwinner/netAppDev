using System.Runtime.InteropServices;
using System.Text;

var dirBuilder = new StringBuilder(256);
GetWindowsDirectory(dirBuilder, 256);
Console.WriteLine(dirBuilder.ToString());
return;

[DllImport("kernel32.dll")]
static extern int GetWindowsDirectory(StringBuilder sb, int maxChars);