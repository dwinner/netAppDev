namespace TemplateMethod
{
   public abstract class ProjectItem
   {
      protected ProjectItem(
         string name, string description, double rate)
      {
         Name = name;
         Description = description;
         Rate = rate;
      }

      public string Name { get; }

      public string Description { get; }

      public double Rate { get; }

      public abstract double TimeRequired();

      public abstract double MaterialCost();

      public double CostEstimate()
         => TimeRequired() * Rate + MaterialCost();

      public override string ToString() => Name;
   }
}