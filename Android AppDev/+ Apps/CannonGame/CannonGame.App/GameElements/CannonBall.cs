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

      public int Radius => (_shape.Right - _shape.Left) / 2;

      public bool OnScreen { get; private set; }

      public bool CollidesWith(GameElement element) => Rect.Intersects(_shape, element._shape) && _velocityX > 0;

      public void ReverseVelocityX() => _velocityX *= -1;

      public override void Update(double interval)
      {
         base.Update(interval);
         _shape.Offset((int) (_velocityX * interval), 0);

         if (_shape.Top < 0 || _shape.Left < 0
                            || _shape.Bottom > _view.Height
                            || _shape.Right > _view.Width)
         {
            OnScreen = false;
         }
      }

      public override void Draw(Canvas canvas) =>
         canvas.DrawCircle(_shape.Left + Radius, _shape.Top + Radius, Radius, _paint);
   }
}