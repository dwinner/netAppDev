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

      public string CheckApplication() => "You have to submit an application.";

      public string RentAppartment() => "You have to submit an application.";

      public string DispenseKeys() => "You have to submit an application.";
   }
}