using Microsoft.Office.Tools.Ribbon;

namespace WordEditTimerAddIn
{
   public partial class TimerRibbon
   {
      private void TimerRibbon_Load(object sender, RibbonUIEventArgs e)
      {
      }

      private void PrimaryGroup_DialogLauncherClick(object sender, RibbonControlEventArgs e)
      {
         // Показать или скрыть панель задач
         Globals.ThisAddIn.ToggleTaskPaneDisplay();
      }

      private void TimerPassedCheckBox_Click(object sender, RibbonControlEventArgs e)
      {
         // Приостановить таймер
         Globals.ThisAddIn.PauseOrResumeTimer(TimerPassedCheckBox.Checked);
      }

      private void TimerToggleButton_Click(object sender, RibbonControlEventArgs e)
      {
         // Показать или скрыть панель задач
         Globals.ThisAddIn.ToggleTaskPaneDisplay();
      }

      internal void SetPauseStatus(bool isPaused)
      {
         // Привести флажок в соответствие
         TimerPassedCheckBox.Checked = isPaused;
      }
   }
}