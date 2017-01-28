using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace BombDropper
{
   /// <summary>
   ///    Окно с падающими бомбами
   /// </summary>
   public partial class MainWindow
   {
      // Сначала бомбы падают каждые 1.3 секунды, достигая "земли" за 3.5 секунды.
      private const double InitialSecondsBetweenBombs = 1.3;
      private const double InitialSecondsToFall = 3.5;

      // Завершить игру после пяти упавших бомб.
      private const int MaxDropped = 5;

      // Вносить поправки скорости падения каждые 15 секунд.
      private const double SecondsBetweenAdjustments = 15;

      // При каждой поправке вычитать по 0.1 секунды из обоих значений.
      private const double SecondsBetweenBombsReduction = 0.1;
      private const double SecondsToFallReduction = 0.1;

      // Fires events on the user interface thread.
      private readonly DispatcherTimer _bombTimer = new DispatcherTimer();

      // Позволяет находить раскадровку по бомбе.
      private readonly Dictionary<Bomb, Storyboard> _storyboards = new Dictionary<Bomb, Storyboard>();

      // Счетчики для сброшенных и перехваченных бомб.
      private int _droppedCount;

      // "Adjustments" happen periodically, increasing the speed of bomb
      // falling and shortening the time between bombs.
      private DateTime _lastAdjustmentTime = DateTime.MinValue;
      private int _savedCount;
      private double _secondsBetweenBombs;
      private double _secondsToFall;

      public MainWindow()
      {
         InitializeComponent();
         _bombTimer.Tick += (sender, e) =>
         {
            PerformAdjustment();
            SetupStoryboard();
         };
      }

      private void OnCanvasBackgroundSizeChanged(object sender, SizeChangedEventArgs e)
      {
         // Установить прямоугольник отчесения, совпадающий с текущей областью отображения Canvas
         CanvasBackground.Clip = new RectangleGeometry
         {
            Rect = new Rect(0, 0, CanvasBackground.ActualWidth, CanvasBackground.ActualHeight)
         };
      }

      // Start the game.
      private void OnGameStart(object sender, RoutedEventArgs e)
      {
         StartButton.IsEnabled = false;

         // Сбросить игру.
         _droppedCount = 0;
         _savedCount = 0;
         _secondsBetweenBombs = InitialSecondsBetweenBombs;
         _secondsToFall = InitialSecondsToFall;

         // Запустить таймер сбрасывания бомб.            
         _bombTimer.Interval = TimeSpan.FromSeconds(_secondsBetweenBombs);
         _bombTimer.Start();
      }      

      private void PerformAdjustment()
      {
         // Внести поправку при необходимости.
         if (!(DateTime.Now.Subtract(_lastAdjustmentTime).TotalSeconds > SecondsBetweenAdjustments))
            return;

         _lastAdjustmentTime = DateTime.Now;
         _secondsBetweenBombs -= SecondsBetweenBombsReduction;
         _secondsToFall -= SecondsToFallReduction;

         // (Формально необходимо предпринимать проверку на 0 или отрицательные значения.
         // Однако на практике этого не произойдет, поскольку игра всегда закончится раньше.)

         // Установить таймер для сброса следующей бомбы в соответствующее время.
         _bombTimer.Interval = TimeSpan.FromSeconds(_secondsBetweenBombs);

         // Обновить сообщение о состоянии.
         RateTextblock.Text = string.Format("A bomb is released every {0} seconds.", _secondsBetweenBombs);
         SpeedTextblock.Text = string.Format("Each bomb takes {0} seconds to fall.", _secondsToFall);
      }

      private void SetupStoryboard()
      {
         var bomb = CreateBomb();
         var storyboard = new Storyboard();

         // Создать анимацию для падающей бомбы.
         var fallAnimation = CreateFallAnimation();

         Storyboard.SetTarget(fallAnimation, bomb);
         Storyboard.SetTargetProperty(fallAnimation, new PropertyPath("(Canvas.Top)"));
         storyboard.Children.Add(fallAnimation);

         // Создать анимацию "раскачивания" бомбы.
         var wiggleAnimation = CreateWiggleAnimation();

         Storyboard.SetTarget(wiggleAnimation, ((TransformGroup)bomb.RenderTransform).Children[0]);
         Storyboard.SetTargetProperty(wiggleAnimation, new PropertyPath("Angle"));
         storyboard.Children.Add(wiggleAnimation);

         // Добавить бомбу на холст.
         CanvasBackground.Children.Add(bomb);

         // Добавить раскадровку в коллекцию.            
         _storyboards.Add(bomb, storyboard);

         // Конфигурирование и запуск раскадровки.
         storyboard.Duration = fallAnimation.Duration;
         storyboard.Completed += OnStoryboardCompleted;
         storyboard.Begin();
      }

      private static DoubleAnimation CreateWiggleAnimation()
      {
         return new DoubleAnimation
         {
            To = 30,
            Duration = TimeSpan.FromSeconds(0.2),
            RepeatBehavior = RepeatBehavior.Forever,
            AutoReverse = true
         };
      }

      private DoubleAnimation CreateFallAnimation()
      {
         return new DoubleAnimation
         {
            To = CanvasBackground.ActualHeight,
            Duration = TimeSpan.FromSeconds(_secondsToFall)
         };
      }

      private Bomb CreateBomb()
      {
         // Создать бомбу.
         var bomb = new Bomb { IsFalling = true };

         // Позиционировать бомбу.            
         var random = new Random();
         bomb.SetValue(Canvas.LeftProperty,
            (double)random.Next(0, (int)(CanvasBackground.ActualWidth - 50)));
         bomb.SetValue(Canvas.TopProperty, -100.0);

         // Присоединить события щелчка мыши (для перехвата бомб).
         bomb.MouseLeftButtonDown += OnBombMouseLeftButtonDown;

         return bomb;
      }

      private void OnBombMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         // Получить бомбу.
         var bomb = (Bomb)sender;
         bomb.IsFalling = false;

         // Запомнить ее текущую (анимированную) позицию.
         var storyboard = _storyboards[bomb];
         var currentTop = Canvas.GetTop(bomb);

         // Остановить падение бомбы.
         storyboard.Stop();

         // Повторно использовать текущую раскадровку, но с новыми анимациями.
         // Запустить бомбу по новой траектории, анимируя Canvas.Top и Canvas.Left/Right
         storyboard.Children.Clear();

         var riseAnimation = new DoubleAnimation
         {
            From = currentTop,
            To = 0,
            Duration = TimeSpan.FromSeconds(2)
         };

         Storyboard.SetTarget(riseAnimation, bomb);
         Storyboard.SetTargetProperty(riseAnimation, new PropertyPath("(Canvas.Top)"));
         storyboard.Children.Add(riseAnimation);

         var slideAnimation = new DoubleAnimation();
         var currentLeft = Canvas.GetLeft(bomb);

         // Выбросить бомбу на ближайший край поля.
         slideAnimation.To = currentLeft < CanvasBackground.ActualWidth / 2 ? -100 : CanvasBackground.ActualWidth + 100;
         slideAnimation.Duration = TimeSpan.FromSeconds(1);
         Storyboard.SetTarget(slideAnimation, bomb);
         Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("(Canvas.Left)"));
         storyboard.Children.Add(slideAnimation);

         // Запустить новую анимацию.
         storyboard.Duration = slideAnimation.Duration;
         storyboard.Begin();
      }

      private void OnStoryboardCompleted(object sender, EventArgs e)
      {
         var clockGroup = (ClockGroup)sender;

         // Получить первую анимацию в раскадровке и воспользоваться ею для нахождения
         // анимированной бомбы.
         var completedAnimation = (DoubleAnimation)clockGroup.Children[0].Timeline;
         var completedBomb = (Bomb)Storyboard.GetTarget(completedAnimation);

         // Определить, упала бомба или отбита за пределы Canvas в результате щелчка.
         if (completedBomb.IsFalling)
         {
            _droppedCount++;
         }
         else
         {
            _savedCount++;
         }

         // Обновить отображение.
         StatusTextblock.Text = string.Format("You have dropped {0} bombs and saved {1}.",
            _droppedCount, _savedCount);

         // Проверить условие завершения игры.
         if (_droppedCount >= MaxDropped)
         {
            _bombTimer.Stop();
            StatusTextblock.Text += "\r\n\r\nGame over.";

            // Найти все действующие раскадровки.
            foreach (var item in _storyboards)
            {
               var storyboard = item.Value;
               var bomb = item.Key;

               storyboard.Stop();
               CanvasBackground.Children.Remove(bomb);
            }

            // Очистить коллекцию раскадровок.
            _storyboards.Clear();

            // Позволить пользователю начать новую игру.
            StartButton.IsEnabled = true;
         }
         else
         {
            // Очистить только эту бомбу и продолжить игру.
            var storyboard = (Storyboard)clockGroup.Timeline;
            storyboard.Stop();

            _storyboards.Remove(completedBomb);
            CanvasBackground.Children.Remove(completedBomb);
         }
      }
   }
}