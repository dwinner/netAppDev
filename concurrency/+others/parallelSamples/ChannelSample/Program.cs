﻿using System;
using System.Threading.Channels;
using System.Threading.Tasks;

await UsingTheUnboundedChannelAsync();
await UsingTheBoundedChannelAsync();

async Task UsingTheUnboundedChannelAsync()
{
   var channel = Channel.CreateUnbounded<SomeData>(
      new UnboundedChannelOptions
      {
         SingleReader = false,
         SingleWriter = true
      });

   Console.WriteLine("Using the unbounded channel");

   var t1 = ChannelSample.WriteSomeDataAsync(channel.Writer);
   var t2 = ChannelSample.ReadSomeDataAsync(channel.Reader);

   await Task.WhenAll(t1, t2);

   Console.WriteLine();
}

async Task UsingTheBoundedChannelAsync()
{
   var channel = Channel.CreateBounded<SomeData>(
      new BoundedChannelOptions(10)
      {
         FullMode = BoundedChannelFullMode.Wait,
         SingleWriter = true
      });

   Console.WriteLine("Using the bounded channel");

   var t1 = ChannelSample.WriteSomeDataWithTryWriteAsync(channel.Writer);
   var t2 = ChannelSample.ReadSomeDataUsingAsyncStreams(channel.Reader);

   await Task.WhenAll(t1, t2);

   Console.WriteLine("bye");
   Console.WriteLine();
}


public record SomeData(string Text, int Number);