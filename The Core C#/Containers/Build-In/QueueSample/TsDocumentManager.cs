namespace QueueSample
{
   public class TsDocumentManager : IDocumentManager<Document>
   {
      private readonly IDocumentManager<Document> _documentManager;

      public TsDocumentManager(IDocumentManager<Document> documentManager)
      {
         _documentManager = documentManager;
      }

      public void AddDocument(Document document)
      {
         lock (this)
         {
            _documentManager.AddDocument(document);
         }
      }

      public Document GetDocument()
      {
         Document document;
         lock (this)
         {
            document = _documentManager.GetDocument();
         }
         return document;
      }

      public bool IsDocumentAvailable
      {
         get
         {
            return _documentManager.IsDocumentAvailable;
         }
      }
   }
}