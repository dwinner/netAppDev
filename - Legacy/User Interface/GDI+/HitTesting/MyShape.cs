using System.Drawing;

namespace HitTesting
{
   internal abstract class MyShape
   {
      public abstract void Draw(Graphics graphics);
      public abstract bool HitTest(Point location);
      public abstract override string ToString();
   }
}