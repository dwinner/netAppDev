using System.Diagnostics;
using Microsoft.Build.Framework;
using MsbuildTask = Microsoft.Build.Utilities.Task;

namespace CommentRemover.MsBuildTask
{
   public class CommentRemoverTask : MsbuildTask
   {
      [Required]
      public string ProjectFilePath { get; set; }

      public override bool Execute()
      {
         Log.LogMessage(
            $"Removing comments for project {ProjectFilePath}...");
         var stopwatch = Stopwatch.StartNew();
         WorkspaceCommentRemover.RemoveCommentsFromProjectAsync(ProjectFilePath).Wait();
         stopwatch.Stop();
         Log.LogMessage(
            $"Removing comments for project {ProjectFilePath} complete - total time: {stopwatch.Elapsed}");

         return true;
      }
   }
}