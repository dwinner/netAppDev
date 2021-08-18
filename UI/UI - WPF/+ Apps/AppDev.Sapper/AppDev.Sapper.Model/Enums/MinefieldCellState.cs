using AppDev.Sapper.Model.Attributes;
using JetBrains.Annotations;

namespace AppDev.Sapper.Model.Enums
{
	/// <summary>
	///    Minefield cell state
	/// </summary>
	public enum MinefieldCellState
	{
		/// <summary>
		///    Zero Mine Neighbor
		/// </summary>
		[MineImageFile(ImageFile = "zero.png")] [UsedImplicitly] ZeroMineNeighbor = 0,

		/// <summary>
		///    One Mine Neighbor
		/// </summary>
		[MineImageFile(ImageFile = "one.png")] [UsedImplicitly] OneMineNeighbor = 1,

		/// <summary>
		///    Two Mine Neighbor
		/// </summary>
		[MineImageFile(ImageFile = "two.png")] [UsedImplicitly] TwoMineNeighbor = 2,

		/// <summary>
		///    Three Mine Neighbor
		/// </summary>
		[MineImageFile(ImageFile = "three.png")] [UsedImplicitly] ThreeMineNeighbor = 3,

		/// <summary>
		///    Four Mine Neighbor
		/// </summary>
		[MineImageFile(ImageFile = "four.png")] [UsedImplicitly] FourMineNeighbor = 4,

		/// <summary>
		///    Five Mine Neighbor
		/// </summary>
		[MineImageFile(ImageFile = "five.png")] [UsedImplicitly] FiveMineNeighbor = 5,

		/// <summary>
		///    Six Mine Neighbor
		/// </summary>
		[MineImageFile(ImageFile = "six.png")] [UsedImplicitly] SixMineNeighbor = 6,

		/// <summary>
		///    Seven Mine Neighbor
		/// </summary>
		[MineImageFile(ImageFile = "seven.png")] [UsedImplicitly] SevenMineNeighbor = 7,

		/// <summary>
		///    Eight Mine Neighbor
		/// </summary>
		[MineImageFile(ImageFile = "eight.png")] [UsedImplicitly] EightMineNeighbor = 8,

		/// <summary>
		///    Marked
		/// </summary>
		[MineImageFile(ImageFile = "flag.png")] Marked = 9,

		/// <summary>
		///    Closed
		/// </summary>
		[MineImageFile(ImageFile = "close.png")] Closed = 10,

		/// <summary>
		///    Mine
		/// </summary>
		[MineImageFile(ImageFile = "mine.png")] Mine = 11
	}
}