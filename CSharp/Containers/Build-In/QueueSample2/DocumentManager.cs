using System.Collections.Generic;

namespace QueueSample
{
   public class DocumentManager
   {
      private readonly Queue<Document> _documentQueue = new();
      private readonly object _syncQueue = new();

      public bool IsDocumentAvailable
      {
         get
         {
            lock (_syncQueue)
            {
               return _documentQueue is { Count: > 0 };
            }
         }
      }

      public int AddDocument(Document doc)
      {
         lock (_syncQueue)
         {
            _documentQueue.Enqueue(doc);
            return _documentQueue.Count;
         }
      }

      public Document GetDocument()
      {
         lock (_syncQueue)
         {
            return _documentQueue.Dequeue();
         }
      }
   }
}