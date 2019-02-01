using System;

namespace WerDisable
{
   [Flags]
   public enum ErrorModes
   {
      SystemDefault = 0x0,
      SemFailCriticalErrors = 0x0001,
      SemNoAlignmentFaultExcept = 0x0004,
      SemNoGpFaultErrorBox = 0x0002,
      SemNoOpenFileErrorBox = 0x8000
   }
}