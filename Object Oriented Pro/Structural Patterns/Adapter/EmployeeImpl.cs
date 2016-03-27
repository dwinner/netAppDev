namespace Adapter
{
   public class EmployeeImpl : IEmployee
   {
      private readonly int _id;

      public EmployeeImpl(int id, string firstName, string lastName)
      {
         _id = id;
         FirstName = firstName;
         LastName = lastName;
      }

      public int Id
      {
         get { return _id; }
      }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public override string ToString()
      {
         return string.Format("{0}, {1}, {2}", _id, FirstName, LastName);
      }
   }
}