namespace ThreadingInUI;

public partial class MainWindow
{
   public MainWindow()
   {
      InitializeComponent();
      var thread = new Thread(Work);
      thread.Start();
   }

   private void Work()
   {
      Thread.Sleep(5000); // Simulate time-consuming task
      UpdateMessage("The answer");
   }

   private void UpdateMessage(string message)
   {
      Dispatcher.BeginInvoke((Action)Action);
      return;

      void Action() => txtMessage.Text = message;
   }
}