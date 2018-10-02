namespace FunWithGenericCollections
{
   public class Person
   {
      public int Age { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }

      public override string ToString()
      {
         return string.Format("Name: {0} {1}, Age: {2}",
             FirstName, LastName, Age);
      }
   }
}
