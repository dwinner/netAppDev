// The consumer is half as fast as the producer. We compensate by starting two consumers.

using System.Threading.Channels;

var channel =
   Channel.CreateBounded<string>(new BoundedChannelOptions(1_000)
   {
      // Specifying SingleReader and/or SingleWriter 
      // allows the Channel to make optimizing assumptions
      SingleReader = false,
      SingleWriter = true
   });

var producer = Produce().ContinueWith(_ => channel.Writer.Complete());
var consumer1 = Consume(1);
var consumer2 = Consume(2);

await Task.WhenAll(consumer1, consumer2)
   .ConfigureAwait(false);

return;

async Task Produce()
{
   for (var i = 0; i < 10; i++)
   {
      await channel.Writer.WriteAsync($"Msg {i}").ConfigureAwait(false);
      await Task.Delay(1_000).ConfigureAwait(false);
   }

   Console.WriteLine("Producer done.");
}

async Task Consume(int id) // We add an ID just to visualize which one processed a given message
{
   while (await channel.Reader.WaitToReadAsync().ConfigureAwait(false))
   {
      if (channel.Reader.TryRead(out var data))
      {
         Console.WriteLine($"Processed on {id}: {data}");
         // Simulate processing takes twice as long as producing
         await Task.Delay(2_000).ConfigureAwait(false);
      }
   }

   Console.WriteLine($"Consumer {id} done.");
}