using System;
using System.ComponentModel;
using System.Linq;
using AppDev.Sapper.Model.Attributes;
using AppDev.Sapper.Model.Enums;
using PostSharp.Patterns.Contracts;

namespace AppDev.Sapper.Model.Ext
{
	/// <summary>
	///    Minefield cell state extensions
	/// </summary>
	public static class MinefieldCellStateExtensions
	{
		/// <summary>
		///    Gets image file name (with extension) for appropriate <see cref="MinefieldCellState" />
		/// </summary>
		/// <param name="this">Minefield cell state</param>
		/// <returns>Image file name</returns>
		public static string GetImageFile(this MinefieldCellState @this)
		{
			if (!Enum.IsDefined(typeof(MinefieldCellState), @this))
				throw new InvalidEnumArgumentException(nameof(@this), (int) @this, typeof(MinefieldCellState));

			return @this.GetType()
				.GetField(@this.ToString())
				.GetCustomAttributes<MineImageFileAttribute>()
				.FirstOrDefault()
				?.ImageFile;
		}

		/// <summary>
		///    Parse int value to cell state
		/// </summary>
		/// <param name="this">integer value</param>
		/// <returns>Parsed MinefieldCellState-value</returns>
		/// <exception cref="ArgumentException">If value isn't define in <see cref="MinefieldCellState" /></exception>
		public static MinefieldCellState ToCellState([Positive] this int @this)
		{
			var state = (MinefieldCellState) @this;
			if (Enum.IsDefined(typeof(MinefieldCellState), state))
				return state;

			throw new ArgumentException($"state for {@this} isn't defined", nameof(@this));
		}
	}
}