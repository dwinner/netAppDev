using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace MulticastNetworkUtils
{
   /// <summary>
   ///    Some utils for IP Multicasting
   /// </summary>
   public static class MulticastUtils
   {
      /// <summary>
      ///    Ping 'all-routers.mcast.net' to check if multicast works on the router
      /// </summary>
      /// <returns>true if router supports multicast packets, false - otherwise</returns>
      public static bool IsRouterMulticastEnabled()
      {
         var mcastRouterPing = new Ping();
         var mcastReply = mcastRouterPing.Send("all-routers.mcast.net");
         var mcastStatus = mcastReply.Status;
         var mcastReplyBuffer = mcastReply.Buffer;

         return mcastReplyBuffer.Length > 0 && mcastStatus == IPStatus.Success;
      }

      /// <summary>
      ///    Check if TCPIP6 stack is enabled in Registry
      /// </summary>
      /// <returns>
      ///    true, if HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters\DisabledComponents set to
      ///    0x20, false otherwise
      /// </returns>
      public static bool IsTcpIpv6StackEnabled()
      {
         var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

         // we can only check out TCPIPv6 for Windows
         if (!isWindows)
         {
            return true;
         }

         var disableComp = Registry.LocalMachine.OpenSubKey("SYSTEM")
            ?.OpenSubKey("CurrentControlSet")
            ?.OpenSubKey("Services")
            ?.OpenSubKey("Tcpip6")
            ?.OpenSubKey("Parameters")
            ?.GetValue("DisabledComponents");

         return int.TryParse(disableComp?.ToString(), out var ipv6Value) && ipv6Value == 0x20;
      }
   }
}