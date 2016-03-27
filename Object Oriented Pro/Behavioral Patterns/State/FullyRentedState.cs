namespace State
{
   public class FullyRentedState : IAutomatState
   {
      private IAutomat _automat;

      public FullyRentedState(IAutomat automat)
      {
         _automat = automat;
      }

      public string GotApplication()
      {
         return "Sorry, we're fully rented.";
      }

      public string CheckApplication()
      {
         return "Sorry, we're fully rented.";
      }

      public string RentAppartment()
      {
         return "Sorry, we're fully rented.";
      }

      public string DispenseKeys()
      {
         return "Sorry, we're fully rented.";
      }
   }
}