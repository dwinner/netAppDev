/**
 * Основные возможности обобщенных классов
 */

using System;

namespace DocumentManager
{
   static class Program
   {
      static void Main()
      {
         var documentManager = new DocumentManager<Document>();
         documentManager.AddDocument(new Document("Title A", "Sample A"));
         documentManager.AddDocument(new Document("Title B", "Sample B"));

         documentManager.DisplayAllDocuments();

         if (documentManager.IsDocumentAvailable)
         {
            Document document = documentManager.GetDocument();
            Console.WriteLine(document.Content);
         }

         Console.ReadKey();
      }
   }
}
