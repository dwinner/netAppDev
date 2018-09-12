using System.Globalization;
using System.Text;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Net;

namespace Microsoft.WindowsAPICodePack.Samples.NetworkDemo
{
   public partial class Window1
   {
      public Window1()
      {
         InitializeComponent();
         LoadNetworkConnections();
      }

      private void LoadNetworkConnections()
      {
         var networks = NetworkListManager.GetNetworks(NetworkConnectivityLevels.All);

         foreach (var n in networks)
         {
            var tabItem = new TabItem { Header = string.Format("Network {0} ({1})", TabControl1.Items.Count, n.Name) };
            TabControl1.Items.Add(tabItem);
            var stackPanel2 = new StackPanel { Orientation = Orientation.Vertical };
            
            AddProperty("Name: ", n.Name, stackPanel2);
            AddProperty("Description: ", n.Description, stackPanel2);
            AddProperty("Domain type: ", n.DomainType.ToString(), stackPanel2);
            AddProperty("Is connected: ", n.IsConnected.ToString(), stackPanel2);
            AddProperty("Is connected to the internet: ", n.IsConnectedToInternet.ToString(), stackPanel2);
            AddProperty("Network ID: ", n.NetworkId.ToString(), stackPanel2);
            AddProperty("Category: ", n.Category.ToString(), stackPanel2);
            AddProperty("Created time: ", n.CreatedTime.ToString(CultureInfo.InvariantCulture), stackPanel2);
            AddProperty("Connected time: ", n.ConnectedTime.ToString(CultureInfo.InvariantCulture), stackPanel2);
            AddProperty("Connectivity: ", n.Connectivity.ToString(), stackPanel2);
            
            var s = new StringBuilder();
            s.AppendLine("Network Connections:");
            var connections = n.Connections;
            foreach (var nc in connections)
            {
               s.AppendFormat(
                  "\n\tConnection ID: {0}\n\tDomain: {1}\n\tIs connected: {2}\n\tIs connected to internet: {3}\n",
                  nc.ConnectionId, nc.DomainType, nc.IsConnected, nc.IsConnectedToInternet);
               s.AppendFormat("\tAdapter ID: {0}\n\tConnectivity: {1}\n",
                  nc.AdapterId, nc.Connectivity);
            }
            s.AppendLine();

            var label = new Label { Content = s.ToString() };

            stackPanel2.Children.Add(label);
            tabItem.Content = stackPanel2;
         }
      }

      private static void AddProperty(string propertyName, string propertyValue, Panel parent)
      {
         var panel = new StackPanel { Orientation = Orientation.Horizontal };

         var propertyNameLabel = new Label { Content = propertyName };
         panel.Children.Add(propertyNameLabel);

         var propertyValueLabel = new Label { Content = propertyValue };
         panel.Children.Add(propertyValueLabel);

         parent.Children.Add(panel);
      }
   }
}