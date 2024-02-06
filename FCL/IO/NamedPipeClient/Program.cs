using System.IO.Pipes;
using System.Text;

var messageMode = true; // Be sure Server and Client set the same

if (messageMode)
{
   using var clientStream = new NamedPipeClientStream("pipedream");
   clientStream.Connect();
   clientStream.ReadMode = PipeTransmissionMode.Message;
   Console.WriteLine(Encoding.UTF8.GetString(ReadMessage(clientStream)));
   var msg = "Hello right back!"u8.ToArray();
   clientStream.Write(msg, 0, msg.Length);
}
else
{
   using var s = new NamedPipeClientStream("pipedream");
   s.Connect();
   Console.WriteLine(s.ReadByte());
   s.WriteByte(200); // Send the value 200 back.
}

return;

static byte[] ReadMessage(PipeStream pipeStream)
{
   var memoryStream = new MemoryStream();
   var buffer = new byte[0x1000]; // Read in 4 KB blocks
   do
   {
      memoryStream.Write(buffer, 0, pipeStream.Read(buffer, 0, buffer.Length));
   } while (!pipeStream.IsMessageComplete);

   return memoryStream.ToArray();
}