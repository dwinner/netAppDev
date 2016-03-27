using System;

namespace Mediator
{
   public class ConcreteColleague1 : ColleagueImpl
   {
      public ConcreteColleague1(IMediator mediator)
         : base(mediator)
      {
      }

      public void Send(string message)
      {
         Mediator.Send(message, this);
      }

      public void Notify(string message)
      {
         Console.WriteLine("Colleague1 gets message: {0}", message);
      }
   }
}