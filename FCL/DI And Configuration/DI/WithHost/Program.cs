﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
   .ConfigureServices(services =>
   {
      services.AddSingleton<IGreetingService, GreetingService>();
      services.AddTransient<HomeController>();
   }).Build();

var controller = host.Services.GetRequiredService<HomeController>();
string result = controller.Hello("Matthias");
Console.WriteLine(result);