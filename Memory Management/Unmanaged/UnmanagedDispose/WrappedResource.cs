using System;
using System.Runtime.InteropServices;

namespace Dispose
{
   public class WrappedResource
   {
      [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
         SetLastError = true)]
      public static extern IntPtr CreateFile(
         string lpFileName,
         uint dwDesiredAccess,
         uint dwSharedMode,
         IntPtr securityAttributes,
         uint dwCreationDisposition,
         uint dwFlagsAndAttributes,
         IntPtr hTemplateFile);

      [DllImport("kernel32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      private static extern bool CloseHandle(IntPtr hObject);

      private readonly IntPtr _handle = IntPtr.Zero;  // Тип IntPtr используется для представления дескрипторов ОС

      public WrappedResource(string fileName)
      {
         _handle = CreateFile(fileName,
            0x80000000, // Доступ только на чтение
            1,          // Совместное использование
            IntPtr.Zero,
            3,          // Открыть существующий
            0,
            IntPtr.Zero);
      }

      // Финализаторы выглядят как деструкторы С++,
      // но их поведение не детерминировано
      ~WrappedResource()
      {
         // Note: В реальных приложениях не помещайте в финализаторы ничего, кроме самого необходимого
         Console.WriteLine("In Finalizer");
         if (_handle != IntPtr.Zero)
         {
            CloseHandle(_handle);
         }
      }

      public void Close()
      {
         if (_handle != IntPtr.Zero)
         {
            // Файл закрыт, поэтому объект больше не нуждается в финализации
            GC.SuppressFinalize(this);
            CloseHandle(_handle);
         }
      }
   }
}
