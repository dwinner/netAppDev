namespace VisitorDispatch;

internal static class Program
{
   private static void Main()
   {
      var customer = new Customer
      {
         FirstName = "Joe",
         LastName = "Bloggs",
         CreditLimit = 123
      };

      customer.Friends.Add(new Employee
      {
         FirstName = "Sue",
         LastName = "Brown",
         Salary = 50000
      });
      var visitor = new ToXElementPersonVisitor();
      var xElement = visitor.DynamicVisit(customer);
      Console.WriteLine(xElement);
   }
}