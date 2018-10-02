namespace State
{
   public class ApartmentRentedState : IAutomatState
   {
      private readonly IAutomat _automat;

      public ApartmentRentedState(IAutomat automat)
      {
         _automat = automat;
      }

      public string GotApplication()
         => "Hang on, we'ra renting you an apartmeny.";

      public string CheckApplication()
         => "Hang on, we'ra renting you an apartmeny.";

      public string RentAppartment()
      {
         _automat.Count = _automat.Count - 1;
         return "Renting you an apartment....";
      }

      public string DispenseKeys()
      {
         _automat.State = _automat.Count <= 0
            ? _automat.FullyRentedState
            : _automat.WaitingState;
         return "Here are your keys!";
      }
   }
}