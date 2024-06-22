namespace GameSample;

public abstract class Game(int numberOfPlayers)
{
   protected readonly int NumberOfPlayers = numberOfPlayers;
   protected int CurrentPlayer;

   protected abstract bool HaveWinner { get; }
   protected abstract int WinningPlayer { get; }

   public void Run()
   {
      Start();
      while (!HaveWinner)
      {
         TakeTurn();
      }

      Console.WriteLine($"Player {WinningPlayer} wins.");
   }

   protected abstract void Start();

   protected abstract void TakeTurn();
}