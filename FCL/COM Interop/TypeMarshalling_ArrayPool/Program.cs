using System.Buffers;
using System.Runtime.InteropServices;

var windowsDir = GetWinDir();
Console.WriteLine(windowsDir);
return;

string GetWinDir()
{
   var array = ArrayPool<char>.Shared.Rent(256);
   try
   {
      int length = GetWindowsDirectory(array, 256);
      return new string(array, 0, length);
   }
   finally
   {
      ArrayPool<char>.Shared.Return(array);
   }

   [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
   static extern int GetWindowsDirectory(char[] buffer, int maxChars);
}