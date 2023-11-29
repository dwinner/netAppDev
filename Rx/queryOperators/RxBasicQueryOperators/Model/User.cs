namespace RxBasicQueryOperators.Model;

internal class User
{
   public string Name { get; set; }
   public string Id { get; set; }

   public override string ToString() => $"Id={Id} Name:{Name}";
}