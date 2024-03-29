﻿using BookServiceClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
   .ConfigureServices((context, services) =>
   {
      var bookApiSettings = context.Configuration.GetSection("BooksService");
      services.Configure<BooksApiClientOptions>(bookApiSettings);
      services.AddHttpClient<BooksApiClient>(config =>
      {
         var baseAddress = context.Configuration.GetSection("BooksService")["BaseAddress"]
                           ?? "https://localhost:5001";
         config.BaseAddress = new Uri(baseAddress);
      });
   }).Build();

Console.WriteLine("Client - press return to continue");
Console.ReadLine();

using var scope = host.Services.CreateScope();

var client = scope.ServiceProvider.GetRequiredService<BooksApiClient>();
await client.ReadChaptersAsync().ConfigureAwait(false);
await client.ReadChapterAsync().ConfigureAwait(false);
await client.ReadNotExistingChapterAsync().ConfigureAwait(false);
await client.AddChapterAsync().ConfigureAwait(false);
await client.UpdateChapterAsync().ConfigureAwait(false);
await client.RemoveChapterAsync().ConfigureAwait(false);