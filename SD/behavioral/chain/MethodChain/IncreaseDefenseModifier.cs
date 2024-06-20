namespace MethodChain;

public class IncreaseDefenseModifier(Creature creature) : CreatureModifier(creature)
{
   public override void Handle()
   {
      if (Creature.Attack <= 2)
      {
         Console.WriteLine($"Increasing {Creature.Name}'s defense");
         Creature.Defense++;
      }

      base.Handle();
   }
}