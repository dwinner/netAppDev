using System.Runtime.InteropServices;

var systemTime = new SystemTime();
GetSystemTime(systemTime);
Console.WriteLine(systemTime.Year);
return;

[DllImport("kernel32.dll")]
static extern void GetSystemTime(SystemTime systemTime);