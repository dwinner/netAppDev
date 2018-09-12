using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace Printing
{   
   public partial class PrintQueues
   {      
      private readonly PrintServer _printServer = new PrintServer();

      public PrintQueues()
      {
         InitializeComponent();
      }

      private void OnPrintQueueWindow_Loaded(object sender, EventArgs e)
      {
         PrintQueuesListBox.DisplayMemberPath = "FullName";
         PrintQueuesListBox.SelectedValuePath = "FullName";
         PrintQueuesListBox.ItemsSource = _printServer.GetPrintQueues();
      }

      private void OnPrintQueue_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
         QueueStatusTextBox.Text = string.Format("Queue Status: {0}", queue.QueueStatus);
         JobStatusTextBlock.Text = string.Empty;
         JobsListBox.DisplayMemberPath = "JobName";
         JobsListBox.SelectedValuePath = "JobIdentifier";
         JobsListBox.ItemsSource = queue.GetPrintJobInfoCollection();
      }

      private void OnJobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         if (JobsListBox.SelectedValue == null)
         {
            JobStatusTextBlock.Text = string.Empty;
         }
         else
         {
            var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
            var job = queue.GetJob((int) JobsListBox.SelectedValue);

            JobStatusTextBlock.Text = string.Format("Job Status: {0}", job.JobStatus);
         }
      }

      private void OnPauseQueue(object sender, RoutedEventArgs e)
      {
         if (PrintQueuesListBox.SelectedValue != null)
         {
            var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
            queue.Pause();
         }
      }

      private void OnResumeQueue(object sender, RoutedEventArgs e)
      {
         if (PrintQueuesListBox.SelectedValue != null)
         {
            var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
            queue.Resume();
         }
      }

      private void OnRefreshQueue(object sender, RoutedEventArgs e)
      {
         if (PrintQueuesListBox.SelectedValue != null)
         {
            var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
            queue.Refresh();
         }
      }

      private void OnPurgeQueue(object sender, RoutedEventArgs e)
      {
         if (PrintQueuesListBox.SelectedValue != null)
         {
            var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
            queue.Purge();
         }
      }

      private void OnPauseJob(object sender, RoutedEventArgs e)
      {
         if (JobsListBox.SelectedValue != null)
         {
            var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
            var job = queue.GetJob((int) JobsListBox.SelectedValue);
            job.Pause();
         }
      }

      private void OnResumeJob(object sender, RoutedEventArgs e)
      {
         if (JobsListBox.SelectedValue != null)
         {
            var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
            var job = queue.GetJob((int) JobsListBox.SelectedValue);
            job.Resume();
         }
      }

      private void OnRefreshJob(object sender, RoutedEventArgs e)
      {
         if (JobsListBox.SelectedValue != null)
         {
            var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
            var job = queue.GetJob((int) JobsListBox.SelectedValue);
            job.Refresh();

            OnJobs_SelectionChanged(null, null);
         }
      }

      private void OnCancelJob(object sender, RoutedEventArgs e)
      {
         if (JobsListBox.SelectedValue != null)
         {
            var queue = _printServer.GetPrintQueue(PrintQueuesListBox.SelectedValue.ToString());
            var job = queue.GetJob((int) JobsListBox.SelectedValue);
            job.Cancel();

            OnPrintQueue_SelectionChanged(null, null);
         }
      }
   }
}