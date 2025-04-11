using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using IpcRemoting.Lib;

namespace IpcRemoting_Client;

internal class IpcRemotingClient
{
   private static void Main()
   {
      var channel = IpcRemotingUtil.CreateIpcChannel();
      ChannelServices.RegisterChannel(channel, true);

      const string objectUri = $"ipc://{RemoteObject.DefaultIpcPortName}/{RemoteObject.DefaultUri}";
      var remoteObj = (RemoteObject)Activator.GetObject(typeof(RemoteObject), objectUri);

      try
      {
         // Simple RMI
         var response = remoteObj.SayHi("Denis");
         Console.WriteLine($"RMI-endpoint answered: {response}");

         // RMI with notification from server
         var sink = new EventSink();
         remoteObj.StatusChanged += sink.StatusHandler;
         remoteObj.SayHiWithSubscription("Denis");
         remoteObj.StatusChanged -= sink.StatusHandler;

         // RMI with client data
         var cookie = new CallContextData
         {
            Data = "Info for RMI"
         };
         CallContext.SetData(RemoteObject.CookieKey, cookie);
         var answer = remoteObj.SayHiWithClientData();
         Console.WriteLine($"From RMI: {answer}");
      }
      catch (Exception commonEx)
      {
         Debug.WriteLine(commonEx.Message);
      }
   }
}