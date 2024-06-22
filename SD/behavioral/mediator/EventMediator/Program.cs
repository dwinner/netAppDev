namespace EventMediator;

internal class Program
{
   private static void Main()
   {
      var game = new Game();
      var player = new Player("Sam", game);
      var coach = new Coach(game);

      player.Score(); // coach says: well done, Sam
      player.Score(); // coach says: well done, Sam
      player.Score(); // ignored by coach
   }
}