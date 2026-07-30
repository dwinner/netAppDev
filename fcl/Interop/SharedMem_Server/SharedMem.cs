using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SharedMem_Server;

public sealed class SharedMem : IDisposable
{
   private static readonly IntPtr NoFileHandle = new(-1);

   private IntPtr _fileHandle;

   public SharedMem(string name, bool existing, uint sizeInBytes)
   {
      if (existing)
      {
         _fileHandle = OpenFileMapping(FileRights.ReadWrite, false, name);
      }
      else
      {
         _fileHandle = CreateFileMapping(NoFileHandle, 0,
            FileProtection.ReadWrite,
            0, sizeInBytes, name);
      }

      if (_fileHandle == IntPtr.Zero)
      {
         throw new Win32Exception();
      }

      // Obtain a read/write map for the entire file
      Root = MapViewOfFile(_fileHandle, FileRights.ReadWrite, 0, 0, 0);

      if (Root == IntPtr.Zero)
      {
         throw new Win32Exception();
      }
   }

   public IntPtr Root { get; private set; }

   public void Dispose()
   {
      if (Root != IntPtr.Zero)
      {
         UnmapViewOfFile(Root);
      }

      if (_fileHandle != IntPtr.Zero)
      {
         CloseHandle(_fileHandle);
      }

      Root = _fileHandle = IntPtr.Zero;
   }

   [DllImport("kernel32.dll", SetLastError = true)]
   private static extern IntPtr CreateFileMapping(IntPtr hFile,
      int lpAttributes,
      FileProtection flProtect,
      uint dwMaximumSizeHigh,
      uint dwMaximumSizeLow,
      string lpName);

   [DllImport("kernel32.dll", SetLastError = true)]
   private static extern IntPtr OpenFileMapping(FileRights dwDesiredAccess,
      bool bInheritHandle,
      string lpName);

   [DllImport("kernel32.dll", SetLastError = true)]
   private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject,
      FileRights dwDesiredAccess,
      uint dwFileOffsetHigh,
      uint dwFileOffsetLow,
      uint dwNumberOfBytesToMap);

   [DllImport("Kernel32.dll", SetLastError = true)]
   private static extern bool UnmapViewOfFile(IntPtr map);

   [DllImport("kernel32.dll", SetLastError = true)]
   private static extern int CloseHandle(IntPtr hObject);
   // Here we're using enums because they're safer than constants

   private enum FileProtection : uint // constants from winnt.h
   {
      ReadOnly = 2,
      ReadWrite = 4
   }

   private enum FileRights : uint // constants from WinBASE.h
   {
      Read = 4,
      Write = 2,
      ReadWrite = Read + Write
   }
}