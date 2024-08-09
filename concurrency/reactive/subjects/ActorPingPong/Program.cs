using ActorPingPong;

var ping = new Ping();
var pong = new Pong();

Console.WriteLine("Press any key to stop ...");

using (ping.Subscribe(pong))
using (pong.Subscribe(ping))
{
   Console.ReadKey();
}

Console.WriteLine("Ping Pong has completed.");