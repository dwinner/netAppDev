namespace EventMediator;

internal class PlayerScoredEventArgs(string playerName, int goalsScoredSoFar) : GameEventArgs
{
   public int GoalsScoredSoFar { get; } = goalsScoredSoFar;

   public string PlayerName { get; } = playerName;

   public override void Print()
   {
      Console.WriteLine($"{PlayerName} has scored! " +
                        $"(their {GoalsScoredSoFar} goal)");
   }
}