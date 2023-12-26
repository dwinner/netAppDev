using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Id = Microsoft.Identity.Client;

internal class Runner
{
   private readonly string _clientId;
   private readonly ILogger _logger;
   private readonly string _tenantId;
   private Id.IPublicClientApplication? _clientApp;

   public Runner(IConfiguration configuration, ILogger<Runner> logger)
   {
      _clientId = configuration["ClientId"] ?? throw new InvalidOperationException("Configure a ClientId");
      _tenantId = configuration["TenantId"] ?? throw new InvalidOperationException("Configure a TenantId");
      _logger = logger;
   }

   public void Init()
   {
      void LogCallback(Id.LogLevel level, string message, bool containsPii)
         => _logger.Log(level.ToLogLevel(), message);

      _clientApp = Id.PublicClientApplicationBuilder
         .Create(_clientId)
         .WithLogging(LogCallback, Id.LogLevel.Verbose)
         .WithAuthority(Id.AzureCloudInstance.AzurePublic, _tenantId)
         .WithRedirectUri("http://localhost")
         .Build();
   }

   public async Task LoginAsync()
   {
      if (_clientApp is null)
      {
         throw new InvalidOperationException("Invoke Init before calling this method");
      }

      try
      {
         string[] scopes = { "user.read" };
         var accounts = await _clientApp.GetAccountsAsync();
         var firstAccount = accounts.FirstOrDefault();
         if (firstAccount is not null)
         {
            Id.AuthenticationResult result = await _clientApp.AcquireTokenSilent(scopes, firstAccount)
               .ExecuteAsync();
            ShowAuthenticationResult(result);
         }
         else
         {
            Id.AuthenticationResult result = await _clientApp.AcquireTokenInteractive(scopes)
               .ExecuteAsync();
            ShowAuthenticationResult(result);
         }
      }
      catch (Exception ex)
      {
         _logger.LogError(ex, ex.Message);
         throw;
      }
   }

   private void ShowAuthenticationResult(Id.AuthenticationResult result)
   {
      Console.WriteLine($"Id token: {result.IdToken[..20]}");
      Console.WriteLine($"Access token: {result.AccessToken[..20]}");
      Console.WriteLine($"Username: {result.Account.Username}");
      Console.WriteLine($"Environment: {result.Account.Environment}");
      Console.WriteLine($"Account Id: {result.Account.HomeAccountId}");
      foreach (var scope in result.Scopes)
      {
         Console.WriteLine($"scope: {scope}");
      }
   }
}