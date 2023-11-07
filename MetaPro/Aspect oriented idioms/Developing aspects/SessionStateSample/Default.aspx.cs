namespace SessionStateSample
{
   using System;
   using System.Web.UI;

   public partial class Default : Page
   {
      [SessionState]
      private int _sessionCounter;

      [ViewState]
      private int _viewStateCounter;

      protected void incrementButton_OnClick(object sender, EventArgs e)
      {
         _sessionCounter++;
         _viewStateCounter++;
      }

      protected override void OnPreRender(EventArgs e)
      {
         sessionCounterLabel.Text = _sessionCounter.ToString();
         pageViewCounterLabel.Text = _viewStateCounter.ToString();
      }
   }
}