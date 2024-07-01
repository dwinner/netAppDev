using Fundamentals;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var types = new Types();
builder.Services.AddSingleton<ITypes>(types);
builder.Services.AddBindingsByConvention(types);

var app = builder.Build();
app.UseRouting();
app.MapControllers();
//app.UseEndpoints(routeBuilder => routeBuilder.MapControllers());
app.Run();