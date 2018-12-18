using System;
using Android.Graphics;

namespace AppDevUnited.CannonGame.App.GameElements
{
   /// <summary>
   ///    Класс представляет пушку, стреляющую ядрами
   /// </summary>
   public class Cannon
   {
      private readonly Point _barrelEnd = new Point(); // Конечная точка ствола
      private readonly int _barrelLength; // Длина ствола
      private readonly int _baseRadius; // Радиус основания
      private readonly Paint _paint = new Paint(); // Объект Paint для рисования пушки
      private readonly CannonView _view; // Представление, в котором находится пушка
      private double _barrelAngle; // Угол наклона ствола

      /// <summary>
      ///    Конструктор
      /// </summary>
      /// <param name="view">Представление <see cref="CannonView" /></param>
      /// <param name="baseRadius">Радиус основания пушки</param>
      /// <param name="barrelLength">Длина ствола</param>
      /// <param name="barrelWidth">Толщина ствола</param>
      public Cannon(CannonView view, int baseRadius, int barrelLength, int barrelWidth)
      {
         _view = view;
         _baseRadius = baseRadius;
         _barrelLength = barrelLength;
         _paint.StrokeWidth = barrelWidth; // Назначение толщины ствола
         _paint.Color = Color.Black; // Пушка окрашена в чёрный цвет
         Align(Math.PI / 2); // Ствол пушки обращен вправо
      }

      /// <summary>
      ///    Возвращает объект Cannonball, представляющий выпущенное ядро
      /// </summary>
      public CannonBall CannonBall { get; private set; }

      /// <summary>
      ///    Задает направление ствола пушки
      /// </summary>
      /// <param name="barrelAngle">Угол наклона</param>
      private void Align(double barrelAngle)
      {
         _barrelAngle = barrelAngle;
         _barrelEnd.X = (int) (_barrelLength * Math.Sin(_barrelAngle));
         _barrelEnd.Y = (int) (-_barrelLength * Math.Cos(_barrelAngle)) + _view.Height / 2;
      }

      /// <summary>
      ///    Метод создает ядро и стреляет в направлении ствола
      /// </summary>
      public void FireCannonBall()
      {
         // Вычисление горизонтальной состявляющей скорости ядра
         var velocityX = (int) (CannonView.CannonBallSpeedPercent * _view.Width * Math.Sin(_barrelAngle));

         // Вычисление вертикальной составляющей скорости ядра
         var velocityY = (int) (CannonView.CannonBallSpeedPercent * _view.Width * -Math.Cos(_barrelAngle));

         // Вычисление радиуса ядра
         var radius = _view.Height * CannonView.CannonBallSpeedPercent;

         // Построение ядра и размещение его в стволе
         CannonBall = new CannonBall(_view, Color.Black, CannonView.CannonSoundId, (int) -radius,
            (int) ((float) _view.Height / 2 - radius), (int) radius, velocityX, velocityY);

         CannonBall.PlaySound(); // Воспроизведение звука выстрела
      }

      /// <summary>
      ///    Рисование пушки на объекте Canvas
      /// </summary>
      /// <param name="canvas">Объект Canvas</param>
      public void Draw(Canvas canvas)
      {
         // Рисование ствола пушки
         canvas.DrawLine(0, (float) _view.Height / 2, _barrelEnd.X, _barrelEnd.Y, _paint);

         // Рисование основания пушки
         canvas.DrawCircle(0, (float) _view.Height / 2, _baseRadius, _paint);
      }

      public void RemoveCannonBall() => CannonBall = null;
   }
}