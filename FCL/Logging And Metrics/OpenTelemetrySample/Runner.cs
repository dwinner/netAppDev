﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LoggingSample
{
   internal class Runner
   {
      internal static readonly ActivitySource ActivitySource = new("LoggingSample.DistributedTracing");
      private readonly ILogger _logger;
      private readonly NetworkService _networkSevice;

      public Runner(NetworkService networkService, ILogger<Runner> logger)
      {
         _networkSevice = networkService;
         _logger = logger;
      }

      public async Task RunAsync()
      {
         using var activity = ActivitySource.StartActivity("Run");
         _logger.LogDebug("RunAsync started");
         var exit = false;
         do
         {
            Console.Write("Please enter a URI or enter to exit: ");
            var url = Console.ReadLine();
            using var urlActivity = ActivitySource.StartActivity("Starting URL Request");
            if (string.IsNullOrEmpty(url))
            {
               exit = true;
            }
            else
            {
               try
               {
                  Uri uri = new(url);
                  await _networkSevice.NetworkRequestSampleAsync(uri);
               }
               catch (UriFormatException ex)
               {
                  _logger.LogError(ex, ex.Message);
               }
            }
         } while (!exit);
      }
   }
}