/**
 * Транзакционные ресурсы: превращение нетранзакционных классов в транзакционные
 */

using System;
using System.Transactions;
using DataLibrary;
using Utilities;

namespace CustomResource
{
   class Program
   {
      static void Main()
      {
         var intVal = new Transactional<int>(1);
         var student = new Transactional<Student>(new Student())
         {
            Value =
            {
               FirstName = "Andrew",
               LastName = "Wilson"
            }
         };

         // Перед транзакцией
         Console.WriteLine("Before the transaction, value: {0}", intVal.Value);
         Console.WriteLine("Before the transaction, student: {0}", student.Value);

         using (var scope = new TransactionScope())
         {
            intVal.Value = 2;

            // Внутри транзакции
            Console.WriteLine("Inside transaction, value: {0}", intVal.Value);
            student.Value.FirstName = "Ten";
            student.Value.LastName = "SixtyNine";
            if (!TxUtilities.AbortTransaction())
            {
               scope.Complete();
            }
         }

         // Вне транзакции
         Console.WriteLine("Outside of transaction, value: {0}", intVal.Value);
         Console.WriteLine("Outside of transaction, student: {0}", student.Value);
      }
   }
}
