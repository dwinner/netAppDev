namespace State
{
   public interface IAutomat
   {
      void GotApplication();

      void CheckApplication();

      void RentApartment();

      IAutomatState State { get; set; }

      IAutomatState WaitingState { get; }

      IAutomatState ApplicationState { get; }

      IAutomatState ApartmentRentedState { get; }

      IAutomatState FullyRentedState { get; }

      int Count { get; set; }
   }
}