namespace ProductLib
{
   public class Product
   {
      public int Id { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public override string ToString()
      {
         return string.Format("Id: {0}, FirstName: {1}, LastName: {2}", Id, FirstName, LastName);
      }
   }
}