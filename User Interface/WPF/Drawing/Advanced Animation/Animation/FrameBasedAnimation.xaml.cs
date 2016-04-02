using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Animation
{
   public partial class FrameBasedAnimation : Window
   {
      private const double AccelerationY = 0.1;
      private const int MaxEllipses = 100;
      private const int MaxStartingSpeed = 50;
      private const int MinEllipses = 20;
      private const int MinStartingSpeed = 1;
      private const double SpeedRatio = 0.1;

      private readonly List<EllipseInfo> _ellipses = new List<EllipseInfo>();

      private bool _rendering;

      public FrameBasedAnimation()
      {
         InitializeComponent();
      }

      private void OnStart(object sender, RoutedEventArgs e)
      {
         if (_rendering)
            return;

         _ellipses.Clear();
         RawCanvas.Children.Clear();
         CompositionTarget.Rendering += RenderFrame;
         _rendering = true;
      }

      private void OnStop(object sender, RoutedEventArgs e)
      {
         StopRendering();
      }

      private void StopRendering()
      {
         CompositionTarget.Rendering -= RenderFrame;
         _rendering = false;
      }

      private void RenderFrame(object sender, EventArgs e)
      {
         if (_ellipses.Count == 0)
         {
            // Animation just started. Create the _ellipses.
            var halfCanvasWidth = (int) RawCanvas.ActualWidth/2;

            var rand = new Random();
            var ellipseCount = rand.Next(MinEllipses, MaxEllipses + 1);
            for (var i = 0; i < ellipseCount; i++)
            {
               var ellipse = EllipseInfo.CreateEllipse();
               Canvas.SetLeft(ellipse, halfCanvasWidth + rand.Next(-halfCanvasWidth, halfCanvasWidth));
               Canvas.SetTop(ellipse, 0);
               RawCanvas.Children.Add(ellipse);
               var info = new EllipseInfo(ellipse, SpeedRatio*rand.Next(MinStartingSpeed, MaxStartingSpeed));
               _ellipses.Add(info);
            }
         }
         else
         {
            for (var i = _ellipses.Count - 1; i >= 0; i--)
            {
               var info = _ellipses[i];
               var top = Canvas.GetTop(info.Ellipse);
               Canvas.SetTop(info.Ellipse, top + 1*info.VelocityY);

               if (top >= RawCanvas.ActualHeight - EllipseInfo.EllipseRadius*2 - 10)
               {
                  // This circle has reached the bottom.
                  // Stop animating it.
                  _ellipses.Remove(info);
               }
               else
               {
                  // Increase the velocity.
                  info.VelocityY += AccelerationY;
               }

               if (_ellipses.Count == 0)
               {
                  // End the animation.
                  // There's no reason to keep calling this method
                  // if it has no work to do.
                  StopRendering();
               }
            }
         }
      }
   }
}