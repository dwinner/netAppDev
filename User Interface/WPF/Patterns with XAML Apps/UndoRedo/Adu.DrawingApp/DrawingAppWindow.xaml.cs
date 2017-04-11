using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Adu.DrawingApp.Configuration;
using Adu.DrawingApp.Infrastructure;
using static Adu.DrawingApp.Utils.MathHelpers;

namespace Adu.DrawingApp
{
	public partial class DrawingAppWindow
	{
		// NOTE: Might be in singleton scope
		private readonly IHistory<Path> _history = new HistoryImpl(new SurfaceImpl(),
			SingletonInjector<AppConfig>.Instance.HistoryLimit);

		private Point _currentPoint;
		private Shape _rubberBand;
		private Point _startPoint;

		public DrawingAppWindow()
		{
			InitializeComponent();
			_history.Surface.Paths.CollectionChanged += OnSurfaceChanged;
		}

		private bool IsAnyChecked
			=>
				SquareRadioButton.IsChecked == true || RectangleRadioButton.IsChecked == true ||
				CircleRadioButton.IsChecked == true || EllipseRadioButton.IsChecked == true ||
				LineRadioButton.IsChecked == true;

		private void OnDrawingWindowUnloaded(object sender, RoutedEventArgs e) =>
			_history.Surface.Paths.CollectionChanged -= OnSurfaceChanged;

		private void OnSurfaceChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			var changedAction = e.Action;
			switch (changedAction)
			{
				case NotifyCollectionChangedAction.Add:
					var addedPath = e.NewItems.Cast<Path>().FirstOrDefault();
					if (addedPath != null)
						SurfaceCanvas.Children.Add(addedPath);
					break;

				case NotifyCollectionChangedAction.Remove:
					var pathToRemove = e.OldItems.Cast<Path>().FirstOrDefault();
					if (pathToRemove != null)
						SurfaceCanvas.Children.RemoveAt(SurfaceCanvas.Children.Count - 1);
					break;

				default:
					throw new ArgumentOutOfRangeException(changedAction.ToString());
			}
		}

		private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (SurfaceCanvas.IsMouseCaptured)
				return;

			_startPoint = e.GetPosition(SurfaceCanvas);
			SurfaceCanvas.CaptureMouse();
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (!SurfaceCanvas.IsMouseCaptured)
				return;

			_currentPoint = e.GetPosition(SurfaceCanvas);
			if (_rubberBand == null)
			{
				_rubberBand = new Rectangle
				{
					Stroke = Brushes.LightCoral,
					StrokeDashArray = new DoubleCollection(new double[] {4, 2})
				};
				if (IsAnyChecked)
					SurfaceCanvas.Children.Add(_rubberBand);
			}

			var inscribingRect = GetInscribingRect(_startPoint, _currentPoint);
			_rubberBand.Width = inscribingRect.Width;
			_rubberBand.Height = inscribingRect.Height;
			Canvas.SetLeft(_rubberBand, inscribingRect.Left);
			Canvas.SetTop(_rubberBand, inscribingRect.Top);
		}

		private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (SquareRadioButton.IsChecked == true)
				_history.Surface.AddSquare(_startPoint, _currentPoint);
			else if (RectangleRadioButton.IsChecked == true)
				_history.Surface.AddRectangle(_startPoint, _currentPoint);
			else if (CircleRadioButton.IsChecked == true)
				_history.Surface.AddCircle(_startPoint, _currentPoint);
			else if (EllipseRadioButton.IsChecked == true)
				_history.Surface.AddEllipse(_startPoint, _currentPoint);
			else if (LineRadioButton.IsChecked == true)
				_history.Surface.AddLine(_startPoint, _currentPoint);

			if (_rubberBand != null)
			{
				SurfaceCanvas.Children.Remove(_rubberBand);
				_rubberBand = null;
				SurfaceCanvas.ReleaseMouseCapture();
			}
		}

		private void OnUndo(object sender, ExecutedRoutedEventArgs e) => _history.Undo();

		private void OnUndoCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = _history.CanUndo;

		private void OnRedoCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = _history.CanRedo;

		private void OnRedo(object sender, ExecutedRoutedEventArgs e) => _history.Redo();
	}
}