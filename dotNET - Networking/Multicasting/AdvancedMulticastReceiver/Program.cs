using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MulticastNetworkUtils;

namespace AdvancedMulticastReceiver
{
   internal static class Program
   {
      private static volatile bool _stop;

      private static void Main( /*string[] args*/)
      {
         var tcpIpv6StackEnabled = MulticastUtils.IsTcpIpv6StackEnabled();
         if (!tcpIpv6StackEnabled)
         {
            Console.Error.WriteLine(
               "Please set value '{0:D}' in 'HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip6\\Parameters\\DisabledComponents' to enable IPv6",
               0x20);

            return;
         }

         var isMcastEnabled = MulticastUtils.IsRouterMulticastEnabled();
         if (!isMcastEnabled)
         {
            Console.WriteLine("Your router doesn't support multicast packets");
            return;
         }

         var mcastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
         var receiveIp = new IPEndPoint(IPAddress.Any, 4567);
         mcastSocket.Bind(receiveIp);

         // Join to the group
         var inputIp = IPAddress.Parse("224.5.6.7");
         mcastSocket.SetSocketOption(
            SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(inputIp, IPAddress.Any));

         Task.Run(() =>
         {
            // Receive the data
            while (!_stop)
            {
               var rcvData = new byte[0x400];
               mcastSocket.Receive(rcvData);
               var rcvStr = Encoding.UTF8.GetString(rcvData, 0, rcvData.Length);
               Console.WriteLine(rcvStr.Trim());
               Thread.Sleep(TimeSpan.FromSeconds(1.0));
            }
         });

         Console.ReadKey();

         _stop = true;
      }
   }
}