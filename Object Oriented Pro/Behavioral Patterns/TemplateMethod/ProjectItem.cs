namespace TemplateMethod
{
   public abstract class ProjectItem
   {
      public string Name { get; set; }

      public string Description { get; set; }

      public double Rate { get; set; }

      protected ProjectItem(string name, string description, double rate)
      {
         Name = name;
         Description = description;
         Rate = rate;
      }

      protected ProjectItem() { }

      public abstract double TimeRequired();

      public abstract double MaterialCost();

      public double CostEstimate()
      {
         return TimeRequired() * Rate + MaterialCost();
      }

      public override string ToString()
      {
         return Name;
      }
   }
}