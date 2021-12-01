using System;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Machine;

namespace StateMachineViaEnum
{
   public class Elevator
   {
      private readonly PassiveStateMachine<States, Events> _elevator;

      public Elevator()
      {
         var builder = new StateMachineDefinitionBuilder<States, Events>();

         builder.DefineHierarchyOn(States.Healthy)
            .WithHistoryType(HistoryType.Deep)
            .WithInitialSubState(States.OnFloor)
            .WithSubState(States.Moving);

         builder.DefineHierarchyOn(States.Moving)
            .WithHistoryType(HistoryType.Shallow)
            .WithInitialSubState(States.MovingUp)
            .WithSubState(States.MovingDown);

         builder.DefineHierarchyOn(States.OnFloor)
            .WithHistoryType(HistoryType.None)
            .WithInitialSubState(States.DoorClosed)
            .WithSubState(States.DoorOpen);

         builder.In(States.Healthy)
            .On(Events.Error).Goto(States.Error);

         builder.In(States.Error)
            .On(Events.Reset).Goto(States.Healthy)
            .On(Events.Error);

         builder.In(States.OnFloor)
            .ExecuteOnEntry(AnnounceFloor)
            .ExecuteOnExit(Beep)
            .ExecuteOnExit(Beep) // just beep a second time
            .On(Events.CloseDoor).Goto(States.DoorClosed)
            .On(Events.OpenDoor).Goto(States.DoorOpen)
            .On(Events.GoUp)
            .If(CheckOverload).Goto(States.MovingUp)
            .Otherwise().Execute(AnnounceOverload)
            .On(Events.GoDown)
            .If(CheckOverload).Goto(States.MovingDown)
            .Otherwise().Execute(AnnounceOverload);
         builder.In(States.Moving)
            .On(Events.Stop).Goto(States.OnFloor);

         builder.WithInitialState(States.OnFloor);

         var definition = builder
            .Build();

         _elevator = definition
            .CreatePassiveStateMachine("Elevator");

         _elevator.Start();
      }

      public void GoToUpperLevel()
      {
         _elevator.Fire(Events.CloseDoor);
         _elevator.Fire(Events.GoUp);
         _elevator.Fire(Events.OpenDoor);
      }

      public void GoToLowerLevel()
      {
         _elevator.Fire(Events.CloseDoor);
         _elevator.Fire(Events.GoDown);
         _elevator.Fire(Events.OpenDoor);
      }

      public void Error()
      {
         _elevator.Fire(Events.Error);
      }

      public void Stop()
      {
         _elevator.Fire(Events.Stop);
      }

      public void Reset()
      {
         _elevator.Fire(Events.Reset);
      }

      private void AnnounceFloor()
      {
         Console.WriteLine(nameof(AnnounceFloor));
      }

      private void AnnounceOverload()
      {
         Console.WriteLine(nameof(AnnounceOverload));
      }

      private void Beep()
      {
         Console.WriteLine(nameof(Beep));
      }

      private bool CheckOverload()
      {
         Console.WriteLine(nameof(CheckOverload));
         return false;
      }

      private enum States
      {
         Healthy,
         OnFloor,
         Moving,
         MovingUp,
         MovingDown,
         DoorOpen,
         DoorClosed,
         Error
      }

      private enum Events
      {
         GoUp,
         GoDown,
         OpenDoor,
         CloseDoor,
         Stop,
         Error,
         Reset
      }
   }
}