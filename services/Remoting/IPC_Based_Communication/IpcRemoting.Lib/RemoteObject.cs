using System;
using System.Runtime.Remoting.Messaging;

namespace IpcRemoting.Lib;

public class RemoteObject : MarshalByRefObject
{
   public const string DefaultAppName = "mDbg";
   public const string DefaultIpcPortName = "9090";
   public const string DefaultUri = "mDbg_IPC.rem";
   public const string CookieKey = nameof(CookieKey);

   public RemoteObject()
   {
      Console.WriteLine($"Ctor called for {nameof(RemoteObject)}");
   }

   public event EventHandler<StatusEventArgs> StatusChanged;

   public string SayHi(string name)
   {
      Console.WriteLine($"{nameof(SayHi)} called");
      return $"Hi from remote endpoint, {name}";
   }

   public void SayHiWithSubscription(string name)
   {
      var eventArgs = new StatusEventArgs($"Hi with subscription, {name}");
      OnStatusChanged(eventArgs);
   }

   public string SayHiWithClientData() =>
      CallContext.GetData(CookieKey) is CallContextData ctxData
         ? $"Hi, with your context data: '{ctxData.Data}'"
         : "Hi, you have no context data set";

   protected virtual void OnStatusChanged(StatusEventArgs e)
   {
      StatusChanged?.Invoke(this, e);
   }
}