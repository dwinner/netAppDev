using Android.Views.Animations;
using Java.Lang;

namespace SatelliteMovingApp.Lib
{
   /// <summary>
   ///    Генератор объектов для анимации представлений
   /// </summary>
   public static class ViewAnimationFactory
   {
      /// <summary>
      ///    Вращение View относительно центра
      /// </summary>
      /// <param name="aDistance">Расстояние от центра</param>
      /// <param name="anAngle">Угол расположения относительно горизонтали</param>
      /// <param name="aDuration">Время полного оборота</param>
      /// <returns></returns>
      public static Animation CreateOrbitalRotation(float aDistance, float anAngle, long aDuration)
         => new OrbitalAnimation(aDistance, anAngle, aDuration);

      /// <summary>
      ///    Реализация анимации вращения по орбите
      /// </summary>
      private sealed class OrbitalAnimation : Animation
      {
         private readonly float _angle;
         private readonly float _distance;
         private readonly long _duration;
         private float _centerX;
         private float _centerY;

         public OrbitalAnimation(float aDistance, float anAngle, long aDuration)
         {
            _distance = aDistance;
            _angle = anAngle;
            _duration = aDuration;
         }
         
         public override void Initialize(int width, int height, int parentWidth, int parentHeight)
         {
            base.Initialize(width, height, parentWidth, parentHeight);

            _centerX = (float) (_distance * Math.Cos(_angle)) + width / 2.0f;
            _centerY = (float) (_distance * Math.Sin(_angle)) + height / 2.0f;
            Duration = _duration;
            RepeatMode = RepeatMode.Restart;
            RepeatCount = Infinite;
            Interpolator = new LinearInterpolator();
         }

         protected override void ApplyTransformation(float interpolatedTime, Transformation transformation)
         {
            base.ApplyTransformation(interpolatedTime, transformation);

            var matrix = transformation.Matrix;
            matrix.SetRotate(interpolatedTime * 360);
            matrix.PreTranslate(-_centerX, -_centerY);
            matrix.PostTranslate(_centerX, _centerY);
         }
      }
   }
}