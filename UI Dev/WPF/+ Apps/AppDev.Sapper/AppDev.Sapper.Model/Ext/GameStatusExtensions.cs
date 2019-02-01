using System;
using System.ComponentModel;
using System.Linq;
using AppDev.Sapper.Model.Attributes;
using AppDev.Sapper.Model.Enums;

namespace AppDev.Sapper.Model.Ext
{
	/// <summary>
	///    Game status extensions
	/// </summary>
	public static class GameStatusExtensions
	{
		/// <summary>
		///    Get the game status string
		/// </summary>
		/// <param name="this">Extensible type</param>
		/// <returns>The game status</returns>
		public static string GetGameStatus(this GameStatus @this)
		{
			if (!Enum.IsDefined(typeof(GameStatus), @this))
				throw new InvalidEnumArgumentException(nameof(@this), (int) @this, typeof(GameStatus));

			return @this.GetType()
				.GetField(@this.ToString())
				.GetCustomAttributes<GameStatusAttribute>()
				.FirstOrDefault()
				?.Status;
		}
	}
}