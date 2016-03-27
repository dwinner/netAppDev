namespace State
{
   public class WaitingState : IAutomatState
   {
      private readonly IAutomat _automat;

      public WaitingState(IAutomat automat)
      {
         _automat = automat;
      }

      public string GotApplication()
      {
         _automat.State = _automat.ApplicationState;
         return "Thanks for the application.";
      }

      public string CheckApplication()
      {
         return "You have to submit an application.";
      }

      public string RentAppartment()
      {
         return "You have to submit an application.";
      }

      public string DispenseKeys()
      {
         return "You have to submit an application.";
      }
   }
}