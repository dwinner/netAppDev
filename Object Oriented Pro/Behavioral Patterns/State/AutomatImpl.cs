using System;

namespace State
{
   public class AutomatImpl : IAutomat
   {
      private IAutomatState _state;

      public IAutomatState WaitingState { get; private set; }

      public IAutomatState ApplicationState { get; private set; }

      public IAutomatState ApartmentRentedState { get; private set; }

      public IAutomatState FullyRentedState { get; private set; }

      public IAutomatState State
      {
         get { return _state; }
         set { _state = value; }
      }

      public int Count { get; set; }

      public AutomatImpl(int n)
      {
         Count = n;
         WaitingState = new WaitingState(this);
         ApplicationState = new GotApplicationState(this);
         ApartmentRentedState = new ApartmentRentedState(this);
         FullyRentedState = new FullyRentedState(this);
         _state = WaitingState;
      }

      public void GotApplication()
      {
         Console.WriteLine(_state.GotApplication());
      }

      public void CheckApplication()
      {
         Console.WriteLine(_state.CheckApplication());
      }

      public void RentApartment()
      {
         Console.WriteLine(_state.RentAppartment());
         Console.WriteLine(_state.DispenseKeys());
      }
   }
}