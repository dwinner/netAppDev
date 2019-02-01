/**
 * Обеспечение слабой связи между объектами
 */

using System;

namespace Mediator
{
   internal static class Program
   {
      private static void Main()
      {
         var mediator = new ConcreteMediator();
         mediator.FirstColleague = new ConcreteColleague1(mediator);
         mediator.SecondColleague = new ConcreteColleague2(mediator);

         mediator.FirstColleague.Send("How Are you?");
         mediator.SecondColleague.Send("Fine, thanks");

         Console.ReadLine();
      }
   }
}