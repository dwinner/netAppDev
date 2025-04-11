using System;

namespace IpcRemoting.Lib;

public class RemoteObject : MarshalByRefObject
{
   public const string DefaultAppName = "mDbg";
   public const string DefaultIpcPortName = "9090";
   public const string DefaultUri = "mDbg_IPC.rem";

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

   protected virtual void OnStatusChanged(StatusEventArgs e)
   {
      StatusChanged?.Invoke(this, e);
   }
}