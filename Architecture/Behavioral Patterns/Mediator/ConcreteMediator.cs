namespace Mediator
{
   public class ConcreteMediator : IMediator
   {
      public ConcreteColleague1 FirstColleague { get; set; }

      public ConcreteColleague2 SecondColleague { get; set; }

      public void Send(string message, IColleague colleague)
      {
         if (colleague == FirstColleague)
         {
            ConcreteColleague2.Notify(message);
         }
         else
         {
            ConcreteColleague1.Notify(message);
         }
      }
   }
}