namespace DocumentManager
{
   public class Document : IDocument
   {
      public string Title { get; set; }

      public string Content { get; set; }

      public Document()
      {
      }

      public Document(string title, string content)
      {
         Title = title;
         Content = content;
      }
   }
}