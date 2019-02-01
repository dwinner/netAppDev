using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace PerformanceCounterDemo
{
   /// <summary>
   /// Мониторинг производительности
   /// </summary>
   public partial class MainWindow
   {
      // Значения счетчиков мониторинга производительности
      private int _clickCountPerSec;
      private int _mouseMoveCountPerSec;

      private PerformanceCounter _performanceCounterButtonClicks;
      private PerformanceCounter _performanceCounterButtonClicksPerSec;
      private PerformanceCounter _performanceCounterMouseMoveEvents;
      private PerformanceCounter _performanceCounterMouseMoveEventsPerSec;

      private const string PerfomanceCounterCategoryName = "Wrox Performance Counters";
      private const string InstanceName = "PerformanceCounterDemo";
      private SortedList<string, Tuple<string, string>> _perfCountNames;

      public MainWindow()
      {
         InitializeComponent();
         InitializePerfomanceCountNames();
         InitializePerformanceCounters();

         if (!PerformanceCounterCategory.Exists(PerfomanceCounterCategoryName))
         {
            return;
         }

         ButtonCount.IsEnabled = true;
         var timer = new DispatcherTimer(TimeSpan.FromSeconds(1),
            DispatcherPriority.Background,
            delegate
            {
               _performanceCounterButtonClicksPerSec.RawValue = _clickCountPerSec;
               _clickCountPerSec = 0;
               _performanceCounterMouseMoveEventsPerSec.RawValue = _mouseMoveCountPerSec;
               _mouseMoveCountPerSec = 0;
            },
            Dispatcher.CurrentDispatcher);
         timer.Start();
      }

      private void InitializePerfomanceCountNames()
      {
         _perfCountNames = new SortedList<string, Tuple<string, string>>
         {
            {"clickCount", Tuple.Create("# of button Clicks", "Total # of button clicks")},
            {"clickSec", Tuple.Create("# of button clicks/sec", "# of mouse button clicks in one second")},
            {"mouseCount", Tuple.Create("# of mouse move events", "Total # of mouse move events")},
            {"mouseSec", Tuple.Create("# of mouse move events/sec", "# of mouse move events in one second")}
         };
      }

      private void InitializePerformanceCounters()
      {
         _performanceCounterButtonClicks = new PerformanceCounter
         {
            CategoryName = PerfomanceCounterCategoryName,
            CounterName = _perfCountNames["clickCount"].Item1,
            ReadOnly = false,
            MachineName = ".",
            InstanceLifetime = PerformanceCounterInstanceLifetime.Process,
            InstanceName = InstanceName
         };
         _performanceCounterButtonClicksPerSec = new PerformanceCounter
         {
            CategoryName = PerfomanceCounterCategoryName,
            CounterName = _perfCountNames["clickSec"].Item1,
            ReadOnly = false,
            MachineName = ".",
            InstanceLifetime = PerformanceCounterInstanceLifetime.Process,
            InstanceName = InstanceName
         };
         _performanceCounterMouseMoveEvents = new PerformanceCounter
         {
            CategoryName = PerfomanceCounterCategoryName,
            CounterName = _perfCountNames["mouseCount"].Item1,
            ReadOnly = false,
            MachineName = ".",
            InstanceLifetime = PerformanceCounterInstanceLifetime.Process,
            InstanceName = InstanceName
         };
         _performanceCounterMouseMoveEventsPerSec = new PerformanceCounter
         {
            CategoryName = PerfomanceCounterCategoryName,
            CounterName = _perfCountNames["mouseSec"].Item1,
            ReadOnly = false,
            MachineName = ".",
            InstanceLifetime = PerformanceCounterInstanceLifetime.Process,
            InstanceName = InstanceName
         };
      }

      private void OnButtonClick(object sender, RoutedEventArgs e)
      {
         _performanceCounterButtonClicks.Increment();
         _clickCountPerSec++;
      }

      private void OnMouseMove(object sender, MouseEventArgs e)
      {
         _performanceCounterMouseMoveEvents.Increment();
         _mouseMoveCountPerSec++;
      }

      private void OnRegisterCounts(object sender, RoutedEventArgs e)
      {
         if (PerformanceCounterCategory.Exists(PerfomanceCounterCategoryName))
         {
            return;
         }

         var counterCreationData = new CounterCreationData[4];
         counterCreationData[0] = new CounterCreationData
         {
            CounterName = _perfCountNames["clickCount"].Item1,
            CounterType = PerformanceCounterType.NumberOfItems32,
            CounterHelp = _perfCountNames["clickCount"].Item2
         };
         counterCreationData[1] = new CounterCreationData
         {
            CounterName = _perfCountNames["clickSec"].Item1,
            CounterType = PerformanceCounterType.RateOfCountsPerSecond32,
            CounterHelp = _perfCountNames["clickSec"].Item2,
         };
         counterCreationData[2] = new CounterCreationData
         {
            CounterName = _perfCountNames["mouseCount"].Item1,
            CounterType = PerformanceCounterType.NumberOfItems32,
            CounterHelp = _perfCountNames["mouseCount"].Item2,
         };
         counterCreationData[3] = new CounterCreationData
         {
            CounterName = _perfCountNames["mouseSec"].Item1,
            CounterType = PerformanceCounterType.RateOfCountsPerSecond32,
            CounterHelp = _perfCountNames["mouseSec"].Item2,
         };
         var counters = new CounterCreationDataCollection(counterCreationData);

         var category = PerformanceCounterCategory.Create(PerfomanceCounterCategoryName,
            "Sample Counters for Professional C#",
            PerformanceCounterCategoryType.MultiInstance,
            counters);

         MessageBox.Show(String.Format("category {0} successfully created", category.CategoryName));
      }
   }
}
