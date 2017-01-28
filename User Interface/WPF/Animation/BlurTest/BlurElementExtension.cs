using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace BlurTest
{
    public static class BlurElementExtension
    {
        /// <summary>
        ///     Turning blur on
        /// </summary>
        /// <param name="element">bluring element</param>
        /// <param name="blurRadius">blur radius</param>
        /// <param name="duration">blur animation duration</param>
        /// <param name="beginTime">blur animation delay</param>
        public static void BlurApply(this UIElement element,
            double blurRadius, TimeSpan duration, TimeSpan beginTime)
        {
            var blur = new BlurEffect { Radius = 0 };
            var blurEnable = new DoubleAnimation(0, blurRadius, duration) { BeginTime = beginTime };
            element.Effect = blur;
            blur.BeginAnimation(BlurEffect.RadiusProperty, blurEnable);
        }

        /// <summary>
        ///     Turning blur off
        /// </summary>
        /// <param name="element">bluring element</param>
        /// <param name="duration">blur animation duration</param>
        /// <param name="beginTime">blur animation delay</param>
        public static void BlurDisable(this UIElement element, TimeSpan duration, TimeSpan beginTime)
        {
            var blur = element.Effect as BlurEffect;
            if (blur == null || Math.Abs(blur.Radius) < double.Epsilon)
            {
                return;
            }

            var blurDisable = new DoubleAnimation(blur.Radius, 0, duration) { BeginTime = beginTime };
            blur.BeginAnimation(BlurEffect.RadiusProperty, blurDisable);
        }
    }
}