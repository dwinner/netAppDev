/**
 * Мониторинг и управление службами Windows
 */

using System;
using System.Linq;
using System.ServiceProcess;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ServiceCtrlFrontEnd
{
   public partial class ServiceControlWindow
   {
      private readonly TimeSpan _changeStatusDelaySeconds = TimeSpan.FromSeconds(10);

      public ServiceControlWindow()
      {
         InitializeComponent();
         RefreshServiceList();
      }

      protected void RefreshServiceList()
      {
         DataContext =
            ServiceController.GetServices()
               .OrderBy(controller => controller.DisplayName)
               .Select(controller => new ServiceControllerInfo(controller));
      }

      private void OnServiceCommand(object sender, RoutedEventArgs e)
      {
         Cursor oldCursor = Cursor;
         try
         {
            Cursor = Cursors.Wait;
            var clickButton = sender as Button;
            if (clickButton == null)
               return;
            var currentButtonState = (ButtonState)clickButton.Tag;
            var serviceControllerInfo = ServicesListBox.SelectedItem as ServiceControllerInfo;
            if (serviceControllerInfo == null)
               return;

            switch (currentButtonState)
            {
               case ButtonState.Start:
                  serviceControllerInfo.Controller.Start();
                  serviceControllerInfo.Controller.WaitForStatus(ServiceControllerStatus.Running, _changeStatusDelaySeconds);
                  break;
               case ButtonState.Stop:
                  serviceControllerInfo.Controller.Stop();
                  serviceControllerInfo.Controller.WaitForStatus(ServiceControllerStatus.Stopped, _changeStatusDelaySeconds);
                  break;
               case ButtonState.Pause:
                  serviceControllerInfo.Controller.Pause();
                  serviceControllerInfo.Controller.WaitForStatus(ServiceControllerStatus.Paused, _changeStatusDelaySeconds);
                  break;
               case ButtonState.Continue:
                  serviceControllerInfo.Controller.Continue();
                  serviceControllerInfo.Controller.WaitForStatus(ServiceControllerStatus.Running, _changeStatusDelaySeconds);
                  break;
            }

            int selectedIndex = ServicesListBox.SelectedIndex;
            RefreshServiceList();
            ServicesListBox.SelectedIndex = selectedIndex;
         }
         catch (System.ServiceProcess.TimeoutException timeoutEx)
         {
            MessageBox.Show(
               timeoutEx.Message, "Timeout Service Controller", MessageBoxButton.OK, MessageBoxImage.Error);
         }
         catch (InvalidOperationException invalidOperationEx)
         {
            MessageBox.Show(
               string.Format("{0} {1}", invalidOperationEx.Message,
                  invalidOperationEx.InnerException != null ? invalidOperationEx.InnerException.Message : String.Empty),
               "Error Service Controller", MessageBoxButton.OK, MessageBoxImage.Error);
         }
         finally
         {
            Cursor = oldCursor;
         }
      }

      private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
      {
         RefreshServiceList();
      }

      private void ExitButton_OnClick(object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }
   }
}
