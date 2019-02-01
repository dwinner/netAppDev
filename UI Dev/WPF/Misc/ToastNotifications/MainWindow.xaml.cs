using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using MS.WindowsAPICodePack.Internal;
using ToastNotifications.ComDecl;

namespace ToastNotifications
{
   public partial class MainWindow
   {
      private const string AppId = "Toast sample";

      public MainWindow()
      {
         InitializeComponent();
         PinToStart();
      }

      private static void PinToStart()
      {
         var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            string.Format(@"Microsoft\Windows\Start Menu\Programs\{0}.lnk", AppId));
         if (File.Exists(file))
            return;

         // Create a shortcut that invokes this app with no arguments

         // ReSharper disable SuspiciousTypeConversion.Global
         var shortcut = (IShellLinkW) new ShellLink();
         shortcut.SetPath(Process.GetCurrentProcess().MainModule.FileName);
         shortcut.SetArguments(string.Empty);

         // Set the AppUserModelID
         using (var variant = new PropVariant(AppId))
         {
            var propertyStore = shortcut as IPropertyStore;
            if (propertyStore != null)
            {
               propertyStore.SetValue(SystemProperties.System.AppUserModel.ID, variant);
               propertyStore.Commit();
            }
         }

         // Save the shortcut
         var persistFile = shortcut as IPersistFile;
         if (persistFile != null)
            persistFile.Save(file, true);
         // ReSharper restore SuspiciousTypeConversion.Global
      }

      protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
      {
         base.OnMouseLeftButtonDown(e);
         ScheduleToast();
         SendToast();
      }

      private static void SendToast()
      {
         // create a string with the toast template xml
         const string xmlString = @"
<toast>
  <visual>
    <binding template='ToastImageAndText04'>
      <text id='1'>id='1': A non-wrapping header</text>
      <text id='2'>id='2': A non-wrapping line</text>
      <text id='3'>id='3': A non-wrapping line</text>
      <image id='1' src='file:///INSERT FULL PATH HERE/Assets/tileImage.jpg' />
    </binding>  
  </visual>
</toast>";

         // Load the content into an XML document
         var document = new XmlDocument();
         document.LoadXml(xmlString);

         // Create a toast notification and send it
         var notification = new ToastNotification(document);
         ToastNotificationManager.CreateToastNotifier(AppId).Show(notification);
      }

      private static void ScheduleToast()
      {
         // create a string with the toast template xml
         const string xmlString = @"
<toast>
  <visual>
    <binding template='ToastText01'>
      <text id='1'>Alert!</text>
    </binding>  
  </visual>
</toast>";

         // Load the content into an XML document
         var document = new XmlDocument();
         document.LoadXml(xmlString);

         // Create a toast notification and show it 5 seconds from now
         var notification = new ScheduledToastNotification(document, DateTimeOffset.Now + TimeSpan.FromSeconds(2));
         ToastNotificationManager.CreateToastNotifier(AppId).AddToSchedule(notification);
      }
   }
}