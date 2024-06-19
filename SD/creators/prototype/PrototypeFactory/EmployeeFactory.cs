namespace PrototypeFactory;

public class EmployeeFactory
{
   private static readonly Person Main = new(null!, new Address("123 East Dr", "London", 0));
   private static readonly Person Aux = new(null!, new Address("123B East Dr", "London", 0));

   private static Person NewEmployee(Person proto, string name, int suite)
   {
      var copy = proto.DeepCopy();
      copy.Name = name;
      copy.Address.Suite = suite;
      return copy;
   }

   public static Person NewMainOfficeEmployee(string name, int suite) =>
      NewEmployee(Main, name, suite);

   public static Person NewAuxOfficeEmployee(string name, int suite) =>
      NewEmployee(Aux, name, suite);
}