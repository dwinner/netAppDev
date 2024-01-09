namespace PrimaryCtorSample;

internal static class Program
{
   private static void Main(string[] args)
   {
      new Person1("Alice", "Jones").Print(); // Alice Jones
      new Person2("Alice", "Jones").Print(); // Alice JONES
      var p3 = new Person3("Alice", null); // throws ArgumentNullException
   }
}

internal class Person1(string firstName, string lastName)
{
   // Sometimes it’s useful to perform computation in field initializers: 
   public readonly string FullName = $"{firstName} {lastName}";

   public void Print() => Console.WriteLine(FullName);
}

internal class Person2(string firstName, string lastName)
{
   // Save an uppercase version of lastName to a field of the same name
   // (masking the original value):
   private readonly string _lastName = lastName.ToUpper();

   public void Print() => Console.WriteLine(firstName + " " + _lastName);
}

internal class Person3(string? firstName, string? lastName)
{
   // Validate lastName upon construction, ensuring that it cannot be null:
   private readonly string _lastName = lastName
                                       ?? throw new ArgumentNullException("lastName");

   public void Print() => Console.WriteLine(firstName + " " + _lastName);
}