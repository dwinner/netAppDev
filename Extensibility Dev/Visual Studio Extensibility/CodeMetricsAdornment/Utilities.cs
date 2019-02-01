using System;
using System.IO;
using Microsoft.VisualStudio.Text.Editor;

namespace CodeMetricsAdornment
{
   internal static class Utilities
   {
      internal static int CountLoc(ITextView view)
      {
         var code = view.TextSnapshot.GetText();

         int count = 1, start = 0;
         while ((start = code.IndexOf(Environment.NewLine, start, StringComparison.Ordinal)) != -1) // BUG?!
         {
            count++;
            start++;
         }

         return count;
      }

      internal static int CountWhitespaceLines(ITextView view)
      {
         var code = view.TextSnapshot.GetText();
         var count = 0;

         using (var reader = new StringReader(code))
         {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
               if (line.Trim() == string.Empty)
               {
                  count++;
               }
            }

            return count;
         }
      }

      internal static int CountCommentLines(ITextView view)
      {
         var code = view.TextSnapshot.GetText();
         var count = 0;

         using (var reader = new StringReader(code))
         {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
               if (line.TrimStart().StartsWith("//"))
               {
                  count++;
               }
            }

            return count;
         }
      }
   }
}