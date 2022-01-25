public class Restaurant
{
   private readonly int _id;
   private readonly string _name;

   public Restaurant(string name, int id = default) => (_name, _id) = (name, id);

   public string Name => _name;

   public override string ToString() => $"{Name}, {_id}";
}