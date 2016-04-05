/**
 * Изменение порядка элементов связного списка на обратный
 */

using System;

namespace ReverseLinkedList
{
   internal static class Program
   {
      private static void Main()
      {
         var list = BuildList(10);

         PrintList(list);
         ReverseList(ref list);
         PrintList(list);

         Console.ReadKey();
      }

      private static Node<int> BuildList(int maxValue)
      {
         var head = new Node<int>(0);
         var tail = head;
         for (var i = 1; i <= maxValue; i++)
         {
            tail.Next = new Node<int>(i);
            tail = tail.Next;
         }
         return head;
      }

      private static void PrintList<T>(Node<T> head)
      {
         var current = head;
         while (current != null)
         {
            Console.Write("{0} ", current.Value);
            current = current.Next;
         }
         Console.WriteLine();
      }

      private static void ReverseList<T>(ref Node<T> head)
      {
         var tail = head;
         var nextNode = head.Next; // Перейти к следующему узлу         
         tail.Next = null; // Сделать прежний начальный элемент конечным
         while (nextNode != null)
         {
            var n = nextNode.Next; // Перейти к следующему узлу            
            nextNode.Next = tail; // Сделать его текущим конечным            
            tail = nextNode; // Переустановить конец списка
            nextNode = n;
         }
         head = tail; // Новый начальный элемент там, где конечный
      }
   }
}