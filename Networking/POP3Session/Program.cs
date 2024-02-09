using System.Net.Sockets;
using System.Text;

using var client = new TcpClient("mail.isp.com", 110);
using var networkStream = client.GetStream();
ReadLine(networkStream); // Read the welcome message.
SendCommand(networkStream, "USER username");
SendCommand(networkStream, "PASS password");
SendCommand(networkStream, "LIST"); // Retrieve message IDs
var messageIDs = new List<int>();
while (true)
{
   var line = ReadLine(networkStream); // e.g.,  "1 1876"
   if (line == ".")
   {
      break;
   }

   messageIDs.Add(int.Parse(line.Split(' ')[0])); // Message ID
}

foreach (var id in messageIDs) // Retrieve each message.
{
   SendCommand(networkStream, "RETR " + id);
   var randomFile = Guid.NewGuid() + ".eml";
   using (var writer = File.CreateText(randomFile))
   {
      while (true)
      {
         var line = ReadLine(networkStream); // Read next line of message.
         if (line == ".")
         {
            break; // Single dot = end of message.
         }

         if (line == "..")
         {
            line = "."; // "Escape out" double dot.
         }

         writer.WriteLine(line); // Write to output file.
      }
   }

   SendCommand(networkStream, "DELE " + id); // Delete message off server.
}

SendCommand(networkStream, "QUIT");
return;

string ReadLine(Stream s)
{
   var lineBuffer = new List<byte>();
   while (true)
   {
      var b = s.ReadByte();
      if (b is 10 or < 0)
      {
         break;
      }

      if (b != 13)
      {
         lineBuffer.Add((byte)b);
      }
   }

   return Encoding.UTF8.GetString(lineBuffer.ToArray());
}

void SendCommand(Stream stream, string line)
{
   var data = Encoding.UTF8.GetBytes($"{line}{Environment.NewLine}");
   stream.Write(data, 0, data.Length);
   var response = ReadLine(stream);
   if (!response.StartsWith("+OK"))
   {
      throw new Exception("POP Error: " + response);
   }
}