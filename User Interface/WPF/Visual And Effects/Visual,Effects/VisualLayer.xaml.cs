using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Drawing
{
   public partial class VisualLayer
   {
      private Vector _clickOffset;

      //  онстанты рисовани€.
      private readonly Brush _drawingBrush = Brushes.AliceBlue;
      private readonly Pen _drawingPen = new Pen(Brushes.SteelBlue, 3);

      // Variables for dragging shapes.
      private bool _isDragging;

      // Variables for drawing the selection square.
      private bool _isMultiSelecting;
      private readonly Brush _selectedDrawingBrush = Brushes.LightGoldenrodYellow;
      private DrawingVisual _selectedVisual;
      private DrawingVisual _selectionSquare;

      private readonly Brush _selectionSquareBrush = Brushes.Transparent;
      private readonly Pen _selectionSquarePen = new Pen(Brushes.Black, 2);
      private Point _selectionSquareTopLeft;
      private readonly Size _squareSize = new Size(30, 30);

      public VisualLayer()
      {
         InitializeComponent();
         var v = new DrawingVisual();
         DrawSquare(v, new Point(10, 10), false);
      }

      private void DrawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         var position = e.GetPosition(DrawingSurface);

         if (AddRadioButton.IsChecked == true)
         {
            // —оздать, нарисовать и добавить новый квадрат
            var visual = new DrawingVisual();
            DrawSquare(visual, position, false);
            DrawingSurface.AddVisual(visual);
         }
         else if (DeleteRadioButton.IsChecked == true)
         {
            var visual = DrawingSurface.GetVisual(position);
            if (visual != null)
            {
               DrawingSurface.DeleteVisual(visual);
            }
         }
         else if (SelectMoveRadioButton.IsChecked == true)
         {
            var visual = DrawingSurface.GetVisual(position);
            if (visual != null)
            {
               /* Ќайти верхний левый угол квадрата. Ёто делаетс€ просмотром текущих границ
                  и удалени€ половины границы (толщины пера). јльтернативное решение может
                  предусматривать хранение верхней левой точки каждого визуального элемента
                  в коллекции DrawingCanvas и предоставление этой точки при проверке попадани€. */
               var topLeftCorner = new Point(
                  visual.ContentBounds.TopLeft.X + _drawingPen.Thickness / 2,
                  visual.ContentBounds.TopLeft.Y + _drawingPen.Thickness / 2);
               DrawSquare(visual, topLeftCorner, true);

               _clickOffset = topLeftCorner - position;
               _isDragging = true;

               if (_selectedVisual != null && !Equals(_selectedVisual, visual))
               {
                  // ¬ыбор изменилс€. ќчистить предыдущий выбор.
                  ClearSelection();
               }

               _selectedVisual = visual;
            }
         }
         else if (SelectMultipleRadioButton.IsChecked == true)
         {
            _selectionSquare = new DrawingVisual();

            DrawingSurface.AddVisual(_selectionSquare);

            _selectionSquareTopLeft = position;
            _isMultiSelecting = true;

            // ќбеспечить получение событи€ MouseLeftButtonUp, даже
            // если пользователь вышел за пределы Canvas. ¬ противном
            // случае могут быть нарисованы два квадрата сразу.
            DrawingSurface.CaptureMouse();
         }
      }

      // –исование квадрата.
      private void DrawSquare(DrawingVisual visual, Point topLeftCorner, bool isSelected)
      {
         using (var drawingContext = visual.RenderOpen())
         {
            var brush = _drawingBrush;
            if (isSelected)
            {
               brush = _selectedDrawingBrush;
            }

            drawingContext.DrawRectangle(brush, _drawingPen, new Rect(topLeftCorner, _squareSize));
         }
      }

      private void DrawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         _isDragging = false;

         if (_isMultiSelecting)
         {
            // ќтобразить все квадраты в этой области.
            var geometry = new RectangleGeometry(
               new Rect(_selectionSquareTopLeft, e.GetPosition(DrawingSurface)));
            var visualsInRegion = DrawingSurface.GetVisuals(geometry);
            MessageBox.Show(string.Format("You selected {0} square(s).", visualsInRegion.Count));

            _isMultiSelecting = false;
            DrawingSurface.DeleteVisual(_selectionSquare);
            DrawingSurface.ReleaseMouseCapture();
         }
      }

      private void ClearSelection()
      {
         var topLeftCorner = new Point(
            _selectedVisual.ContentBounds.TopLeft.X + _drawingPen.Thickness / 2,
            _selectedVisual.ContentBounds.TopLeft.Y + _drawingPen.Thickness / 2);
         DrawSquare(_selectedVisual, topLeftCorner, false);
         _selectedVisual = null;
      }

      private void DrawingSurface_MouseMove(object sender, MouseEventArgs e)
      {
         if (_isDragging)
         {
            var pointDragged = e.GetPosition(DrawingSurface) + _clickOffset;
            DrawSquare(_selectedVisual, pointDragged, true);
         }
         else if (_isMultiSelecting)
         {
            var pointDragged = e.GetPosition(DrawingSurface);
            DrawSelectionSquare(_selectionSquareTopLeft, pointDragged);
         }
      }

      private void DrawSelectionSquare(Point firstPoint, Point secondPoint)
      {
         _selectionSquarePen.DashStyle = DashStyles.Dash;
         using (var drawingContext = _selectionSquare.RenderOpen())
         {
            drawingContext.DrawRectangle(_selectionSquareBrush, _selectionSquarePen, new Rect(firstPoint, secondPoint));
         }
      }
   }
}