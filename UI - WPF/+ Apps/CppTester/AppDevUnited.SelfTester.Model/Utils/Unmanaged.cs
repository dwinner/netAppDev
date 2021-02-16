using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AppDevUnited.SelfTester.Model.Utils
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
      private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

      /// <summary>
      ///    Безопасная версия вызова неуправляемой функции TerminateProcess
      /// </summary>
      /// <param name="process">Процесс</param>
      /// <param name="errorMessage">Сообщение об ошибке</param>
      /// <param name="uExitCode">Код завершения</param>
      /// <returns>true, если удачно завершился, false-в противном случае</returns>
      public static bool TryTerminateProcess(Process process, out string errorMessage, uint uExitCode = 0)
      {
         try
         {
            var success = TerminateProcess(process.Handle, uExitCode);
            errorMessage = success
               ? string.Empty
               : string.Format("Last Win32 Error Code is: {0}", Marshal.GetLastWin32Error());

            return success;
         }
         catch (Exception ex)
         {
            errorMessage = ex.Message;
            return false;
         }
      }

      [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
      public static extern bool FreeConsole();

      /// <summary>
      ///    Определяет, является ли окно видимым
      /// </summary>
      /// <param name="hWnd">Значение оконного дескриптора</param>
      /// <returns>true, если окно видимо, false - в противном случае</returns>
      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool IsWindowVisible(IntPtr hWnd);

      /// <summary>
      ///    Показывает окно в неблокирующем режиме
      /// </summary>
      /// <param name="hWnd">Действительный указатель оконного дескриптора</param>
      /// <param name="showOptions">Опции открытия окна</param>
      /// <returns>true, если операция успешна, false - в противном случае</returns>
      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool ShowWindowAsync(IntPtr hWnd, ShowOptions showOptions);

      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool ShowWindow(IntPtr hWnd, ShowOptions showOptions);

      internal enum ShowOptions
      {
         Hide = 0,
         ShowNormal = 1,
         ShowMinimized = 2,
         ShowMaximized = 3,
         ShowNoActivate = 4,
         Show = 5,
         Minimize = 6,
         ShowMinNoActive = 7,
         ShowNa = 8,
         Restore = 9,
         ShowDefault = 10,
         ForceMinimize = 11
      }
   }
}