using System;

public class Restaurant
{
   private readonly Guid _id;
   private string _name;

   public Restaurant(string name, Guid id = default) => (_name, _id) = (name, id);

   public string Name => _name;

   public override string ToString() => $"{Name}, {_id}";
}