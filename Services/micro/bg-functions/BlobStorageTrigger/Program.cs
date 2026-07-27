using BlobStorageTrigger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
   .ConfigureFunctionsWebApplication()
   .ConfigureAppConfiguration(config => { config.AddUserSecrets<FunctionBlobTrigger>(true, false); })
   .Build();

host.Run();