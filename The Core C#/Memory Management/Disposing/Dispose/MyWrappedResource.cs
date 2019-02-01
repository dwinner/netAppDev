using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Finalizer
{
   public class WrappedResource : IDisposable
   {
      [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
         SetLastError = true)]
      private static extern IntPtr CreateFile(string lpFileName,
         uint dwDesiredAccess,
         uint dwSharedMode,
         IntPtr securityAttributes,
         uint dwCreationDisposition,
         uint dwFlagsAndAttributes,
         IntPtr hTemplateFile);

      [DllImport("kernel32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      private static extern bool CloseHandle(IntPtr hObject);

      private readonly IntPtr _handle;  // Тип IntPtr используется для представления дескрипторов ОС

      private bool _disposed;
      private readonly IDbConnection _conn;

      private const string ConnStr =
         "Data Source=DWINNER\\DWINNER;Initial Catalog=TestDB;User ID=sa;Password=bboytronik1985_DWINNER";

      public WrappedResource(string fileName)
      {
         _conn = new SqlConnection(ConnStr);   // Управляемый ресурс
         _handle = CreateFile(fileName, 0x80000000, 1, IntPtr.Zero, 3, 0, IntPtr.Zero);   // Неуправляемый ресурс
      }

      public void Close()
      {
         Dispose(true);
      }

      public void Dispose()
      {
         Console.WriteLine("In dispose");
         Dispose(true);
      }

      ~WrappedResource()
      {
         Console.WriteLine("In finalizer");
         Dispose(false);
      }

      protected virtual void Dispose(bool disposing)
      {
         // base.Dispose(disposing) В иерархии классов необходимо вызвать базовый класс!
         if (_disposed)
            return;
         _disposed = true;
         if (!disposing && _conn != null) // Note: Освобождение управляемых ресурсов
         {
            _conn.Dispose();
         }
         // Note: Здесь нужно производить освобождение неуправляемых ресурсов, если таковые имеются
         if (_handle != IntPtr.Zero)
         {
            CloseHandle(_handle);
         }
      }
   }
}
