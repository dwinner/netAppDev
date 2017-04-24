using AppDev.Sapper.Model.Attributes;

namespace AppDev.Sapper.Model.Enums
{
   /// <summary>
   ///    The status of the game
   /// </summary>
   public enum GameStatus
   {
		/// <summary>
		///    Begin
		/// </summary>
		[GameStatus(Status = "The new game has begun.")]
		Begin,

		/// <summary>
		///    Game goes on
		/// </summary>
		[GameStatus(Status = "The game goes on.")]
		On,

		/// <summary>
		///    Game over, you won
		/// </summary>
		[GameStatus(Status = "Сongratulations! You have opened all the mines.")]
		Victory,

		/// <summary>
		///    Game over, you failed
		/// </summary>
		[GameStatus(Status = "We are sorry. You have blown on a mine and lost. Try again.")]
		Fail
   }
}