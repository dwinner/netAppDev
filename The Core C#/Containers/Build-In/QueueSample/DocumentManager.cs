using System.Collections.Generic;

namespace QueueSample
{
   public class DocumentManager : IDocumentManager<Document>
   {
      private const int DefaultQueueCapacity = 0x100;

      private readonly Queue<Document> _documentQueue;

      public DocumentManager()
      {
         _documentQueue = new Queue<Document>(DefaultQueueCapacity);
      }

      public void AddDocument(Document document)
      {
         _documentQueue.Enqueue(document);
      }

      public Document GetDocument()
      {
         return _documentQueue.Dequeue();
      }

      public bool IsDocumentAvailable
      {
         get
         {
            return _documentQueue.Count > 0;
         }
      }
   }
}