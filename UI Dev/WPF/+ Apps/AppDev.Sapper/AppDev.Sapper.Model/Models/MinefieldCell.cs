using AppDev.Sapper.Model.Enums;
using AppDev.Sapper.Model.ViewModels;
using AutoMapper;

namespace AppDev.Sapper.Model.Models
{
	/// <summary>
	///    Minefield cell
	/// </summary>
	public sealed class MinefieldCell
	{
		/// <summary>
		///    Ctor
		/// </summary>
		/// <param name="row">Row</param>
		/// <param name="column">Column</param>
		/// <param name="cellState">State</param>
		public MinefieldCell(int row, int column, MinefieldCellState cellState = MinefieldCellState.Closed)
		{
			Row = row;
			Column = column;
			State = cellState;
		}

		/// <summary>
		///    Row
		/// </summary>
		public int Row { get; }

		/// <summary>
		///    Column
		/// </summary>
		public int Column { get; }

		/// <summary>
		///    The cell has mine
		/// </summary>
		public bool HasMine { get; set; }

		/// <summary>
		///    The state of the cell
		/// </summary>
		public MinefieldCellState State { get; }

		/// <summary>
		///    The number of mines in neibour cells
		/// </summary>
		public int NeibourMinesCount { get; set; }

		public static implicit operator MinefieldCellViewModel(MinefieldCell minefieldCell)
			=> Mapper.Map<MinefieldCellViewModel>(minefieldCell);
	}
}