namespace MethodChain;

public class DoubleAttackModifier(Creature creature) : CreatureModifier(creature)
{
   public override void Handle()
   {
      Console.WriteLine($"Doubling {Creature.Name}'s attack");
      Creature.Attack *= 2;
      base.Handle();
   }
}