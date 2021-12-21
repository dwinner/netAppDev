using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
   .ConfigureAppConfiguration(config =>
   {
      config.SetBasePath(Directory.GetCurrentDirectory());
      config.AddJsonFile("customconfigurationfile.json", true);
      config.AddJsonFile("dynamicchanges.json", true, true);
   }).ConfigureServices(services =>
   {
      services.AddTransient<ConfigurationSampleService>();
      services.AddTransient<EnvironmentSampleService>();
   }).Build();

var service = host.Services.GetRequiredService<ConfigurationSampleService>();
service.ShowConfiguration();
service.ShowTypedConfiguration();
service.ShowCustomConfiguration();
service.ShowDynamicChangedValue();
service.ShowDynamicValue();

var service2 = host.Services.GetRequiredService<EnvironmentSampleService>();
service2.ShowHostEnvironment();