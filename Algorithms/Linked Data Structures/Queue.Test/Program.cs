using System;
using LinkedList.Lib;
using QueueInheritance.Lib;

namespace Queue.Test
{
   internal static class Program
   {
      private static void Main()
      {
         var queue = new QueueInheritance<object>();

         // create objects to store in the queue
         const char aCharacter = '$';
         const int anInteger = 34567;
         const string aString = "hello";

         // use method Enqueue to add items to queue
         queue.Enqueue(true);
         Console.WriteLine(queue);

         queue.Enqueue(aCharacter);
         Console.WriteLine(queue);

         queue.Enqueue(anInteger);
         Console.WriteLine(queue);

         queue.Enqueue(aString);
         Console.WriteLine(queue);

         // remove items from queue
         try
         {
            while (true)
            {
               var removedObject = queue.Dequeue();
               Console.WriteLine($"{removedObject} dequeued");
               Console.WriteLine(queue);
            }
         }
         catch (EmptyListException emptyListException)
         {
            // if exception occurs, write stack trace
            Console.Error.WriteLine(emptyListException.StackTrace);
         }
      }
   }
}