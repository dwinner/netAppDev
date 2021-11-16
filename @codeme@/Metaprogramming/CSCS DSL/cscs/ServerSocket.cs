using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CsCsLang
{
   internal class ServerSocket : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var portRes = Utils.GetItem(script);
         Utils.CheckPosInt(portRes);
         var port = (int) portRes.Value;

         try
         {
            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = ipHostInfo.AddressList[0];
            var localEndPoint = new IPEndPoint(ipAddress, port);

            var listener = new Socket(AddressFamily.InterNetwork,
               SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(10);

            Socket handler = null;
            while (true)
            {
               Interpreter.Instance.AppendOutput("Waiting for connections on " + port + " ...", true);
               handler = listener.Accept();

               // Data buffer for incoming data.
               var bytes = new byte[1024];
               var bytesRec = handler.Receive(bytes);
               var received = Encoding.UTF8.GetString(bytes, 0, bytesRec);

               Interpreter.Instance.AppendOutput("Received from " + handler.RemoteEndPoint +
                                                 ": [" + received + "]", true);

               var msg = Encoding.UTF8.GetBytes(received);
               handler.Send(msg);

               if (received.Contains("<EOF>")) break;
            }

            if (handler != null)
            {
               handler.Shutdown(SocketShutdown.Both);
               handler.Close();
            }
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't start server: (" + exc.Message + ")");
         }

         return Variable.EmptyInstance;
      }
   }
}