namespace State
{
   public class FullyRentedState : IAutomatState
   {
      private IAutomat _automat;

      public FullyRentedState(IAutomat automat)
      {
         _automat = automat;
      }

      public string GotApplication() => "Sorry, we're fully rented.";

      public string CheckApplication() => "Sorry, we're fully rented.";

      public string RentAppartment() => "Sorry, we're fully rented.";

      public string DispenseKeys() => "Sorry, we're fully rented.";
   }
}