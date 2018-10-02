namespace CommentRemover.MsBuildTaskApp
{
   internal class Program
   {
      private static void Main()
      {
      }
   }

   public class Commentary
   {
      /// <summary>
      ///    Adds commentary to a piece of text.
      /// </summary>
      /// <param name="text">The text to process.</param>
      public int Process(string text)
      {
         return text?.Length ?? 0;
      }
   }
}