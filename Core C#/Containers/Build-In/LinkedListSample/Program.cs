/**
 * Связные списки
 */

namespace LinkedListSample
{
   internal static class Program
   {
      static void Main()
      {
         var priorityDocumentManager = new PriorityDocumentManager();
         priorityDocumentManager.AddDocument(new Document("one", "Sample", 8));
         priorityDocumentManager.AddDocument(new Document("two", "Sample", 3));
         priorityDocumentManager.AddDocument(new Document("three", "Sample", 4));
         priorityDocumentManager.AddDocument(new Document("four", "Sample", 8));
         priorityDocumentManager.AddDocument(new Document("five", "Sample", 1));
         priorityDocumentManager.AddDocument(new Document("six", "Sample", 9));
         priorityDocumentManager.AddDocument(new Document("seven", "Sample", 1));
         priorityDocumentManager.AddDocument(new Document("eight", "Sample", 1));

         priorityDocumentManager.DisplayAllNodes();
      }
   }
}
