using System.Net;
using System.Net.Sockets;

RunServerAsync();

using (var client = new TcpClient("localhost", 51111))
using (var networkStream = client.GetStream())
using (var binWriter = new BinaryWriter(networkStream))
using (var binReader = new BinaryReader(networkStream))
{
   binWriter.Write(Enumerable.Range(0, 5000).Select(x => (byte)x).ToArray());
   binWriter.Flush();
   var bytesRead = binReader.ReadBytes(5000);
   Console.WriteLine(bytesRead);
}

Console.ReadLine();
return;

async void RunServerAsync()
{
   var listener = new TcpListener(IPAddress.Any, 51111);
   listener.Start();
   try
   {
      while (true)
      {
         var incomingClient = await listener.AcceptTcpClientAsync()
            .ConfigureAwait(false);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
         Accept(incomingClient);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
      }
   }
   finally
   {
      listener.Stop();
   }
}

async Task Accept(TcpClient client)
{
   await Task.Yield();
   try
   {
      using (client)
      await using (var networkStream = client.GetStream())
      {
         var data = new byte[5000];
         var bytesRead = 0;
         var chunkSize = 1;
         while (bytesRead < data.Length && chunkSize > 0)
         {
            bytesRead += chunkSize = await networkStream.ReadAsync(data, bytesRead, data.Length - bytesRead)
               .ConfigureAwait(false);
         }

         Array.Reverse(data); // Reverse the byte sequence
         await networkStream.WriteAsync(data, 0, data.Length)
            .ConfigureAwait(false);
      }
   }
   catch (Exception ex)
   {
      Console.WriteLine(ex.Message);
   }
}