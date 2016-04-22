using System;
using System.Collections.Generic;

namespace LinkedListSample
{
   public class PriorityDocumentManager
   {
      private readonly LinkedList<Document> _documentList;
      private readonly List<LinkedListNode<Document>> _priorityNodes;   // Приоритеты от 0 до 9

      public PriorityDocumentManager()
      {
         _documentList = new LinkedList<Document>();
         _priorityNodes = new List<LinkedListNode<Document>>(10);
         for (int i = 0; i < 10; i++)
         {
            _priorityNodes.Add(new LinkedListNode<Document>(null));
         }
      }

      public void AddDocument(Document document)
      {
         if (document == null)
            throw new ArgumentNullException("document");
         AddDocumentToPriorityNode(document, document.Priority);
      }

      public void DisplayAllNodes()
      {
         foreach (Document document in _documentList)
         {
            Console.WriteLine("priority: {0}, title {1}", document.Priority, document.Title);
         }
      }

      // Возвращает документ с максимальным приоритетом
      // (первый в связном списке)
      public Document GetDocument()
      {
         Document document = _documentList.First.Value;
         _documentList.RemoveFirst();
         return document;
      }

      private void AddDocumentToPriorityNode(Document document, int priority)
      {
         if (priority > 9 || priority < 0)
            throw new ArgumentException("Priority must be between 0 and 9");
         if (_priorityNodes[priority].Value == null)
         {
            if (--priority >= 0)
            {
               AddDocumentToPriorityNode(document, priority);  // Проверить следующий меньший приоритет
            }
            else   // Теперь нет узлов приоритетов с тем же или меньшим приоритетом.
            {      // Добавить документ в конец.
               _documentList.AddLast(document);
               _priorityNodes[document.Priority] = _documentList.Last;
            }
         }
         else   // Узел приоритета существует.
         {
            LinkedListNode<Document> prioNode = _priorityNodes[priority];
            if (priority == document.Priority)   // Узел приоритета с тем же значением приоритета уже существует
            {
               _documentList.AddAfter(prioNode, document);
               _priorityNodes[document.Priority] = prioNode.Next; // Установить узел приоритета в последний документ с тем же значением приоритета.
            }
            else   // Существует только узел приоритета с меньшим значением приоритета.
            {
               // Получить первый узел с меньшим приоритетом.
               LinkedListNode<Document> firstPrioNode = prioNode;
               while (prioNode != null && (firstPrioNode != null
                                           && (firstPrioNode.Previous != null
                                           && firstPrioNode.Previous.Value.Priority == prioNode.Value.Priority)))
               {
                  firstPrioNode = prioNode.Previous;
                  prioNode = firstPrioNode;
               }
               if (firstPrioNode != null)
               {
                  _documentList.AddBefore(firstPrioNode, document);
                  _priorityNodes[document.Priority] = firstPrioNode.Previous;
               }
            }
         }
      }
   }
}