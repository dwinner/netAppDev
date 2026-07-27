using EfCrudSvc;
using EfCrudSvc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string connectionString = "Data Source=grpc.db";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connectionString));
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<UserApiService>();
app.MapGet("/",
   () =>
      "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();