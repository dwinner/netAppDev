using Microsoft.Extensions.Options;

public class GreetingService : IGreetingService
{
   private readonly string? _from;

   public GreetingService(IOptions<GreetingServiceOptions> options) => _from = options.Value.From;

   public string Greet(string name) => $"Hello, {name}! Greetings from {_from}";
}