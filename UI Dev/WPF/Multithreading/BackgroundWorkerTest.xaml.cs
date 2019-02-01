using System.ComponentModel;
using System.Windows;

namespace Multithreading
{   
   public partial class BackgroundWorkerTest
   {
      private readonly BackgroundWorker _primeBackgroundWorker;

      public BackgroundWorkerTest()
      {
         InitializeComponent();
         _primeBackgroundWorker = (BackgroundWorker) FindResource("PrimeBackgroundWorker");
      }

      private void OnFind(object sender, RoutedEventArgs e)
      {
         // Disable the button and clear previous results.
         FindButton.IsEnabled = false;
         CancelButton.IsEnabled = true;
         PrimeListBox.Items.Clear();

         // Get the search range.
         int from, to;
         if (!int.TryParse(FromTextBox.Text, out from))
         {
            MessageBox.Show("Invalid From value.");
            return;
         }

         if (!int.TryParse(ToTextBox.Text, out to))
         {
            MessageBox.Show("Invalid To value.");
            return;
         }

         // Start the search for primes on another thread.
         var input = new FindPrimesInput(from, to);
         _primeBackgroundWorker.RunWorkerAsync(input);
      }

      private void OnDoWork(object sender, DoWorkEventArgs e)
      {
         // Get the input values.
         var input = (FindPrimesInput) e.Argument;

         // Start the search for primes and wait.
         var primes = Worker.FindPrimes(input.From, input.To, _primeBackgroundWorker);
         if (_primeBackgroundWorker.CancellationPending)
         {
            e.Cancel = true;
            return;
         }

         // Return the result.
         e.Result = primes;
      }

      private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if (e.Cancelled)
         {
            MessageBox.Show("Search cancelled.");
         }
         else if (e.Error != null)
         {
            // An error was thrown by the DoWork event handler.
            MessageBox.Show(e.Error.Message, "An Error Occurred");
         }
         else
         {
            var primes = (int[]) e.Result;
            foreach (var prime in primes)
               PrimeListBox.Items.Add(prime);
         }

         FindButton.IsEnabled = true;
         CancelButton.IsEnabled = false;
         PrimeProgressBar.Value = 0;
      }

      private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         PrimeProgressBar.Value = e.ProgressPercentage;
      }

      private void OnCancel(object sender, RoutedEventArgs e)
      {
         _primeBackgroundWorker.CancelAsync();
      }
   }
}