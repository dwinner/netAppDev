using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomControls
{
	public class CustomDrawnDecorator : Decorator
	{
		static CustomDrawnDecorator()
		{
			CustomDrawnElement.BackgroundColorProperty.AddOwner(
				typeof(CustomDrawnDecorator));
		}

		public Color BackgroundColor
		{
			get { return (Color) GetValue(CustomDrawnElement.BackgroundColorProperty); }
			set { SetValue(CustomDrawnElement.BackgroundColorProperty, value); }
		}

		private Brush GetForegroundBrush()
		{
			if (!IsMouseOver)
				return new SolidColorBrush(BackgroundColor);

			var absoluteGradientOrigin = Mouse.GetPosition(this);
			var relativeGradientOrigin = new Point(
				absoluteGradientOrigin.X / ActualWidth, absoluteGradientOrigin.Y / ActualHeight);

			var brush = new RadialGradientBrush(Colors.White, BackgroundColor)
			{
				GradientOrigin = relativeGradientOrigin,
				RadiusX = 0.2,
				Center = relativeGradientOrigin
			};
			brush.Freeze();

			return brush;
		}

		protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);
			var bounds = new Rect(0, 0, ActualWidth, ActualHeight);
			dc.DrawRectangle(GetForegroundBrush(), null, bounds);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			InvalidateVisual();
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);
			InvalidateVisual();
		}

		protected override Size MeasureOverride(Size constraint)
		{
			var child = Child;
			if (child != null)
			{
				child.Measure(constraint);
				return child.DesiredSize;
			}

			return new Size();
		}
	}
}