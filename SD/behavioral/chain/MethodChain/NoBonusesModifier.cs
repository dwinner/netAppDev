namespace MethodChain;

public class NoBonusesModifier(Creature creature) : CreatureModifier(creature)
{
   public override void Handle()
   {
      // nothing
      Console.WriteLine("No bonuses for you!");
   }
}