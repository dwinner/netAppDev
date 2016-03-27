using System;

namespace Mediator
{
   public class ConcreteColleague2 : ColleagueImpl
   {
      public ConcreteColleague2(IMediator mediator)
         : base(mediator)
      {
      }

      public void Send(string message)
      {
         Mediator.Send(message, this);
      }

      public void Notify(string message)
      {
         Console.WriteLine("Colleague2 gets message: {0}", message);
      }
   }
}