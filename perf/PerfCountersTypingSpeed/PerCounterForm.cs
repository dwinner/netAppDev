using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Windows.Forms;

namespace PerfCountersTypingSpeed
{
   public partial class PerCounterForm : Form
   {
      private const string CategoryName = "PerfCountersTypingSpeed";
      private readonly PerformanceCounter _bytesAllocPerformanceCounter;
      private readonly PerformanceCounter _cpuPerformanceCounter;
      private readonly Timer _timer = new Timer();
      private readonly PerformanceCounter _wpmCounter;

      public PerCounterForm()
      {
         InitializeComponent();
         _timer.Interval = 1000;
         _timer.Tick += timer_Tick;
         _timer.Enabled = true;
         var process = Process.GetCurrentProcess();

         _cpuPerformanceCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
         _bytesAllocPerformanceCounter =
            new PerformanceCounter(".NET CLR Memory", "Allocated Bytes/sec", process.ProcessName);

         CreateCustomCategories();

         _wpmCounter = new PerformanceCounter(CategoryName, "WPM", false);
         _wpmCounter.RawValue = 0;
      }

      private void CreateCustomCategories()
      {
         if (PerformanceCounterCategory.Exists(CategoryName))
         {
            return;
         }

         // Create and inform user to exit
         var counterDataCollection = new CounterCreationDataCollection();

         // Add the counter.
         var wpmCounter = new CounterCreationData();
         wpmCounter.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
         wpmCounter.CounterName = "WPM";
         counterDataCollection.Add(wpmCounter);

         try
         {
            // Create the category.
            PerformanceCounterCategory.Create(CategoryName, "Demo category to show how to create and consume counters",
               PerformanceCounterCategoryType.SingleInstance, counterDataCollection);
            MessageBox.Show("Perf Counters created. Please exit and restart the program to consume them.");
         }
         catch (SecurityException)
         {
            MessageBox.Show("Could not create Perf Counter. Please run the process as admin. Exiting");
            Environment.Exit(1);
         }
      }

      private void timer_Tick(object sender, EventArgs e)
      {
         textBoxCpu.Text = $"{_cpuPerformanceCounter.NextValue():F2}%";
         textBoxBytesPerSec.Text = $"{_bytesAllocPerformanceCounter.NextValue():F2} bytes/sec";
         textBoxWpm.Text = _wpmCounter != null
            ? $"{_wpmCounter.NextValue() * 60:F2} wpm"
            : "N/A";
      }

      protected override void OnClosing(CancelEventArgs e)
      {
         _timer.Dispose();
         base.OnClosing(e);
      }

      private void OnKeyPress(object sender, KeyPressEventArgs e)
      {
         if (e.KeyChar == ' ')
         {
            _wpmCounter.Increment();
         }
      }
   }
}