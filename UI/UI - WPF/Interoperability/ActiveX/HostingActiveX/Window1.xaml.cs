using System.Windows;
using System.Windows.Forms.Integration;
using AxMSTSCLib;

namespace HostingActiveX
{
   public partial class Window1
   {
      private readonly AxMsTscAxNotSafeForScripting _terminalService;

      public Window1()
      {
         InitializeComponent();

         // Create the host and the ActiveX control
         _terminalService = new AxMsTscAxNotSafeForScripting();

         // Add the ActiveX control to the host, and the host to the WPF panel
         var host = new WindowsFormsHost { Child = _terminalService };
         Panel.Children.Add(host);
      }

      private void OnConnect(object sender, RoutedEventArgs e)
      {
         _terminalService.Server = ServerBox.Text;
         _terminalService.Connect();
      }
   }
}