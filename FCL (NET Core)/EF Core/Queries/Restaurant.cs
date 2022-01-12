public class Restaurant
{
   private readonly int _id;

   public Restaurant(string name, int id = default) => (Name, _id) = (name, id);

   public string Name { get; }

   public override string ToString() => $"{Name}, {_id}";
}