using ProgramVerificationSystems.SelfTester.Model;
using ProgramVerificationSystems.SelfTester.Model.Poco;
using ProgramVerificationSystems.SelfTester.UI.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace ProgramVerificationSystems.SelfTester.UI
{
   /// <summary>
   ///    Утилиты для обработки UI в приложениях WPF
   /// </summary>
   public static class UiUtils
   {
      private const string StartImageFileName = "start.png";
      private const string StopImageFileName = "stop.png";
      private const string GreenBallImageFileName = "greenball.png";
      private const string OrangeBallImageFileName = "orangeball.png";
      private const string RedBallImageFileName = "redball.png";
      private const string PicsDirectory = "Pics";
      public static readonly Brush DefaulHighlightBrush = Brushes.Aquamarine;
      public const int DefaultBlurRadius = 10;
      private static readonly BitmapImage RunningImage;
      private static readonly BitmapImage StoppingImage;
      private static readonly BitmapImage GreenBallImage;
      private static readonly BitmapImage RedBallImage;
      private static readonly BitmapImage OrangeBallImage;

      static UiUtils()
      {
         RunningImage =
            new BitmapImage(
               new Uri(string.Format("{0}{1}{2}{1}{3}", Environment.CurrentDirectory, Path.DirectorySeparatorChar,
                  PicsDirectory, StartImageFileName)));
         StoppingImage =
            new BitmapImage(
               new Uri(string.Format("{0}{1}{2}{1}{3}", Environment.CurrentDirectory, Path.DirectorySeparatorChar,
                  PicsDirectory, StopImageFileName)));
         GreenBallImage =
            new BitmapImage(
               new Uri(string.Format("{0}{1}{2}{1}{3}", Environment.CurrentDirectory, Path.DirectorySeparatorChar,
                  PicsDirectory, GreenBallImageFileName)));
         RedBallImage =
            new BitmapImage(
               new Uri(string.Format("{0}{1}{2}{1}{3}", Environment.CurrentDirectory, Path.DirectorySeparatorChar,
                  PicsDirectory, RedBallImageFileName)));
         OrangeBallImage =
            new BitmapImage(
               new Uri(string.Format("{0}{1}{2}{1}{3}", Environment.CurrentDirectory, Path.DirectorySeparatorChar,
                  PicsDirectory, OrangeBallImageFileName)));
      }

      public static BitmapImage GreenBall
      {
         get { return GreenBallImage; }
      }

      public static BitmapImage RedBall
      {
         get { return RedBallImage; }
      }

      public static BitmapImage OrangeBall
      {
         get { return OrangeBallImage; }
      }

      /// <summary>
      ///    Выделение всего столбца для элемента управления DataGrid с заданной строкой заголовка
      /// </summary>
      /// <param name="startupConfigurationMode">Режим конфигурации запуска</param>
      /// <param name="dataGrid">Элемент управления DataGrid</param>
      public static void SelectColumnCellsByHeaderString(StartupConfigurationMode startupConfigurationMode,
         DataGrid dataGrid)
      {
         var foundedColumnIndex = -1;
         for (var i = 0; i < dataGrid.Columns.Count; i++)
         {
            var header = dataGrid.Columns[i].Header.ToString();
            var headerMode = SlnInfoGridHeaderToStartupConfigurationMode(header);

            if (headerMode == startupConfigurationMode)
            {
               foundedColumnIndex = i;
               break;
            }
         }

         if (foundedColumnIndex == -1) return;

         foreach (var currentItem in dataGrid.Items)
         {
            dataGrid.CurrentCell = new DataGridCellInfo(currentItem, dataGrid.Columns[foundedColumnIndex]);
            if (!dataGrid.SelectedCells.Contains(dataGrid.CurrentCell))
               dataGrid.SelectedCells.Add(dataGrid.CurrentCell);
         }
      }

      /// <summary>
      ///    Получение растрового изображения для статуса обычной кнопки
      /// </summary>
      /// <param name="buttonState">Статус кнопки</param>
      /// <returns>Растровое изображение для статуса обычной кнопки</returns>
      public static BitmapImage GetStatusImage(ButtonState buttonState)
      {
         switch (buttonState)
         {
            case ButtonState.Running:
               return StoppingImage;
            case ButtonState.Stopped:
               return RunningImage;
            default:
               throw new InvalidOperationException("You don't know, what you want");
         }
      }

      /// <summary>
      ///    Изменение визуального состояния кнопки на противоположное
      /// </summary>
      /// <param name="buttonState">Исходное состояние кнопки</param>
      /// <param name="aButton">Кнопка</param>
      /// <param name="aButtonImage">Изображение кнопки</param>
      public static void TurnButtonState(ButtonState buttonState, Button aButton, Image aButtonImage)
      {
         switch (buttonState)
         {
            case ButtonState.Running:
               aButton.Tag = ButtonState.Stopped;
               aButtonImage.Source = GetStatusImage(ButtonState.Stopped);
               break;

            case ButtonState.Stopped:
               aButton.Tag = ButtonState.Running;
               aButtonImage.Source = GetStatusImage(ButtonState.Running);
               break;

            default:
               throw new InvalidOperationException("You don't know, what you want");
         }
      }

      /// <summary>
      ///    Нахождение первого элемента XAML с указанным типом
      /// </summary>
      /// <typeparam name="T">Указанный тип</typeparam>
      /// <param name="dependencyObject">Узел поиска</param>
      /// <returns>Найденный узел</returns>
      public static T FindProperDependencyElement<T>(DependencyObject dependencyObject)
         where T : class
      {
         if (dependencyObject == null)
            return default(T);

         while ((dependencyObject != null) && !(dependencyObject is T))
            dependencyObject = VisualTreeHelper.GetParent(dependencyObject);

         return dependencyObject as T;
      }

      /// <summary>
      ///    "Ищет" ScrollViewer в потомках dependencyObject
      /// </summary>
      /// <param name="dependencyObject">Родительский объект</param>
      /// <returns>ScrollViewer, если найден, null - в противном случае</returns>
      public static DependencyObject GetScrollViewer(DependencyObject dependencyObject)
      {
         if (dependencyObject is ScrollViewer)
         {
            return dependencyObject;
         }

         for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
         {
            var child = VisualTreeHelper.GetChild(dependencyObject, i);
            var result = GetScrollViewer(child);
            if (result == null)
               continue;

            return result;
         }

         return null;
      }

      /// <summary>
      ///    Сбор результатов анализа
      /// </summary>
      /// <param name="solutionsVm">Набор с результатами</param>
      /// <returns>Кортеж результатов [Кол-во Ok'ов, Кол-во Diff'ов]</returns>
      public static Tuple<int, int> CollectResults(IEnumerable<SolutionInfoViewModel> solutionsVm)
      {
         var totalOks = 0;
         var totalDiffs = 0;

         foreach (var slnViewModel in solutionsVm)
         {
            switch (slnViewModel.LaunchStatusOnPvsVs2010)
            {
               case AnalysisStatus.OkFinished:
                  totalOks++;
                  break;
               case AnalysisStatus.DiffFinished:
                  totalDiffs++;
                  break;
            }

            switch (slnViewModel.LaunchStatusOnPvsVs2012)
            {
               case AnalysisStatus.OkFinished:
                  totalOks++;
                  break;
               case AnalysisStatus.DiffFinished:
                  totalDiffs++;
                  break;
            }

            switch (slnViewModel.LaunchStatusOnPvsVs2013)
            {
               case AnalysisStatus.OkFinished:
                  totalOks++;
                  break;
               case AnalysisStatus.DiffFinished:
                  totalDiffs++;
                  break;
            }

            switch (slnViewModel.LaunchStatusOnPvsVs2015)
            {
               case AnalysisStatus.OkFinished:
                  totalOks++;
                  break;
               case AnalysisStatus.DiffFinished:
                  totalDiffs++;
                  break;
            }
         }

         return Tuple.Create(totalOks, totalDiffs);
      }

      /// <summary>
      ///    Сброс подсветки для элементов меню
      /// </summary>
      /// <param name="menuItems">Элементы меню</param>
      /// <param name="aBrush">Кисть для сброса цвета фона</param>
      public static void ResetHighlighting(IEnumerable<MenuItem> menuItems, Brush aBrush)
      {
         foreach (var item in menuItems)
         {
            item.Background = aBrush;
         }
      }

      /// <summary>
      ///    Преобразование заголовка сетки к типу конфигурации запуска
      /// </summary>
      /// <param name="headerString">Строка заголовка</param>
      /// <returns>Конфигурация запуска</returns>
      public static StartupConfigurationMode SlnInfoGridHeaderToStartupConfigurationMode(string headerString)
      {
         if (headerString == Resources.AllColumnName)
            return StartupConfigurationMode.All;

         if (headerString == Resources.PvsVs2010ColumnName)
            return StartupConfigurationMode.PvsVs2010;

         if (headerString == Resources.PvsVs2012ColumnName)
            return StartupConfigurationMode.PvsVs2012;

         if (headerString == Resources.PvsVs2013ColumnName)
            return StartupConfigurationMode.PvsVs2013;

         if (headerString == Resources.PvsVs2015ColumnName)
            return StartupConfigurationMode.PvsVs2015;

         throw new InvalidOperationException(string.Format("Not able to transform {0}", headerString));
      }

      public static void SetMenuItemsEnabled(MenuItem topLevelMenuItem, bool enabled)
      {
         foreach (var currentMenuItem in topLevelMenuItem.Items.OfType<MenuItem>())
            currentMenuItem.IsEnabled = enabled;
      }

      /// <summary>
      ///    Сброс набора решений из завершенного в незапущенное состояние
      /// </summary>
      /// <param name="selectedSolutions">Набор решений</param>
      public static void ResetFinishedStatuses(IEnumerable<SolutionInfoViewModel> selectedSolutions)
      {
         foreach (var solution in selectedSolutions)
         {
            if (solution.LaunchStatusOnPvsVs2010.IsInFinishedStatus())
               solution.LaunchStatusOnPvsVs2010 = AnalysisStatus.NotStarted;

            if (solution.LaunchStatusOnPvsVs2012.IsInFinishedStatus())
               solution.LaunchStatusOnPvsVs2012 = AnalysisStatus.NotStarted;

            if (solution.LaunchStatusOnPvsVs2013.IsInFinishedStatus())
               solution.LaunchStatusOnPvsVs2013 = AnalysisStatus.NotStarted;

            if (solution.LaunchStatusOnPvsVs2015.IsInFinishedStatus())
               solution.LaunchStatusOnPvsVs2015 = AnalysisStatus.NotStarted;
         }
      }

      /// <summary>
      ///    Сброс набора решений из активного в незапущенное состояние
      /// </summary>
      /// <param name="selectedSolutions">Набор решений</param>
      /// <param name="resetFail">Нужно ли сбрасывать fail-статус решения</param>
      public static void ResetActiveStatuses(IEnumerable<SolutionInfoViewModel> selectedSolutions,
         bool resetFail = false)
      {
         foreach (var solution in selectedSolutions)
         {
            if (solution.LaunchStatusOnPvsVs2010.IsInActiveStatus(resetFail))
               solution.LaunchStatusOnPvsVs2010 = AnalysisStatus.NotStarted;

            if (solution.LaunchStatusOnPvsVs2012.IsInActiveStatus(resetFail))
               solution.LaunchStatusOnPvsVs2012 = AnalysisStatus.NotStarted;

            if (solution.LaunchStatusOnPvsVs2013.IsInActiveStatus(resetFail))
               solution.LaunchStatusOnPvsVs2013 = AnalysisStatus.NotStarted;

            if (solution.LaunchStatusOnPvsVs2015.IsInActiveStatus(resetFail))
               solution.LaunchStatusOnPvsVs2015 = AnalysisStatus.NotStarted;
         }
      }

      /// <summary>
      ///    Установка свойств решения для запуска
      /// </summary>
      /// <param name="startupConfigurationMode">Конфигурация запуска</param>
      /// <param name="slnInfo">Модель решения</param>
      /// <param name="pendingStatus">Статус планирования к запуску</param>
      /// <param name="currentTotalCount">Счетчик решений, запланированных к запуску</param>
      public static void EstablishLaunchStatus(StartupConfigurationMode startupConfigurationMode,
         SolutionInfoViewModel slnInfo, AnalysisStatus pendingStatus, ref int currentTotalCount)
      {
         switch (startupConfigurationMode)
         {
            case StartupConfigurationMode.PvsVs2010:
               var pvsVs2010Stat = slnInfo.LaunchStatusOnPvsVs2010;
               if (!pvsVs2010Stat.IsInDisablingStatus())
               {
                  slnInfo.LaunchStatusOnPvsVs2010 = pendingStatus;
                  currentTotalCount++;
               }
               break;

            case StartupConfigurationMode.PvsVs2012:
               var pvsVs2012Stat = slnInfo.LaunchStatusOnPvsVs2012;
               if (!pvsVs2012Stat.IsInDisablingStatus())
               {
                  slnInfo.LaunchStatusOnPvsVs2012 = pendingStatus;
                  currentTotalCount++;
               }
               break;

            case StartupConfigurationMode.PvsVs2013:
               var pvsVs2013Stat = slnInfo.LaunchStatusOnPvsVs2013;
               if (!pvsVs2013Stat.IsInDisablingStatus())
               {
                  slnInfo.LaunchStatusOnPvsVs2013 = pendingStatus;
                  currentTotalCount++;
               }
               break;

            case StartupConfigurationMode.PvsVs2015:
               var pvsVs2015Stat = slnInfo.LaunchStatusOnPvsVs2015;
               if (!pvsVs2015Stat.IsInDisablingStatus())
               {
                  slnInfo.LaunchStatusOnPvsVs2015 = pendingStatus;
                  currentTotalCount++;
               }
               break;
         }
      }

      /// <summary>
      ///    Открыть Html-лог в браузере
      /// </summary>
      /// <param name="selectedRunFolder">Директория тестового запуска</param>
      /// <param name="errorMessage">Текст сообщения об ошибке</param>
      /// <param name="caption">Заголовок сообщения об ошибке</param>
      /// <returns>true, если удалось открыть лог, false - в противном случае</returns>
      public static bool OpenHtmlLog(string selectedRunFolder, out string errorMessage, out string caption)
      {
         if (string.IsNullOrEmpty(selectedRunFolder))
         {
            errorMessage = "There is no selected testes run";
            caption = "Select Test Run";
            return false;
         }

         if (!Directory.Exists(selectedRunFolder))
         {
            errorMessage = "Selected test run is not exists";
            caption = "No test run";
            return false;
         }

         var currentHtmlLog = string.Format("{0}{1}{2}.html", selectedRunFolder, Path.DirectorySeparatorChar,
            Path.GetFileName(selectedRunFolder));

         if (!File.Exists(currentHtmlLog))
         {
            errorMessage = "Html log is not exists";
            caption = "No Html log";
            return false;
         }

         Process.Start(currentHtmlLog);
         errorMessage = caption = string.Empty;
         return true;
      }

      /// <summary>
      ///     Включение анимации для эффекта размытия элемента
      /// </summary>
      /// <param name="element">UI-элемент назначения</param>
      /// <param name="blurRadius">Радиус размытия</param>
      /// <param name="duration">Время эффекта</param>
      /// <param name="beginTime">Время задержки</param>
      public static void BlurApply(this UIElement element, double blurRadius, TimeSpan duration, TimeSpan beginTime)
      {
         var blur = new BlurEffect { Radius = 0 };
         var blurEnable = new DoubleAnimation(0, blurRadius, duration) { BeginTime = beginTime };
         element.Effect = blur;
         blur.BeginAnimation(BlurEffect.RadiusProperty, blurEnable);
      }

      /// <summary>
      ///     Выключение анимации для эффекта размытия элемента
      /// </summary>
      /// <param name="element">UI-элемент назначения</param>
      /// <param name="duration">Радиус размытия</param>
      /// <param name="beginTime">Время задержки</param>
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