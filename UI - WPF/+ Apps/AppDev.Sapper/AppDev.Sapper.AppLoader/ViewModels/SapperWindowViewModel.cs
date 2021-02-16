using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using AppDev.Sapper.AppLoader.Infrastructure;
using AppDev.Sapper.Model.Enums;
using AppDev.Sapper.Model.Ext;
using AppDev.Sapper.Model.Models;
using AppDev.Sapper.Model.ViewModels;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using static ReactiveUI.RxApp;
using PostSharp.Patterns.Contracts;

namespace AppDev.Sapper.AppLoader.ViewModels
{
	/// <summary>
	///    View model for Sapper Window
	/// </summary>
	[UsedImplicitly]
	public sealed class SapperWindowViewModel : ReactiveObject
	{
		private const int DefaultMinefieldSize = 10; // NOTE: Hard coded value
		private const int DefaultMaxMineCount = 20;  // NOTE: Hard coded value
		private readonly IGameFieldFactory _fieldFactory;

		public SapperWindowViewModel([Required] IGameFieldFactory fieldFactory)
		{
			_fieldFactory = fieldFactory;
			MinefieldSize = DefaultMinefieldSize;
			MaxMineCount = DefaultMaxMineCount;

			// There is already a request to create a new field when the window is loaded,
			// but here I shouldn't know about it. I just want to avoid NullReferenceException
			Minefields = CreateMinefieldCellCollection();

			StatusLog = new ReactiveList<string>();
			this.WhenAnyValue(vm => vm.Status)
				.Subscribe(status =>
				{
					var gameStatusMessage = status.GetGameStatus();
					StatusLog.Clear();
					StatusLog.Add(gameStatusMessage);
				});

			SetupNewGameCommand();
		}

		[Reactive]
		public int MinefieldSize { get; set; }

		[Reactive]
		public int MaxMineCount { get; set; }

		public MinefieldCellCollection Minefields { get; }

		[Reactive]
		public GameStatus Status { get; set; }

		[Reactive]
		public ReactiveList<string> StatusLog { get; set; }

		public ReactiveCommand<Unit, MinefieldCellCollection> NewGameCommand { get; private set; }

		private void SetupNewGameCommand()
		{
			NewGameCommand = ReactiveCommand.Create<Unit, MinefieldCellCollection>(
				unit => CreateMinefieldCellCollection(),
				this.WhenAny(vm => vm.Status, model => model.Value != GameStatus.Begin));
			NewGameCommand
				.ObserveOn(MainThreadScheduler)
				.Subscribe(UpdateMinefield);
			NewGameCommand.ThrownExceptions.Subscribe(exception => MessageBox.Show(exception.Message));
		}

		private void UpdateMinefield(MinefieldCellCollection newMinefield)
		{
			for (var row = 0; row < MinefieldSize; row++)
			for (var col = 0; col < MinefieldSize; col++)
			{
				Minefields[row, col].Row = newMinefield[row, col].Row;
				Minefields[row, col].Column = newMinefield[row, col].Column;
				Minefields[row, col].HasMine = newMinefield[row, col].HasMine;
				Minefields[row, col].State = newMinefield[row, col].State;
				Minefields[row, col].NeibourMinesCount = newMinefield[row, col].NeibourMinesCount;
			}

			Status = GameStatus.Begin;
		}

		private MinefieldCellCollection CreateMinefieldCellCollection()
		{
			var minefieldCells = _fieldFactory.CreateNewMinefield(MinefieldSize, MinefieldSize);
			var minefieldCellVms = ToViewModels(minefieldCells, MinefieldSize);
			var cellCollection = new MinefieldCellCollection(minefieldCellVms, MinefieldSize);

			return cellCollection;
		}

		private static MinefieldCellViewModel[,] ToViewModels(MinefieldCell[,] minefieldCells, int mineFieldSize)
		{
			var minefieldCellVms = new MinefieldCellViewModel[mineFieldSize, mineFieldSize];
			for (var row = 0; row < mineFieldSize; row++)
			for (var col = 0; col < mineFieldSize; col++)
				minefieldCellVms[row, col] = new MinefieldCellViewModel
				{
					Row = minefieldCells[row, col].Row,
					Column = minefieldCells[row, col].Column,
					HasMine = minefieldCells[row, col].HasMine,
					NeibourMinesCount = minefieldCells[row, col].NeibourMinesCount,
					State = minefieldCells[row, col].State
				};

			return minefieldCellVms;
		}
	}
}