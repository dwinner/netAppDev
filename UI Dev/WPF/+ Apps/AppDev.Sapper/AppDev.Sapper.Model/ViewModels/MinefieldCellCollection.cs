using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace AppDev.Sapper.Model.ViewModels
{
	/// <summary>
	///    Wrapped container for minefield сells
	///    <remarks>For more convenient jagged array support and data binding</remarks>
	/// </summary>
	public sealed class MinefieldCellCollection : IEnumerable<MinefieldCellViewModel>
	{
		private readonly MinefieldCellViewModel[,] _minefieldCells;
		private readonly int _sharedDimension;

		public MinefieldCellCollection(MinefieldCellViewModel[,] minefieldCells, int sharedDimension)
		{
			_minefieldCells = minefieldCells;
			_sharedDimension = sharedDimension;
		}

		public MinefieldCellViewModel this[int row, int col]
		{
			get
			{
				Contract.Requires(row >= 0 && row < _sharedDimension);
				Contract.Requires(col >= 0 && col < _sharedDimension);
				Contract.Ensures(_minefieldCells[row, col] != null);

				return _minefieldCells[row, col];
			}
		}

		public IEnumerator<MinefieldCellViewModel> GetEnumerator()
		{
			for (var row = 0; row < _sharedDimension; row++)
			for (var col = 0; col < _sharedDimension; col++)
				yield return _minefieldCells[row, col];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}