namespace InjectedCtorParam
{
	public sealed class Table
	{
		private readonly IGame _game;

		public Table(IGame game)
		{
			_game = game;
		}

		public string GetGameStatus() => _game.GetResult();

		public void AddPlayer() => _game.AddPlayer();

		public void RemovePlayer() => _game.RemovePlayer();

		public void Play() => _game.Play();
	}
}