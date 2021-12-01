using System;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Machine;

namespace StateMachineViaEnum
{
   public class SimpleStateMachine
   {
      private readonly PassiveStateMachine<States, Events> _machine;

      public SimpleStateMachine()
      {
         var builder = new StateMachineDefinitionBuilder<States, Events>();

         builder
            .In(States.Off)
            .On(Events.TurnOn)
            .Goto(States.On)
            .Execute(SayHello);

         builder
            .In(States.On)
            .On(Events.TurnOff)
            .Goto(States.Off)
            .Execute(SayBye);

         builder
            .WithInitialState(States.Off);

         _machine = builder
            .Build()
            .CreatePassiveStateMachine();

         _machine.Start();
      }

      public void TurnOn()
      {
         _machine
            .Fire(
               Events.TurnOn);
      }

      public void TurnOff()
      {
         _machine
            .Fire(
               Events.TurnOff);
      }

      private void SayHello()
      {
         Console.WriteLine("hello");
      }

      private void SayBye()
      {
         Console.WriteLine("bye");
      }

      private enum States
      {
         On,
         Off
      }

      private enum Events
      {
         TurnOn,
         TurnOff
      }
   }
}