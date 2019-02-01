using System;

namespace GenExceptionSample
{
   [Serializable]
   public sealed class DiskFullExceptionArgs : GenExceptionArgs
   {
      private readonly string _diskPath;

      public string DiskPath
      {
         get { return _diskPath; }
      }

      public override string Message
      {
         get { return _diskPath == null ? base.Message : string.Format("DiskPath={0}", _diskPath); }
      }

      public DiskFullExceptionArgs(string diskPath)
      {
         _diskPath = diskPath;
      }
   }
}