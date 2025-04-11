using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using IpcRemoting.Lib;

namespace IpcRemoting_Client;

internal class IpcRemotingClient
{
   private static void Main()
   {
      var channel = IpcRemotingUtil.CreateIpcChannel();
      ChannelServices.RegisterChannel(channel, true);

      const string objectUri = $"ipc://{RemoteObject.DefaultIpcPortName}/{RemoteObject.DefaultUri}";
      var remoteObject = (RemoteObject)Activator.GetObject(typeof(RemoteObject), objectUri);

      try
      {
         var response = remoteObject.SayHi("Denis");
         Console.WriteLine($"RMI-endpoint answered: {response}");
      }
      catch (Exception commonEx)
      {
         Debug.WriteLine(commonEx.Message);
      }
   }
}