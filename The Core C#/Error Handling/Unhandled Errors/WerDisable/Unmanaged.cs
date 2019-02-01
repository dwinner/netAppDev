using System.Runtime.InteropServices;

namespace WerDisable
{
   internal static class Unmanaged
   {
      [DllImport("kernel32.dll")]
      internal static extern ErrorModes SetErrorMode(ErrorModes uMode);
   }
}