namespace TemplateMethod
{
   public class Deliverable : ProjectItem
   {
      public Deliverable(
         string name, string description, double rate,
         double materialCost, double productionTime)
         : base(name, description, rate)
      {
         CostMaterial = materialCost;
         ProductionTime = productionTime;
      }

      public double CostMaterial { get; }

      public double ProductionTime { get; }

      public override double TimeRequired() => ProductionTime;

      public override double MaterialCost() => CostMaterial;
   }
}