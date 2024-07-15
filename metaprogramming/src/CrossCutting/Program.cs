using CrossCutting;
using CrossCutting.Commands;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
   .AddControllers(mvcOptions => mvcOptions.Filters.Add<CommandActionFilter>());
builder.Services
   .AddAuthorizationBuilder()
   .AddPolicy($"{nameof(CrossCutting)}.{nameof(CrossCutting.Commands)}",
      policy => policy.Requirements.Add(new AdminForNamespace(nameof(CrossCutting))));
builder.Services
   .AddSingleton<IAuthorizationHandler, AdminForNamespaceHandler>()
   .AddSingleton<IAuthorizationMiddlewareResultHandler, CrossCuttingAuthorizationMiddlewareResultHandler>()
   .AddSingleton<IAuthorizationPolicyProvider, CrossCuttingPoliciesProvider>()
   .AddAuthentication(options => options.DefaultScheme = HardCodedAuthenticationHandler.SchemeName)
   .AddScheme<HardCodedAuthenticationOptions, HardCodedAuthenticationHandler>(
      HardCodedAuthenticationHandler.SchemeName, _ => { });

var app = builder.Build();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();