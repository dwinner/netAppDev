using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AppDev.Sapper.Utils.UI
{
	/// <summary>
	///    Square responsive layout
	/// </summary>
	public sealed class CellPanel : Panel
	{
		public static readonly DependencyProperty FirstColumnProperty = DependencyProperty.Register(nameof(FirstColumn),
			typeof(int), typeof(CellPanel),
			new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure), ValidateFirstColumn);

		public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(nameof(Columns),
			typeof(int), typeof(CellPanel),
			new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure), ValidateColumns);

		public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(nameof(Rows), typeof(int),
			typeof(CellPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure),
			ValidateRows);

		private int _columns;
		private int _rows;

		public int FirstColumn
		{
			get { return (int) GetValue(FirstColumnProperty); }
			set { SetValue(FirstColumnProperty, value); }
		}

		public int Columns
		{
			get { return (int) GetValue(ColumnsProperty); }
			set { SetValue(ColumnsProperty, value); }
		}

		public int Rows
		{
			get { return (int) GetValue(RowsProperty); }
			set { SetValue(RowsProperty, value); }
		}

		private static bool ValidateFirstColumn(object o) => (int) o >= default(int);

		private static bool ValidateColumns(object o) => (int) o >= default(int);

		private static bool ValidateRows(object o) => (int) o >= default(int);

		protected override Size MeasureOverride(Size constraint)
		{
			UpdateComputedValues();
			var availableSize = new Size(constraint.Width / _columns, constraint.Height / _rows);
			var num1 = default(double);
			var num2 = default(double);
			var index = default(int);
			for (var count = InternalChildren.Count; index < count; ++index)
			{
				var internalChild = InternalChildren[index];
				internalChild.Measure(availableSize);
				var desiredSize = internalChild.DesiredSize;
				if (num1 < desiredSize.Width)
					num1 = desiredSize.Width;
				if (num2 < desiredSize.Height)
					num2 = desiredSize.Height;
			}

			return new Size(num1 * _columns, num2 * _rows);
		}

		protected override Size ArrangeOverride(Size arrangeSize)
		{
			var finalRect = new Rect(default(double), default(double),
				arrangeSize.Width / _columns,
				arrangeSize.Height / _rows);
			var width = finalRect.Width;
			var num = arrangeSize.Width - 1.0;
			finalRect.X += finalRect.Width * FirstColumn;
			foreach (var internalChild in InternalChildren.Cast<UIElement>())
			{
				internalChild.Arrange(finalRect);
				if (internalChild.Visibility != Visibility.Collapsed)
				{
					finalRect.X += width;
					if (finalRect.X >= num)
					{
						finalRect.Y += finalRect.Height;
						finalRect.X = default(double);
					}
				}
			}

			return arrangeSize;
		}

		private void UpdateComputedValues()
		{
			_columns = Columns;
			_rows = Rows;
			if (FirstColumn >= _columns)
				FirstColumn = default(int);

			if (_rows != default(int) && _columns != default(int))
				return;

			var num = default(int);
			var index = default(int);
			for (var count = InternalChildren.Count; index < count; ++index)
				if (InternalChildren[index].Visibility != Visibility.Collapsed)
					++num;

			if (num == default(int))
				num = 1;

			if (_rows == default(int))
			{
				if (_columns > default(int))
				{
					_rows = (num + FirstColumn + (_columns - 1)) / _columns;
				}
				else
				{
					_rows = (int) Math.Sqrt(num);
					if (_rows * _rows < num)
						_rows = _rows + 1;

					_columns = _rows;
				}
			}
			else
			{
				if (_columns != default(int))
					return;

				_columns = (num + (_rows - 1)) / _rows;
			}
		}
	}
}