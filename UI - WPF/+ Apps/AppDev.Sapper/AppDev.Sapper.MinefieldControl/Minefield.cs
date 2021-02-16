using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppDev.Sapper.Model.Enums;
using AppDev.Sapper.Model.Ext;
using AppDev.Sapper.Model.ViewModels;
using static System.Windows.EventManager;
using PostSharp.Patterns.Contracts;

namespace AppDev.Sapper.MinefieldControl
{
	/**
	 * To avoid hard binding of UI-events inside an element,
	 * you can configure special commands for the control
	 */

	/// <summary>
	///    Minefield UI-less control
	/// </summary>
	[TemplatePart(Name = MinefieldCtrlPartName, Type = typeof(ItemsControl))]
	public class Minefield : Control
	{
		private ItemsControl _minefieldItemsControl;

		static Minefield()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Minefield), new FrameworkPropertyMetadata(typeof(Minefield)));

			MinefieldCellsProperty =
				DependencyProperty.Register(nameof(MinefieldCells), typeof(MinefieldCellCollection), typeof(Minefield),
					new FrameworkPropertyMetadata(OnMinefieldCellsChanged));

			GameStatusProperty =
				DependencyProperty.Register(nameof(GameStatus), typeof(GameStatus), typeof(Minefield),
					new FrameworkPropertyMetadata(default(GameStatus), OnGameStatusChanged));

			MinefieldSizeProperty =
				DependencyProperty.Register(nameof(MinefieldSize), typeof(int), typeof(Minefield),
					new PropertyMetadata(DefaultMineFieldSize));

			MineCountProperty =
				DependencyProperty.Register(nameof(MineCount), typeof(int), typeof(Minefield),
					new PropertyMetadata(DefaultMineCount));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_minefieldItemsControl = GetTemplateChild(MinefieldCtrlPartName) as ItemsControl;
			if (_minefieldItemsControl != null)
			{
				_minefieldItemsControl.ItemsSource = MinefieldCells;
				_minefieldItemsControl.KeyDown += OnMinefieldCell_KeyDown;
				_minefieldItemsControl.PreviewMouseDown += OnMinefieldCell_PreviewMouseDown;
			}
		}

		#region Internal types

		private enum CellAction
		{
			Open,
			Mark
		}

		#endregion

		#region Constants

		private const string MinefieldCtrlPartName = "PART_MinefieldListBox";
		private const int DefaultMineFieldSize = 10;
		private const int DefaultMineCount = 20;

		#endregion

		#region Internal event selectors

		private readonly Func<Key, CellAction> _keyCellActionSelector = key =>
		{
			CellAction cellAction;
			switch (key)
			{
				case Key.Enter:
				case Key.O:
					cellAction = CellAction.Open;
					break;
				case Key.M:
					cellAction = CellAction.Mark;
					break;
				default:
					goto case Key.Enter;
			}

			return cellAction;
		};

		private readonly Func<MouseButton, CellAction> _mouseCellActionSelector = mouseBtn =>
		{
			CellAction cellAction;
			switch (mouseBtn)
			{
				case MouseButton.Left:
					cellAction = CellAction.Open;
					break;
				case MouseButton.Right:
					cellAction = CellAction.Mark;
					break;
				default:
					goto case MouseButton.Left;
			}

			return cellAction;
		};

		#endregion

		#region Routed events

		/// <summary>
		///    New game event
		/// </summary>
		public static readonly RoutedEvent NewGameEvent =
			RegisterRoutedEvent(nameof(NewGame), RoutingStrategy.Bubble,
				typeof(RoutedPropertyChangedEventHandler<MinefieldCellCollection>), typeof(Minefield));

		/// <summary>
		///    Game status changed event
		/// </summary>
		public static readonly RoutedEvent GameStatusChangedEvent =
			RegisterRoutedEvent(nameof(GameStatusChanged), RoutingStrategy.Bubble,
				typeof(RoutedPropertyChangedEventHandler<GameStatus>), typeof(Minefield));

		#endregion

		#region Dependency properties

		/// <summary>
		///    Minefield cells
		/// </summary>
		public static readonly DependencyProperty MinefieldCellsProperty;

		/// <summary>
		///    Game status
		/// </summary>
		public static readonly DependencyProperty GameStatusProperty;

		/// <summary>
		///    Minefield size
		/// </summary>
		public static readonly DependencyProperty MinefieldSizeProperty;

		/// <summary>
		///    Mine count
		/// </summary>
		public static readonly DependencyProperty MineCountProperty;

		#endregion

		#region Control properties

		public MinefieldCellCollection MinefieldCells
		{
			get { return (MinefieldCellCollection) GetValue(MinefieldCellsProperty); }
			set { SetValue(MinefieldCellsProperty, value); }
		}

		public GameStatus GameStatus
		{
			get { return (GameStatus) GetValue(GameStatusProperty); }
			set { SetValue(GameStatusProperty, value); }
		}

		[StrictlyPositive]
		public int MinefieldSize
		{
			get { return (int) GetValue(MinefieldSizeProperty); }
			set { SetValue(MinefieldSizeProperty, value); }
		}

		[StrictlyPositive]
		public int MineCount
		{
			get { return (int) GetValue(MineCountProperty); }
			set { SetValue(MineCountProperty, value); }
		}

		#endregion

		#region Event wrappers

		public event RoutedPropertyChangedEventHandler<MinefieldCellCollection> NewGame
		{
			add { AddHandler(NewGameEvent, value); }
			remove { RemoveHandler(NewGameEvent, value); }
		}

		private static void OnMinefieldCellsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var minefield = (Minefield) sender;
			var oldCells = (MinefieldCellCollection) e.OldValue;
			var newCells = (MinefieldCellCollection) e.NewValue;
			if (minefield._minefieldItemsControl != null)
			{
				minefield._minefieldItemsControl.ItemsSource = newCells;
				minefield.GameStatus = GameStatus.Begin;
			}

			minefield.OnMinefieldCellsChanged(oldCells, newCells);
		}

		private void OnMinefieldCellsChanged(MinefieldCellCollection oldCells, MinefieldCellCollection newCells)
			=> RaiseEvent(
				new RoutedPropertyChangedEventArgs<MinefieldCellCollection>(oldCells, newCells)
				{
					RoutedEvent = NewGameEvent
				});

		public event RoutedPropertyChangedEventHandler<GameStatus> GameStatusChanged
		{
			add { AddHandler(GameStatusChangedEvent, value); }
			remove { RemoveHandler(GameStatusChangedEvent, value); }
		}

		private static void OnGameStatusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var minefield = (Minefield) sender;
			var oldGameStatus = (GameStatus) e.OldValue;
			var newGameStatus = (GameStatus) e.NewValue;
			minefield.OnGameStatusChanged(oldGameStatus, newGameStatus);
		}

		private void OnGameStatusChanged(GameStatus oldGameStatus, GameStatus newGameStatus)
			=> RaiseEvent(
				new RoutedPropertyChangedEventArgs<GameStatus>(oldGameStatus, newGameStatus)
				{
					RoutedEvent = GameStatusChangedEvent
				});

		#endregion

		#region Internal events

		private void OnMinefieldCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
			=> UpdateCell(e.OriginalSource as DependencyObject, _mouseCellActionSelector(e.ChangedButton));

		private void OnMinefieldCell_KeyDown(object sender, KeyEventArgs e)
			=> UpdateCell(e.OriginalSource as DependencyObject, _keyCellActionSelector(e.Key));

		private void UpdateCell(DependencyObject originalSource, CellAction cellAction)
		{
			var gameFinished = IsGameFinished;
			if (gameFinished)
				return;

			var currentCell = GetCurrentCell(originalSource);
			if (currentCell != null)
				switch (cellAction)
				{
					case CellAction.Open:
						OpenCell(currentCell);
						break;
					case CellAction.Mark:
						MarkCell(currentCell);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(cellAction));
				}
		}

		#endregion

		#region Internal members

		private bool IsGameFinished
		{
			get
			{
				bool gameFinished;
				switch (GameStatus)
				{
					case GameStatus.Begin:
						GameStatus = GameStatus.On;
						gameFinished = false;
						break;
					case GameStatus.On:
						gameFinished = false;
						break;
					case GameStatus.Victory:
					case GameStatus.Fail:
						gameFinished = true;
						break;
					default:
						throw new ArgumentOutOfRangeException(GameStatus.ToString());
				}

				return gameFinished;
			}
		}

		private void MarkCell(MinefieldCellViewModel minefieldCell)
		{
			minefieldCell.State = minefieldCell.State != MinefieldCellState.Marked
				? MinefieldCellState.Marked
				: MinefieldCellState.Closed;
			if (!IsVictoryReached(MinefieldCells))
				return;

			GameStatus = GameStatus.Victory;
		}

		private void OpenCell(MinefieldCellViewModel minefieldCell)
		{
			OpenCell(minefieldCell.Row, minefieldCell.Column);
			if (IsVictoryReached(MinefieldCells))
				GameStatus = GameStatus.Victory;
		}

		private void OpenCell(int row, int col)
		{
			if (MinefieldCells[row, col].State == MinefieldCellState.Closed
			    && MinefieldCells[row, col].HasMine)
			{
				GameStatus = GameStatus.Fail;
				MinefieldCells[row, col].State = MinefieldCellState.Mine;
			}
			else
			{
				if (MinefieldCells[row, col].State == MinefieldCellState.Closed
				    && MinefieldCells[row, col].NeibourMinesCount == 0)
				{
					MinefieldCells[row, col].State = MinefieldCells[row, col].NeibourMinesCount.ToCellState();

					if (col != 0)
						OpenCell(row, col - 1);

					if (row != 0)
						OpenCell(row - 1, col);

					if (col != MinefieldSize - 1)
						OpenCell(row, col + 1);

					if (row != MinefieldSize - 1)
						OpenCell(row + 1, col);

					if (row != 0 && col != 0)
						OpenCell(row - 1, col - 1);

					if (row != 0 && col != MinefieldSize - 1)
						OpenCell(row - 1, col + 1);

					if (row != MinefieldSize - 1 && col != 0)
						OpenCell(row + 1, col - 1);

					if (row != MinefieldSize - 1 && col != MinefieldSize - 1)
						OpenCell(row + 1, col + 1);
				}
				else if (MinefieldCells[row, col].State == MinefieldCellState.Closed
				         && MinefieldCells[row, col].NeibourMinesCount != 0)
				{
					MinefieldCells[row, col].State = MinefieldCells[row, col].NeibourMinesCount.ToCellState();
				}
			}
		}

		private MinefieldCellViewModel GetCurrentCell(DependencyObject dependencyObject)
		{
			if (_minefieldItemsControl != null)
			{
				var currentItem =
					ItemsControl.ContainerFromElement(_minefieldItemsControl, dependencyObject) as FrameworkElement;
				var currentCell = currentItem?.DataContext as MinefieldCellViewModel;
				return currentCell;
			}

			return null;
		}

		private static bool IsVictoryReached(MinefieldCellCollection @this) =>
			@this
				.All(cell => cell.State != MinefieldCellState.Closed) && @this.Where(cell => cell.HasMine)
				.All(cell => cell.State == MinefieldCellState.Marked);

		#endregion
	}
}