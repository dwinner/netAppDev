namespace GameSample;

public class Chess() : Game(2)
{
   private const int MaxTurns = 10;
   private int _turn = 1;

   protected override bool HaveWinner => _turn == MaxTurns;

   protected override int WinningPlayer => CurrentPlayer;

   protected override void Start()
   {
      Console.WriteLine($"Starting a game of chess with {NumberOfPlayers} players.");
   }

   protected override void TakeTurn()
   {
      Console.WriteLine($"Turn {_turn++} taken by player {CurrentPlayer}.");
      CurrentPlayer = (CurrentPlayer + 1) % NumberOfPlayers;
   }
}