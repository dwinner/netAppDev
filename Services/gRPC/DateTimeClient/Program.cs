using DateTimeClient;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7065");
var client = new Inviter.InviterClient(channel);
var response = await client.InviteAsync(new Request { Name = "Tom" });
var invitation = response.Invitation;
var dateTime = response.Start.ToDateTime();
var timeSpan = response.Duration.ToTimeSpan();

Console.WriteLine(invitation);
Console.WriteLine($"Start: {dateTime:dd.Mm HH:mm}. Duration: {timeSpan.TotalHours} hours");

Console.ReadKey();