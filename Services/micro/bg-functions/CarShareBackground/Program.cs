using CarShareBackground;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
   .ConfigureFunctionsWebApplication()
   .ConfigureAppConfiguration(config => { config.AddUserSecrets<ProcessDriversLicensePhoto>(true, false); })
   .Build();

host.Run();