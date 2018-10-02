using System;

namespace State
{
   public class GotApplicationState : IAutomatState
   {
      private readonly IAutomat _automat;
      private readonly Random _random;

      public GotApplicationState(IAutomat automat)
      {
         _automat = automat;
         _random = new Random(DateTime.Now.Millisecond);
      }

      public string GotApplication() => "We already got your application.";

      public string CheckApplication()
      {
         var yesNo = _random.Next() % 10;

         if (yesNo > 4 && _automat.Count > 0)
         {
            _automat.State = _automat.ApartmentRentedState;
            return "Congratulations, you were approved.";
         }

         _automat.State = _automat.WaitingState;
         return "Sorry, you were not approved.";
      }

      public string RentAppartment() => "You must have your application checked.";

      public string DispenseKeys() => "You must have your application checked.";
   }
}