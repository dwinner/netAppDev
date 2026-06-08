var builder = DistributedApplication.CreateBuilder(args);

var carvedRockDb = builder.AddPostgres("postgres")
    .AddDatabase("CarvedRockPostgres");

var idsrv = builder.AddProject<Projects.CarvedRock_Identity>("carvedrock-identity");
var idEndpoint = idsrv.GetEndpoint("https");

var api = builder.AddProject<Projects.CarvedRock_Api>("carvedrock-api")
    .WithEnvironment("Auth__Authority", idEndpoint)
    .WithReference(carvedRockDb);

var smtp = builder.AddSmtp4Dev("SmtpUri");

builder.AddProject<Projects.CarvedRock_WebApp>("carvedrock-webapp")
    .WithEnvironment("Auth__Authority", idEndpoint)
    //.WithReference(idsrv) // doesn't really work since Authority is a string
    //.WithEnvironment("CarvedRock__ApiBaseUrl", api.GetEndpoint("https"));
    .WithReference(api) // alternative to above
    .WithReference(smtp);
    
builder.Build().Run();
