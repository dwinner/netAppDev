using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ifElseSwitchLinking
{
   public sealed class DirectoryWalker // NOTE: Рекурсивное связывание блоков с продвижением заверщения
   {
      private readonly TransformManyBlock<string, string> _directoryBrowseBlock;
      private readonly ActionBlock<string> _fileActionBlock;
      private long _directoriesRemaining;

      public DirectoryWalker(Action<string> fileAction)
      {
         _directoryBrowseBlock =
            new TransformManyBlock<string, string>((Func<string, IEnumerable<string>>)(GetFilesInDirectory));
         _fileActionBlock = new ActionBlock<string>(fileAction);
         _directoryBrowseBlock.LinkTo(_directoryBrowseBlock, Directory.Exists);
         _directoryBrowseBlock.LinkTo(_fileActionBlock, new DataflowLinkOptions { PropagateCompletion = true });
      }

      public Task WalkAsync(string path)
      {
         _directoriesRemaining = 1;
         _directoryBrowseBlock.Post(path);
         return _fileActionBlock.Completion;
      }

      private IEnumerable<string> GetFilesInDirectory(string path)
      {
         var dir = new DirectoryInfo(path);
         var subDirectories = dir.GetDirectories().Select(directoryInfo => directoryInfo.FullName).ToArray();
         _directoriesRemaining += subDirectories.Length;
         if (--_directoriesRemaining == 0)
         {
            _directoryBrowseBlock.Complete();
         }

         return dir
            .GetFiles().Select(fileInfo => fileInfo.FullName)
            .Concat(subDirectories);
      }
   }
}