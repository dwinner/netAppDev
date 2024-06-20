using static System.Console;

namespace MethodChain;

internal class Program
{
   private static void Main()
   {
      var goblin = new Creature("Goblin", 1, 1);
      WriteLine(goblin);

      var root = new CreatureModifier(goblin);

      //root.Add(new NoBonusesModifier(goblin));

      root.Add(new DoubleAttackModifier(goblin));
      root.Add(new DoubleAttackModifier(goblin));

      root.Add(new IncreaseDefenseModifier(goblin));

      // eventually...
      root.Handle();
      WriteLine(goblin);
   }
}