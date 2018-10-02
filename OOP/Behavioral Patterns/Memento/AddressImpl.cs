namespace Memento
{
   public class AddressImpl : IAddress
   {
      public string Type { get; set; }

      public string Description { get; set; }

      public string Street { get; set; }

      public string City { get; set; }

      public string State { get; set; }

      public string ZipCode { get; set; }

      public override string ToString() =>
         $"Type: {Type}, Description: {Description}, Street: {Street}, City: {City}, State: {State}, ZipCode: {ZipCode}";
   }
}