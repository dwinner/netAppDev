/**
 * Очереди
 */

using System;
using System.Threading;

namespace QueueSample
{
   internal static class Program
   {
      static void Main()
      {
         IDocumentManager<Document> documentManager = new TsDocumentManager(new DocumentManager());
         ProcessDocuments.Start(documentManager);

         // Создать документы и добавить их в DocumentManager
         for (int i = 0; i < 1000; i++)
         {
            var document = new Document(string.Format("Doc {0}", i), "content");
            documentManager.AddDocument(document);
            Console.WriteLine("Added document {0}", document.Title);
            Thread.Yield();
         }

         Console.ReadLine();
      }
   }
}
