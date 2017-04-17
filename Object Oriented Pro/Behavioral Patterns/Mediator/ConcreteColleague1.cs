using static System.Console;

namespace Mediator
{
   public class ConcreteColleague1 : ColleagueImpl
   {
      public ConcreteColleague1(IMediator mediator)
         : base(mediator)
      {
      }

      public void Send(string message) => Mediator.Send(message, this);

      public static void Notify(string message)
         => WriteLine("Colleague1 gets message: {0}", message);
   }
}