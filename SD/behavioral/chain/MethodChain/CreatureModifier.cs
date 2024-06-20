namespace MethodChain;

public class CreatureModifier(Creature creature)
{
   protected Creature Creature = creature;
   protected CreatureModifier? Next;

   public void Add(CreatureModifier aCreatureModifier)
   {
      if (Next != null)
      {
         Next.Add(aCreatureModifier);
      }
      else
      {
         Next = aCreatureModifier;
      }
   }

   public virtual void Handle() => Next?.Handle();
}