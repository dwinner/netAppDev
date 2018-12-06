using Android.Graphics;

namespace AppDevUnited.CannonGame.App.Based
{
   public class GameElement
   {
      protected Paint _paint = new Paint();
      protected internal Rect _shape;
      private readonly int _soundId;
      private float _velocityY;
      protected CannonView _view;

      public GameElement(
         CannonView view, Color color, int soundId, int x, int y, int width, int length, float velocityY)
      {
         _view = view;
         _paint.Color = color;
         _shape = new Rect(x, y, x + width, y + length);
         _soundId = soundId;
         _velocityY = velocityY;
      }

      public virtual void Update(double interval)
      {
         _shape.Offset(0, (int) (_velocityY * interval));

         if (_shape.Top < 0 && _velocityY < 0
             || _shape.Bottom > _view.Height && _velocityY > 0)
         {
            _velocityY *= -1;
         }
      }

      public virtual void Draw(Canvas canvas) => canvas.DrawRect(_shape, _paint);

      public void PlaySound() => _view.PlaySound(_soundId);
   }
}