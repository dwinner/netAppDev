using System;
using System.Runtime.InteropServices;

namespace Killing
{
   /// <summary>
   ///    "Неуправляемые" методы
   /// </summary>
   public static class Unmanaged
   {
      /// <summary>
      ///    Принудительное завершение процесса Win32-функцией TerminateProcess
      /// </summary>
      /// <param name="hProcess">Дескриптор процесса</param>
      /// <param name="uExitCode">Код завершения</param>
      /// <returns>true, если удачно завершился, false-в противном случае</returns>
      [DllImport("kernel32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
   }
}