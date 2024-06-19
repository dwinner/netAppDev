namespace PropertyProxy;

public class Creature
{
   public readonly Property<int> Agility = new(10, nameof(AgilityProperty));

   public int AgilityProperty
   {
      get => Agility.Value;
      set => Agility.Value = value;
   }
}