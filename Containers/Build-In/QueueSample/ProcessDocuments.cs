using System;
using System.Threading;
using System.Threading.Tasks;

namespace QueueSample
{
   public class ProcessDocuments
   {
      public static void Start(IDocumentManager<Document> documentManager)
      {
         Task.Factory.StartNew(new ProcessDocuments(documentManager).Run);
      }

      private readonly IDocumentManager<Document> _documentManager;

      protected ProcessDocuments(IDocumentManager<Document> documentManager)
      {
         if (documentManager == null)
            throw new ArgumentNullException("documentManager");
         _documentManager = documentManager;
      }

      protected void Run()
      {
         while (true)
         {
            if (_documentManager.IsDocumentAvailable)
            {
               Document document = _documentManager.GetDocument();
               Console.WriteLine("Processing document {0}", document.Title);
            }
            Thread.Yield();
         }
         // ReSharper disable once FunctionNeverReturns
      }
   }
}