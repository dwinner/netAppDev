﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HttpClientSample
{
   public class NamedClientSample
   {
      private readonly HttpClient _httpClient;
      private readonly ILogger _logger;
      private readonly string _url;

      public NamedClientSample(
         IOptions<HttpClientSampleOptions> options,
         IHttpClientFactory httpClientFactory,
         ILogger<HttpClientSamples> logger)
      {
         _logger = logger;
         _url = options.Value.InvalidUrl ?? "localhost:5052";
         _httpClient = httpClientFactory.CreateClient("racersClient");
      }

      public async Task RunAsync()
      {
         try
         {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/racers");
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
         }
         catch (HttpRequestException ex)
         {
            _logger.LogError(ex, ex.Message);
         }
      }
   }
}