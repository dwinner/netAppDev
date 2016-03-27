using System;
using System.Runtime.InteropServices;

namespace SplashScreenWinForms
{
   internal static class Win32
   {
      [Flags]
      public enum Soundflags
      {
         SndSync = 0x0000,
         SndAsync = 0x0001,
         SndNodefault = 0x0002,
         SndMemory = 0x0004,
         SndLoop = 0x0008,
         SndNostop = 0x0010,
         SndNowait = 0x00002000,
         SndAlias = 0x00010000,
         SndAliasId = 0x00110000,
         SndFilename = 0x00020000,
         SndResource = 0x00040004,
         SndPurge = 0x0040,
         SndApplication = 0x0080
      }

      [DllImport("winmm.dll", EntryPoint = "PlaySound", CharSet = CharSet.Auto)]
      public static extern int PlaySound(String pszSound, int hmod, int flags);
   }
}