/* Advanced multicast sender */

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MulticastNetworkUtils;

namespace AdvancedMulticastSender
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

         // Create IPv4 socket and adapt it for multicasting
         var mcastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
         var ipv4Addr = IPAddress.Parse("224.5.6.7");
         mcastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
            new MulticastOption(ipv4Addr));
         mcastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);

         // Connect the socket
         var ipEndPoint = new IPEndPoint(ipv4Addr, 4567);
         mcastSocket.Connect(ipEndPoint);

         // Transfer some data to the group
         var someData = new byte[10];
         for (var i = 0; i < someData.Length; i++)
         {
            someData[i] = (byte) (i + 65);
         }

         Task.Run(() =>
         {
            while (!_stop)
            {
               mcastSocket.Send(someData, someData.Length, SocketFlags.None);
               Console.WriteLine("Sending one more time...");
               Thread.Sleep(TimeSpan.FromSeconds(1.0));
            }
         });

         Console.ReadKey();

         _stop = true;
      }
   }
}