using System;
using System.Collections.Generic;

namespace DocumentManager
{
   public class DocumentManager<TDocument>
      where TDocument : IDocument
   {
      private readonly Queue<TDocument> _documentQueue = new Queue<TDocument>();

      public bool IsDocumentAvailable
      {
         get
         {
            return _documentQueue.Count > default(int);
         }
      }

      public void AddDocument(TDocument document)
      {
         lock (this)
         {
            _documentQueue.Enqueue(document);
         }
      }

      public TDocument GetDocument()
      {
         TDocument document/* = default(TDocument)*/;
         lock (this)
         {
            document = _documentQueue.Dequeue();
         }
         return document;
      }

      public void DisplayAllDocuments()
      {
         foreach (TDocument document in _documentQueue)
         {
            Console.WriteLine(document.Title);
         }
      }
   }
}