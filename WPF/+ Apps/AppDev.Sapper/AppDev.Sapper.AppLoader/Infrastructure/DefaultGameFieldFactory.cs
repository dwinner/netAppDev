using System;
using AppDev.Sapper.Model.Models;
using JetBrains.Annotations;

namespace AppDev.Sapper.AppLoader.Infrastructure
{
	/// <summary>
	///    Default gamefield creation impl.
	/// </summary>
	[UsedImplicitly]
	public sealed class DefaultGameFieldFactory : IGameFieldFactory
	{		
		/// <inheritdoc />
		public MinefieldCell[,] CreateNewMinefield(int minefieldSize, int mineCount)
			=> CreateNewGameField(minefieldSize, mineCount);

		private static MinefieldCell[,] CreateNewGameField(int mineFieldSize, int mineCount)
		{
			var newGameField = new MinefieldCell[mineFieldSize, mineFieldSize];
			for (var row = 0; row < mineFieldSize; row++)
			for (var col = 0; col < mineFieldSize; col++)
				newGameField[row, col] = new MinefieldCell(row, col);

			var setMineCount = 0;
			var random = new Random();

			// Place mines
			do
			{
				var row = random.Next(mineFieldSize);
				var col = random.Next(mineFieldSize);
				if (newGameField[row, col].HasMine)
					continue;

				newGameField[row, col].HasMine = true;
				if (row != 0 && col != 0)
					newGameField[row - 1, col - 1].NeibourMinesCount++;

				if (row != 0)
					newGameField[row - 1, col].NeibourMinesCount++;

				if (row != 0 && col != mineFieldSize - 1)
					newGameField[row - 1, col + 1].NeibourMinesCount++;

				if (col != 0)
					newGameField[row, col - 1].NeibourMinesCount++;

				if (col != mineFieldSize - 1)
					newGameField[row, col + 1].NeibourMinesCount++;

				if (row != mineFieldSize - 1 && col != 0)
					newGameField[row + 1, col - 1].NeibourMinesCount++;

				if (row != mineFieldSize - 1)
					newGameField[row + 1, col].NeibourMinesCount++;

				if (row != mineFieldSize - 1 && col != mineFieldSize - 1)
					newGameField[row + 1, col + 1].NeibourMinesCount++;

				setMineCount++;
			} while (setMineCount != mineCount);

			return newGameField;
		}
	}
}