// State machine implementation via enum

namespace StateMachineViaEnum
{
   internal static class Program
   {
      private static void Main()
      {
         var simpleSm = new SimpleStateMachine();
         simpleSm.TurnOn();
         simpleSm.TurnOff();

         var elevator = new Elevator();
         elevator.GoToUpperLevel();
         elevator.Stop();
         elevator.Reset();
         elevator.GoToLowerLevel();
         elevator.Error();
      }
   }
}