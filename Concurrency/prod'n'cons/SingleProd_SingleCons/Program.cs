// The consumer is half as fast as the producer. The producer will finish first.

using System.Threading.Channels;

var channel =
   Channel.CreateBounded<string>(new BoundedChannelOptions(1000)
   {
      // Specifying SingleReader and/or SingleWriter 
      // allows the Channel to make optimizing assumptions
      SingleReader = true,
      SingleWriter = true
   });
var producer = Produce().ContinueWith(_ => channel.Writer.Complete());
var consumer = Consume();

return;

async Task Produce()
{
   for (var i = 0; i < 10; i++)
   {
      await channel.Writer.WriteAsync($"Msg {i}").ConfigureAwait(false);
      await Task.Delay(1000).ConfigureAwait(false);
   }

   Console.WriteLine("Producer done.");
}

async Task Consume()
{
   while (await channel.Reader.WaitToReadAsync().ConfigureAwait(false))
   {
      if (channel.Reader.TryRead(out var data))
      {
         Console.WriteLine($"Processed: {data}");
         // Simulate processing takes twice as long as producing
         await Task.Delay(2000).ConfigureAwait(false);
      }
   }

   Console.WriteLine("Consumer done.");
}