namespace BrokerChain;

public class Creature(Game game, string name, int attack, int defense)
{
   private readonly Game _game = game ?? throw new ArgumentNullException(nameof(game));
   public string Name = name ?? throw new ArgumentNullException(nameof(name));

   public int Attack
   {
      get
      {
         var query = new Query(Name, Query.Argument.Attack, attack);
         _game.PerformQuery(this, query);

         return query.Value;
      }
   }

   public int Defense
   {
      get
      {
         var query = new Query(Name, Query.Argument.Defense, defense);
         _game.PerformQuery(this, query);

         return query.Value;
      }
   }

   public override string ToString() // no game
      => $"{nameof(Name)}: {Name}, {nameof(attack)}: {Attack}, {nameof(defense)}: {Defense}";
}