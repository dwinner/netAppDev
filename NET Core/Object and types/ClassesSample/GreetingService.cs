internal class GreetingService
{
   public string Greet(Person person) => $"Hello, {person.FirstName}!";
}