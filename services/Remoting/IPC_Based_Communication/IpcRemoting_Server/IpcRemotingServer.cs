using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using IpcRemoting.Lib;

namespace IpcRemoting_Server;

internal class IpcRemotingServer
{
   private static void Main()
   {
      var channel = IpcRemotingUtil.CreateIpcChannel(RemoteObject.DefaultIpcPortName);
      ChannelServices.RegisterChannel(channel, true);

      var entry = new WellKnownServiceTypeEntry(
         typeof(RemoteObject),
         RemoteObject.DefaultUri,
         WellKnownObjectMode.Singleton
      );
      RemotingConfiguration.RegisterWellKnownServiceType(entry);
      RemotingConfiguration.RegisterActivatedServiceType(typeof(RemoteObject));
      RemotingConfiguration.ApplicationName = RemoteObject.DefaultAppName;

      Console.ReadLine();
   }
}