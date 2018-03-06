/**
 * Реализация очереди с приоритетами
 */

using System;

namespace PriorityQueueDemo
{
   class Program
   {
      enum Priority  // Сделаем порядок приоритетов обратным
      {         
         High,
         Middle,
         Low
      };

      static void Main()
      {
         PriorityQueue<Priority, string> priQueue = new PriorityQueue<Priority, string>();

         priQueue.Enqueue(Priority.Low, "Task 1");
         priQueue.Enqueue(Priority.Low, "Task 2");
         priQueue.Enqueue(Priority.Middle, "Task 3");
         priQueue.Enqueue(Priority.Middle, "Task 4");
         priQueue.Enqueue(Priority.High, "Task 5");
         priQueue.Enqueue(Priority.Low, "Task 6");
         priQueue.Enqueue(Priority.High, "Task 7");

         // Ожидаемый порядок: 5,7,3,4,1,2,6
         Console.WriteLine("Enumerate all:");
         foreach (string item in priQueue)
         {
            Console.WriteLine(item);
         }

         Console.WriteLine();
         Console.WriteLine("Dequeue all");
         while (priQueue.Count > 0)
         {
            string s = priQueue.Dequeue();
            Console.WriteLine(s);
         }

         Console.ReadKey();
      }
   }
}
