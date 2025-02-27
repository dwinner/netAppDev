﻿using Books.Data.Models;
using Books.Initializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
   .ConfigureServices((context, services) =>
   {
      var booksConnection = context.Configuration.GetConnectionString("BooksConnection");
      services.AddDbContext<BooksContext>(options => { options.UseSqlServer(booksConnection); });

      services.AddTransient<SampleChapters>();
   })
   .Build();

using var scope = host.Services.CreateScope();
var booksContext = scope.ServiceProvider.GetRequiredService<BooksContext>();
await booksContext.Database.EnsureCreatedAsync().ConfigureAwait(false);

var sampledata = scope.ServiceProvider.GetRequiredService<SampleChapters>();
var chapters = sampledata.GetSampleChapters();
await booksContext.Chapters.AddRangeAsync(chapters).ConfigureAwait(false);
await booksContext.SaveChangesAsync().ConfigureAwait(false);