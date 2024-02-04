namespace SyncContext;

public partial class MainWindow
{
   private readonly SynchronizationContext? _uiSyncContext;

   public MainWindow()
   {
      InitializeComponent();

      // Capture the synchronization context for the current UI thread:
      _uiSyncContext = SynchronizationContext.Current;
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
      // Marshal the delegate to the UI thread:
      _uiSyncContext?.Post(_ => txtMessage.Text = message, null);
   }
}