namespace InheritanceValidationSample
{
   public class Organization : IContact
   {
      public string Name { get; set; }

      public string Email { get; set; }

      public string Headquarters { get; set; }
   }
}