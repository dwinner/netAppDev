using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace ifElseSwitchLinking
{
   public sealed class DirectoryWalkerLite   // NOTE: Рекурсивное связывание блоков
   {
      private readonly TransformManyBlock<string, string> _directoryBrowseBlock;

      public DirectoryWalkerLite(Action<string> fileAction)
      {
         _directoryBrowseBlock =
            new TransformManyBlock<string, string>((Func<string, IEnumerable<string>>) (GetFilesInDirectory));
         var fileActionBlock = new ActionBlock<string>(fileAction);

         _directoryBrowseBlock.LinkTo(_directoryBrowseBlock, Directory.Exists);
         _directoryBrowseBlock.LinkTo(fileActionBlock);
      }

      public void Walk(string path)
      {
         _directoryBrowseBlock.Post(path);
      }

      private static IEnumerable<string> GetFilesInDirectory(string path)
      {
         var dir = new DirectoryInfo(path);
         return dir.EnumerateFileSystemInfos().Select(fileSystemInfo => fileSystemInfo.FullName);
      }
   }
}