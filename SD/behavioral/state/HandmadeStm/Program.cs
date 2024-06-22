namespace HandmadeStm;

internal static class Program
{
   private static readonly Dictionary<State, List<(Trigger, State)>> StmRules = new()
   {
      [State.OffHook] = [(Trigger.CallDialed, State.Connecting)],
      [State.Connecting] =
      [
         (Trigger.HungUp, State.OnHook),
         (Trigger.CallConnected, State.Connected)
      ],
      [State.Connected] =
      [
         (Trigger.LeftMessage, State.OnHook),
         (Trigger.HungUp, State.OnHook),
         (Trigger.PlacedOnHold, State.OnHold)
      ],
      [State.OnHold] =
      [
         (Trigger.TakenOffHold, State.Connected),
         (Trigger.HungUp, State.OnHook)
      ]
   };

   private static void Main()
   {
      State state = State.OffHook, exitState = State.OnHook;
      do
      {
         Console.WriteLine($"The phone is currently {state}");
         Console.WriteLine("Select a trigger:");

         for (var i = 0; i < StmRules[state].Count; i++)
         {
            var (t, _) = StmRules[state][i];
            Console.WriteLine($"{i}. {t}");
         }

         var input = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

         var (_, s) = StmRules[state][input];
         state = s;
      } while (state != exitState);

      Console.WriteLine("We are done using the phone.");
   }
}