/*
 * Клиент для RPC
 */

using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels.Tcp;
using RemoteHello.Hello;

namespace RemoteHello.RemoteClient
{
   internal static class RemoteClient
   {
      private static void Main()
      {
         Console.WriteLine("Press return after the server is started");
         Console.ReadLine();

         ChannelServices.RegisterChannel(new IpcChannel(), false);
         ChannelServices.RegisterChannel(new TcpClientChannel(), false);
         var helloObject = Activator.GetObject(typeof(HelloEntity), "tcp://localhost:8086/Hi") as HelloEntity;
         if (helloObject == null)
         {
            Console.WriteLine("Could not locate server");
            return;
         }

         for (var i = 0; i < 5; i++)
         {
            Console.WriteLine(helloObject.Greeting("Denis"));
         }

         // TONOTE: Если больше нравится операция new:

         // RemotingConfiguration.RegisterWellKnownClientType(typeof(HelloEntity), "tcp://localhost:8086/Hi");
         // RemotingConfiguration.RegisterActivatedClientType(typeof(HelloEntity), "tcp://localhost:8086/Hi");
         // var entity = new HelloEntity();
      }
   }
}