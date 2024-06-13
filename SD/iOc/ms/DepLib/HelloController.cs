namespace DepLib;

public class HelloController(IGreetingService greetingService)
{
   public string Action(string name) => greetingService.Greeting(name);
}