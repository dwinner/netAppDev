namespace EventMediator;

internal class Player(string name, Game game)
{
   private int _goalsScored;

   public void Score()
   {
      _goalsScored++;
      var args = new PlayerScoredEventArgs(name, _goalsScored);
      game.Fire(args);
   }
}