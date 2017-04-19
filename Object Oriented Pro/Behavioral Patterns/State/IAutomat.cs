namespace State
{
   public interface IAutomat
   {
      IAutomatState State { set; }

      IAutomatState WaitingState { get; }

      IAutomatState ApplicationState { get; }

      IAutomatState ApartmentRentedState { get; }

      IAutomatState FullyRentedState { get; }

      int Count { get; set; }

      void GotApplication();

      void CheckApplication();

      void RentApartment();
   }
}