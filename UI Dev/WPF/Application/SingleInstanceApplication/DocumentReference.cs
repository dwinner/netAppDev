namespace SingleInstanceApplication
{
   // This package contains a reference to a document window and its name.
   // The purpose of this class is to make it easier to display the list
   // of window names in DocumentList through data binding.
   public class DocumentReference
   {
      public DocumentReference(Document document, string name)
      {
         Document = document;
         Name = name;
      }

      public Document Document { get; set; }

      public string Name { get; private set; }
   }
}