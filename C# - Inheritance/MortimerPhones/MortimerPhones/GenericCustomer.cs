namespace MortimerPhones
{
   public abstract class GenericCustomer
   {
      public string Name { get; set; }

      protected GenericCustomer()
      {
         Name = "<no name>";
      }

      protected GenericCustomer(string name)
      {
         Name = name;
      }
   }
}