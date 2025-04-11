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

   public string SayHi(string name)
   {
      Console.WriteLine($"{nameof(SayHi)} called");
      return $"Hi from remote endpoint, {name}";
   }
}