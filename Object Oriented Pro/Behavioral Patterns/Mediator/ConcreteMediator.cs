namespace Mediator
{
   public class ConcreteMediator : IMediator
   {
      public ConcreteColleague1 FirstColleague { get; set; }

      public ConcreteColleague2 SecondColleague { get; set; }

      public ConcreteMediator(ConcreteColleague1 firstColleague, ConcreteColleague2 secondColleague)
      {
         FirstColleague = firstColleague;
         SecondColleague = secondColleague;
      }

      public ConcreteMediator()
      {         
      }

      public void Send(string message, IColleague colleague)
      {
         if (colleague == FirstColleague)
         {
            SecondColleague.Notify(message);
         }
         else
         {
            FirstColleague.Notify(message);
         }
      }
   }
}