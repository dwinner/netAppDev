namespace BrokerChain;

public abstract class CreatureModifier : IDisposable
{
   protected Creature Creature;
   protected Game Game;

   protected CreatureModifier(Game game, Creature creature)
   {
      Game = game;
      Creature = creature;
      game.Queries += Handle;
   }

   public void Dispose()
   {
      Game.Queries -= Handle;
   }

   protected abstract void Handle(object? sender, Query query);
}