using System.IO.Pipes;
using System.Text;

var messageMode = true; // Be sure Server and Client set the same

if (messageMode)
{
   using var serverStream = new NamedPipeServerStream(
      "pipedream", PipeDirection.InOut, 1, PipeTransmissionMode.Message);
   serverStream.WaitForConnection();
   var msg = "Hello"u8.ToArray();
   serverStream.Write(msg, 0, msg.Length);
   Console.WriteLine(Encoding.UTF8.GetString(ReadMessage(serverStream)));
}
else
{
   using var pipeServerStream = new NamedPipeServerStream("pipedream");
   Console.WriteLine("Please start Named Pipe Client.");
   pipeServerStream.WaitForConnection();
   pipeServerStream.WriteByte(100); // Send the value 100.
   Console.WriteLine("Response from Named Pipe Client.");
   Console.WriteLine(pipeServerStream.ReadByte());
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