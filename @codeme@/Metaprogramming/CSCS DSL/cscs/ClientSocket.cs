using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CsCsLang
{
   internal class ClientSocket : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         // Data buffer for incoming data.
         var bytes = new byte[1024];

         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);

         Utils.CheckArgs(args.Count, 3, Constants.Connectsrv);
         Utils.CheckPosInt(args[1]);

         var hostname = args[0].String;
         var port = (int) args[1].Value;
         var msgToSend = args[2].String;

         if (string.IsNullOrWhiteSpace(hostname) || hostname.Equals("localhost")) hostname = Dns.GetHostName();

         try
         {
            var ipHostInfo = Dns.GetHostEntry(hostname);
            var ipAddress = ipHostInfo.AddressList[0];
            var remoteEP = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP  socket.
            var sender = new Socket(AddressFamily.InterNetwork,
               SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(remoteEP);

            Interpreter.Instance.AppendOutput("Connected to [" + sender.RemoteEndPoint + "]", true);

            var msg = Encoding.UTF8.GetBytes(msgToSend);
            sender.Send(msg);

            // Receive the response from the remote device.
            var bytesRec = sender.Receive(bytes);
            var received = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Interpreter.Instance.AppendOutput("Received [" + received + "]", true);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't connect to server: (" + exc.Message + ")");
         }

         return Variable.EmptyInstance;
      }
   }
}