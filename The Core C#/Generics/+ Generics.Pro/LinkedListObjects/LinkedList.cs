using System.Collections;

namespace LinkedListObjects
{
   /// <summary>
   /// Связный список для объектов
   /// </summary>
   public class LinkedList : IEnumerable
   {
      public LinkedListNode First { get; private set; }

      public LinkedListNode Last { get; private set; }

      public LinkedListNode AddLast(object node)
      {
         var newNode = new LinkedListNode(node);
         if (First == null)
         {
            Last = First = newNode;
         }
         else
         {
            LinkedListNode previous = Last;
            Last.Next = newNode;
            Last = newNode;
            Last.Prev = previous;
         }

         return newNode;
      }

      public IEnumerator GetEnumerator()
      {
         LinkedListNode current = First;
         while (current != null)
         {
            yield return current.Value;
            current = current.Next;
         }
      }
   }
}