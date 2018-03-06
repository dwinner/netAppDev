using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HitTesting.Properties;

namespace HitTesting
{
   public partial class Form1 : Form
   {
      private readonly List<MyShape> _shapes = new List<MyShape>();

      public Form1()
      {
         InitializeComponent();

         _shapes.Add(new MyRectangle(10, 10, 100, 50));
         _shapes.Add(new MyCircle(new Point(150, 200), 50));
         _shapes.Add(new MyEllipse(new Point(150, 50), 35, 15));
      }

      protected override void OnSizeChanged(EventArgs e)
      {
         base.OnSizeChanged(e);
         Refresh();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);

         foreach (var shape in _shapes)
         {
            shape.Draw(e.Graphics);
         }

         //draw center cross
         e.Graphics.DrawLine(Pens.Black, ClientSize.Width / 2, ClientSize.Height / 2 - 10,
            ClientSize.Width / 2, ClientSize.Height / 2 + 10);
         e.Graphics.DrawLine(Pens.Black, ClientSize.Width / 2 - 10, ClientSize.Height / 2,
            ClientSize.Width / 2 + 10, ClientSize.Height / 2);
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
         base.OnMouseMove(e);

         labelDistance.Text = string.Format("{0:N2} from center", DistanceFromCenter(e.Location));

         foreach (var shape in _shapes.Where(shape => shape.HitTest(e.Location)))
         {
            labelOver.Text = Resources.Over + shape.ToString();
            return;
         }

         labelOver.Text = string.Empty;
      }

      private double DistanceFromCenter(Point location)
      {
         var center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);

         /*
             * Distance calculation is basically the Pythagorean theorem:
             * 
             * d = sqrt(dx^2 + dy^2) where dx and dy are the differences between the points
             */
         var dx = location.X - center.X;
         var dy = location.Y - center.Y;
         return Math.Sqrt(dx * dx + dy * dy);
      }
   }
}