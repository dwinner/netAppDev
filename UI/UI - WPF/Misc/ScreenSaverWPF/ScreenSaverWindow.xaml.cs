using System;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace ScreenSaverWPF
{
   public partial class ScreenSaverWindow
   {
      private int _imageIndex;
      private FileInfo[] _images;
      private Point _prevPt;
      private readonly Random _rand = new Random();
      private DispatcherTimer _timer;
      private bool _trackingMouse;

      //full-screen constructor
      public ScreenSaverWindow()
      {
         InitializeComponent();
         SetFullScreen();
         Initialize();
      }

      //constructor for preview window
      public ScreenSaverWindow(Point point, Size size)
      {
         InitializeComponent();
         SetWindowSize(point, size);
         Initialize();
      }

      private void Initialize()
      {
         LoadImages();

         //timer to change image every 5 seconds
         _timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 5)};
         _timer.Tick += OnTick;
         _timer.Start();
      }

      private void LoadImages()
      {
         //if you have a lot of images, this could take a while...
         var directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
         _images = directoryInfo.GetFiles("*.jpg", SearchOption.AllDirectories);
      }

      private void SetFullScreen()
      {
         //get a rectangle representing all the screens
         //alternatively, you could just have separate windows for each monitor
         var fullScreen = new Rectangle(0, 0, 0, 0);
         foreach (var screen in Screen.AllScreens)
            fullScreen = Rectangle.Union(fullScreen, screen.Bounds);
         Left = fullScreen.Left;
         Top = fullScreen.Top;
         Width = fullScreen.Width;
         Height = fullScreen.Height;
      }

      private void SetWindowSize(Point point, Size size)
      {
         Left = point.X;
         Top = point.Y;
         Width = size.Width;
         Height = size.Height;
      }

      private void OnTick(object sender, EventArgs e)
      {
         if (_images != null && _images.Length > 0)
         {
            ++_imageIndex;
            _imageIndex = _imageIndex % _images.Length;

            ImageFloating.Source = new BitmapImage(new Uri(_images[_imageIndex].FullName));
            MoveToRandomLocation(ImageFloating);
            var size = GetImageSize(ImageFloating);
            ImageFloating.Width = size.Width;
            ImageFloating.Height = size.Height;
         }
      }

      private void MoveToRandomLocation(Image imageFloating)
      {
         var x = _rand.NextDouble() * Canvas.ActualWidth / 2;
         var y = _rand.NextDouble() * Canvas.ActualHeight / 2;

         Canvas.SetLeft(imageFloating, x);
         Canvas.SetTop(imageFloating, y);
      }

      private static Size GetImageSize(Image image)
      {
         //this overly-simple algorithm won't work for the preview window
         var ratio = image.Source.Width / image.Source.Height;
         double width;
         double height;
         if (ratio > 1.0)
         {
            //wider than is tall
            width = 1024;
            height = width / ratio;
         }
         else
         {
            height = 1024;
            width = height * ratio;
         }
         return new Size(width, height);
      }

      //end screen saver
      protected override void OnKeyDown(KeyEventArgs e)
      {
         base.OnKeyDown(e);
         Application.Current.Shutdown();
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
         base.OnMouseMove(e);

         var location = e.MouseDevice.GetPosition(this);
         //use _trackingMouse to know when we've got a previous point to compare to
         if (_trackingMouse && (Math.Abs(location.X - _prevPt.X) > 10 || Math.Abs(location.Y - _prevPt.Y) > 10))
            Application.Current.Shutdown();
         _trackingMouse = true;
         _prevPt = location;
      }
   }
}