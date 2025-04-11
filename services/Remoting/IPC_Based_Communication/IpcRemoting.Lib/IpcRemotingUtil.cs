using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;

namespace IpcRemoting.Lib;

/// <summary>
///    Utility methods to support .NET Remoting with IPC channels
/// </summary>
public static class IpcRemotingUtil
{
   /// <summary>
   ///    Return a new client/server IpcChannel
   /// </summary>
   /// <param name="portName">IPC port name</param>
   /// <returns>IpcChannel</returns>
   public static IpcChannel CreateIpcChannel(string portName)
   {
      var serverSinkProvider = new BinaryServerFormatterSinkProvider
      {
         TypeFilterLevel = TypeFilterLevel.Full
      };

      IDictionary properties = new Hashtable();
      properties["portName"] = portName;
      properties["authorizedGroup"] = "Everyone";

      return new IpcChannel(properties, null, serverSinkProvider);
   }

   /// <summary>
   ///    Return a new client/server IpcChannel with a unique IPC port name
   /// </summary>
   /// <returns>IpcChannel</returns>
   public static IpcChannel CreateIpcChannel() => CreateIpcChannel(Guid.NewGuid().ToString());
}