using System.Windows.Input;

namespace Calculator
{
   public static class CalculatorCommands
   {
      private static ICommand _refreshExtensionsCommand;

      private static ICommand _showExportCommand;

      private static ICommand _clearLogCommand;

      private static ICommand _calculateCommand;

      private static ICommand _activateExtensionCommand;

      private static ICommand _closeExtensionCommand;

      public static ICommand RefreshExtensionsCommand
      {
         get
         {
            return _refreshExtensionsCommand ??
                   (_refreshExtensionsCommand =
                      new RoutedUICommand("Refresh Extensions", "Refresh Extensions", typeof (CalculatorCommands)));
         }
      }

      public static ICommand ShowExportCommand
      {
         get
         {
            return _showExportCommand ??
                   (_showExportCommand =
                      new RoutedUICommand("Show Exports", "Show Exports", typeof (CalculatorCommands)));
         }
      }

      public static ICommand ClearLogCommand
      {
         get
         {
            return _clearLogCommand ??
                   (_clearLogCommand = new RoutedUICommand("Clear Log", "Clear Log", typeof (CalculatorCommands)));
         }
      }

      public static ICommand CalculateCommand
      {
         get
         {
            return _calculateCommand ??
                   (_calculateCommand = new RoutedUICommand("Calculate", "Calculate", typeof (CalculatorCommands)));
         }
      }

      public static ICommand ActivateExtensionCommand
      {
         get
         {
            return _activateExtensionCommand ??
                   (_activateExtensionCommand =
                      new RoutedUICommand("Activate AddIn", "Activate", typeof (CalculatorCommands)));
         }
      }

      public static ICommand CloseExtensionCommand
      {
         get
         {
            return _closeExtensionCommand ??
                   (_closeExtensionCommand =
                      new RoutedUICommand("Activate AddIn", "Activate", typeof (CalculatorCommands)));
         }
      }      
   }
}