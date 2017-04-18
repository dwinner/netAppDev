using System;

namespace State
{
   public class AutomatImpl : IAutomat
   {
      public AutomatImpl(int n)
      {
         Count = n;
         WaitingState = new WaitingState(this);
         ApplicationState = new GotApplicationState(this);
         ApartmentRentedState = new ApartmentRentedState(this);
         FullyRentedState = new FullyRentedState(this);
         State = WaitingState;
      }

      public IAutomatState WaitingState { get; }

      public IAutomatState ApplicationState { get; }

      public IAutomatState ApartmentRentedState { get; }

      public IAutomatState FullyRentedState { get; }

      public IAutomatState State { private get; set; }

      public int Count { get; set; }

      public void GotApplication() => Console.WriteLine(State.GotApplication());

      public void CheckApplication() => Console.WriteLine(State.CheckApplication());

      public void RentApartment()
      {
         Console.WriteLine(State.RentAppartment());
         Console.WriteLine(State.DispenseKeys());
      }
   }
}