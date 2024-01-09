namespace CovariantReturn;

internal static class Program
{
   private static void Main()
   {
      var mansion1 = new House { Name = "McMansion", Mortgage = 250000 };
      var mansion2 = mansion1.Clone();

      Console.WriteLine(mansion2);
   }
}

public class Asset
{
   public string Name;
   public virtual Asset Clone() => new() { Name = Name };
}

public class House : Asset
{
   public decimal Mortgage;

   // We can return House when overriding:
   public override House Clone() => new() { Name = Name, Mortgage = Mortgage };
}