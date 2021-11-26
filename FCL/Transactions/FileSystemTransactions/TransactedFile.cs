using System;
using System.IO;
using System.Security.Permissions;
using System.Transactions;
using Microsoft.Win32.SafeHandles;

namespace FileSystemTransactions
{
   public static class TransactedFile
   {
      internal const short FileAttributeNormal = 0x80;
      internal const short InvalidHandleValue = -1;
      internal const uint GenericRead = 0x80000000;
      internal const uint GenericWrite = 0x40000000;
      internal const uint CreateNew = 1;
      internal const uint CreateAlways = 2;
      internal const uint OpenExisting = 3;

      [FileIOPermission(SecurityAction.Demand, Unrestricted = true)]
      public static FileStream GetTransactedFileStream(string fileName)
      {
         var ktx = (IKernelTransaction) TransactionInterop.GetDtcTransaction(Transaction.Current);

         SafeTransactionHandle txHandle;
         ktx.GetHandle(out txHandle);

         SafeFileHandle fileHandle = NativeMethods.CreateFileTransacted(
               fileName, GenericWrite, 0,
               IntPtr.Zero, CreateAlways, FileAttributeNormal,
               IntPtr.Zero,
               txHandle, IntPtr.Zero, IntPtr.Zero);

         return new FileStream(fileHandle, FileAccess.Write);
      }
   }
}

