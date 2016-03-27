using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ColorConverter
{
   public sealed partial class ColorSlider : UserControl
   {
      private const int ColorRectMargin = 5;
      private LinearGradientBrush _brush;
      private Rectangle _colorRect;
      private bool _dragging;
      private Color _startColor, _endColor;

      public ColorSlider()
      {
         InitializeComponent();

         StartColor = Color.Black;
         EndColor = Color.White;
         numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
         DoubleBuffered = true;
      }

      public int Maximum
      {
         private get { return (int)numericUpDown1.Maximum; }
         set { numericUpDown1.Maximum = value; }
      }

      public Color StartColor
      {
         private get { return _startColor; }
         set
         {
            _endColor = value;
            CreateBrush();
         }
      }

      public Color EndColor
      {
         private get { return _endColor; }
         set
         {
            _endColor = value;
            CreateBrush();
         }
      }

      public Color[] CustomColors { private get; set; }

      public int Value
      {
         get { return (int)numericUpDown1.Value; }
         set { numericUpDown1.Value = value; }
      }

      public event EventHandler<EventArgs> ValueChanged;

      private void OnValueChanged()
      {
         if (ValueChanged != null)
         {
            ValueChanged(this, EventArgs.Empty);
         }
      }

      public void SetColors(Color startColor, Color endColor)
      {
         _startColor = startColor;
         _endColor = endColor;
         CreateBrush();
         Refresh();
      }

      private void CreateBrush()
      {
         if (_brush != null)
         {
            _brush.Dispose();
         }
         _brush = new LinearGradientBrush(_colorRect, StartColor, EndColor, 0.0f);
      }

      private void numericUpDown1_ValueChanged(object sender, EventArgs e)
      {
         Refresh();
         OnValueChanged();
      }

      protected override void OnSizeChanged(EventArgs e)
      {
         if (!ClientRectangle.IsEmpty)
         {
            _colorRect = new Rectangle(ColorRectMargin, ColorRectMargin,
               ClientRectangle.Width - numericUpDown1.Width - ColorRectMargin * 3,
               ClientSize.Height - ColorRectMargin * 2);
         }
         Refresh();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         DrawColorRect(e.Graphics);
         DrawSliderWidget(e.Graphics);
      }

      private void DrawSliderWidget(Graphics graphics)
      {
         var state = graphics.Save();
         Point[] outerPoints =
         {
            new Point(0, -10),
            new Point(5, 0),
            new Point(0, 5),
            new Point(-5, 0),
            new Point(0, -10)
         };

         Point[] innerPoints =
         {
            new Point(0, -5),
            new Point(2, 0),
            new Point(0, 2),
            new Point(-2, 0),
            new Point(0, -5)
         };
         var path = new GraphicsPath();
         path.AddPolygon(outerPoints);
         path.AddPolygon(innerPoints);
         graphics.TranslateTransform(_colorRect.Left + (float)(_colorRect.Width * numericUpDown1.Value) / Maximum,
            _colorRect.Bottom);
         graphics.FillPath(Brushes.DarkGray, path);
         //graphics.DrawPolygon(Pens.Black, points);
         graphics.Restore(state);
      }

      // Mouse events

      protected override void OnMouseDown(MouseEventArgs e)
      {
         if (_colorRect.Contains(e.Location) && e.Button == MouseButtons.Left)
         {
            _dragging = true;
            UpdateSliderPos(e.Location);
         }
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
         if (_dragging && e.Button == MouseButtons.Left)
         {
            _dragging = false;
            UpdateSliderPos(e.Location);
         }
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
         if (_dragging)
         {
            UpdateSliderPos(e.Location);
         }
      }

      private void UpdateSliderPos(Point location)
      {
         var percent = (double)(location.X - _colorRect.Left) / _colorRect.Width;
         if (percent > 1.0)
            percent = 1.0;
         else if (percent < 0.0)
            percent = 0.0;
         numericUpDown1.Value = (int)(percent * Maximum);
      }

      private void DrawColorRect(Graphics graphics)
      {
         if (CustomColors == null)
         {
            if (_brush == null)
               CreateBrush();

            if (_brush != null)
               graphics.FillRectangle(_brush, _colorRect);
         }
         else
         {
            //draw a vertical slice for each custom color
            var sliceWidth = (double)_colorRect.Width / CustomColors.Length;
            double left = _colorRect.Left;
            foreach (var color in CustomColors)
            {
               using (Brush brush = new SolidBrush(color))
               {
                  graphics.FillRectangle(brush, (int)left, _colorRect.Top, (int)((sliceWidth < 1) ? 1 : sliceWidth),
                     _colorRect.Height);
               }
               left += sliceWidth;
            }
         }
         graphics.DrawRectangle(Pens.Black, _colorRect);
      }
   }
}