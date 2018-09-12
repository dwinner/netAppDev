namespace InjectedCtorParam
{
	public sealed class TicTacToe : IGame
	{
		private string _status;

		public TicTacToe()
		{
			Name = "Tic Tac Toe";
			CurrentPlayers = 0;
			MinPlayers = 2;
			MaxPlayers = 2;
			_status = "No active games";
		}

		public string Name { get; }

		public int CurrentPlayers { get; private set; }

		public int MinPlayers { get; }

		public int MaxPlayers { get; }

		public void AddPlayer() => CurrentPlayers++;

		public void RemovePlayer() => CurrentPlayers--;

		public void Play() => _status = CurrentPlayers > MaxPlayers || CurrentPlayers < MinPlayers
			? $"{Name}: It's not possible to play with {CurrentPlayers} players."
			: $"{Name}: Playing with {CurrentPlayers} players.";

		public string GetResult() => _status;
	}
}