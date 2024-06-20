using static System.Console;

namespace BrokerChain;

internal class Program
{
   private static void Main()
   {
      var game = new Game();
      var goblin = new Creature(game, "Strong Goblin", 2, 2);
      WriteLine(goblin);

      using (new DoubleAttackModifier(game, goblin))
      {
         WriteLine(goblin);
         using (new IncreaseDefenseModifier(game, goblin))
         {
            WriteLine(goblin);
         }
      }

      WriteLine(goblin);
   }
}