using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomControls
{
	public class CustomDrawnElement : FrameworkElement
	{
		public static readonly DependencyProperty BackgroundColorProperty;

		static CustomDrawnElement()
		{
			var metadata = new FrameworkPropertyMetadata(Colors.Yellow) {AffectsRender = true};
			BackgroundColorProperty = DependencyProperty.Register(nameof(BackgroundColor),
				typeof(Color), typeof(CustomDrawnElement), metadata);
		}

		public Color BackgroundColor
		{
			get { return (Color) GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
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
				Center = relativeGradientOrigin
			};
			brush.Freeze();

			return brush;
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);

			var bounds = new Rect(0, 0, ActualWidth, ActualHeight);
			drawingContext.DrawRectangle(GetForegroundBrush(), null, bounds);
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
	}
}