using System;
using System.IO;
using static CommentRemover.WorkspaceCommentRemover;

namespace CommentRemover.ConsoleApp
{
   internal static class CommentRemoverApp
   {
      private static void Main(string[] args)
      {
         if (args.Length == 0 || args[0] == string.Empty)
         {
            Console.WriteLine("Usage: CommentRemover.ConsoleApp \"solution or project file\"");
         }

         var file = args[0];

         if (!File.Exists(file))
         {
            Console.WriteLine($"File {file} does not exist.");
         }
         else
         {
            var extension = Path.GetExtension(file);

            switch (extension)
            {
               case ".sln":
                  RemoveCommentsFromSolutionAsync(file).Wait();
                  break;
               case ".csproj":
                  RemoveCommentsFromProjectAsync(file).Wait();
                  break;
               default:
                  Console.WriteLine("Only .sln and .csproj files are supported.");
                  break;
            }
         }
      }
   }
}