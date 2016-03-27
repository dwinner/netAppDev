namespace TemplateMethod
{
   public class Deliverable : ProjectItem
   {
      public double CostMaterial { get; set; }

      public double ProductionTime { get; set; }

      public Deliverable() { }

      public Deliverable(string name, string description, double rate, double materialCost, double productionTime)
         : base(name, description, rate)
      {
         CostMaterial = materialCost;
         ProductionTime = productionTime;
      }

      public override double TimeRequired()
      {
         return ProductionTime;
      }

      public override double MaterialCost()
      {
         return CostMaterial;
      }
   }
}