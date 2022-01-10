using System;

public class Restaurant
{
   private readonly Guid _id;
   public Restaurant(string name, Guid id = default) => (Name, _id) = (name, id);
   public string Name { get; }

   public override string ToString() => $"{Name}, {_id}";
}