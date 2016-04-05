/**
 * Отношение таблица на иерархию
 */

using System;
using System.Data.Objects;

namespace PaymentsDemo
{
   internal class Program
   {
      private static void Main()
      {
         using (var data = new PaymentsEntities())
         {
            foreach (Payment p in data.Payments)
            {
               Console.WriteLine("{0}, {1} - {2:C}", p.GetType().Name, p.Name, p.Amount);
            }
         }

         using (var data = new PaymentsEntities())
         {
            ObjectQuery<CreditcardPayment> q = data.Payments.OfType<CreditcardPayment>();
            Console.WriteLine(q.ToTraceString());
            Console.WriteLine();
            foreach (CreditcardPayment p in data.Payments.OfType<CreditcardPayment>())
            {
               Console.WriteLine("{0} {1} {2}", p.Name, p.Amount, p.CreditCard);
            }
         }
      }
   }
}