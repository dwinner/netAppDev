using Android.Graphics;
using AppDevUnited.CannonGame.App.Based;

namespace AppDevUnited.CannonGame.App.GameElements
{
   public class CannonBall : GameElement
   {
      private float _velocityX;

      public CannonBall(CannonView view, Color color, int soundId, int x, int y, int radius, float velocotyX,
         float velocotyY)
         : base(view, color, soundId, x, y, 2 * radius, 2 * radius, velocotyY)
      {
         _velocityX = velocotyX;
         OnScreen = true;
      }

      public int Radius => (Shape.Right - Shape.Left) / 2;

      public bool OnScreen { get; private set; }

      public bool CollidesWith(GameElement element) => Rect.Intersects(Shape, element.Shape) && _velocityX > 0;

      public void ReverseVelocityX() => _velocityX *= -1;

      protected override void Update(double interval)
      {
         base.Update(interval);
         Shape.Offset((int) (_velocityX * interval), 0);

         if (Shape.Top < 0 || Shape.Left < 0
                            || Shape.Bottom > View.Height
                            || Shape.Right > View.Width)
         {
            OnScreen = false;
         }
      }

      public override void Draw(Canvas canvas) =>
         canvas.DrawCircle(Shape.Left + Radius, Shape.Top + Radius, Radius, Paint);
   }
}