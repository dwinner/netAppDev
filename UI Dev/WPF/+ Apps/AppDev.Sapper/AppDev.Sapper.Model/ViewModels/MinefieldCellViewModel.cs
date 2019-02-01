using AppDev.Sapper.Model.Enums;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AppDev.Sapper.Model.ViewModels
{
   /// <summary>
   ///    Minefield cell view model
   /// </summary>
   [UsedImplicitly]
   public sealed class MinefieldCellViewModel : ReactiveObject
   {
	   /// <summary>
		///    Row
		/// </summary>
		[Reactive]
		public int Row { get; set; }

		/// <summary>
		///    Column
		/// </summary>
		[Reactive]
		public int Column { get; set; }

		/// <summary>
		///    The cell has mine
		/// </summary>
		[Reactive]
		public bool HasMine { get; set; }

      /// <summary>
      ///    The state of the cell
      /// </summary>
      [Reactive]
      public MinefieldCellState State { get; set; }

		/// <summary>
		///    The number of mines in neibour cells
		/// </summary>
		[Reactive]
		public int NeibourMinesCount { get; set; }
   }
}