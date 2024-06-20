namespace MethodChain;

public class Creature(string name, int attack, int defense)
{
   public string Name { get; } = name;

   public int Attack { get; set; } = attack;

   public int Defense { get; set; } = defense;

   public override string ToString() =>
      $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
}