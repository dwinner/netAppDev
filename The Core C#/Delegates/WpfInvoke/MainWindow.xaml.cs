/**
 * Гарантия обновления UI в его же потоке
 */

using System.Threading;

namespace WpfInvoke
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();

         // Запуск второго потока выполнения для проведения обновлений
         var thread = new Thread(ThreadProc) { IsBackground = true };
         thread.Start();
      }

      private void ThreadProc()
      {
         int val = 0;
         while (true)
         {
            ++val;
            UpdateValue(val);
            Thread.Sleep(200);
         }
      }

      private delegate void UpdateValueDelegate(int val);

      private void UpdateValue(int val)
      {
         if (Dispatcher.Thread != Thread.CurrentThread)
         {
            Dispatcher.Invoke(new UpdateValueDelegate(UpdateValue), val);
         }
         else
         {
            textBlock.Text = val.ToString("N0");
         }
      }
   }
}
