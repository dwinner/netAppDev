namespace AdaptFactory
{
   public sealed class Employee
   {
      public int Id { get; set; }
      public string Name { get; set; }

      public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
   }
}