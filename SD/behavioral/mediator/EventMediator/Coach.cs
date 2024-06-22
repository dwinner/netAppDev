namespace EventMediator;

internal class Coach
{
   private readonly Game _game;

   public Coach(Game game)
   {
      _game = game;

      // celebrate if player has scored <3 goals
      _game.Events += (_, args) =>
      {
         if (args is PlayerScoredEventArgs { GoalsScoredSoFar: < 3 } scored)
         {
            Console.WriteLine($"coach says: well done, {scored.PlayerName}");
         }
      };
   }
}