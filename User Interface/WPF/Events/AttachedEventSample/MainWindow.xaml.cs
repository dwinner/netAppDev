using System.Windows;

namespace AttachedEventSample
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnTopHandle(object sender, RoutedEventArgs e)
      {
         string traceMessage;

         if (Equals(e.Source, Cmd1Button))
         {
            traceMessage = GetTraceMessage(Cmd1Button);
         }
         else if (Equals(e.Source, Cmd2Button))
         {
            traceMessage = GetTraceMessage(Cmd2Button);
         }
         else if (Equals(e.Source, Cmd3Button))
         {
            traceMessage = GetTraceMessage(Cmd3Button);
         }
         else if (Equals(e.Source, Cmd4Button))
         {
            traceMessage = GetTraceMessage(Cmd4Button);
         }
         else if (Equals(e.Source, Cmd5Button))
         {
            traceMessage = GetTraceMessage(Cmd5Button);
         }
         else if (Equals(e.Source, Cmd6Button))
         {
            traceMessage = GetTraceMessage(Cmd6Button);
         }
         else if (Equals(e.Source, Cmd7Button))
         {
            traceMessage = GetTraceMessage(Cmd7Button);
         }
         else
         {
            traceMessage = string.Empty;
         }

         MessageBox.Show(traceMessage, e.Source.ToString());
      }

      private static string GetTraceMessage(FrameworkElement aButton)
      {
         var tag = aButton.Tag;
         var traceMessage = tag != null ? tag.ToString() : string.Empty;
         return traceMessage;
      }
   }
}