namespace Visitor
{
   public class Employee : IElement
   {
      public string Name { get; set; }

      public double Income { get; set; }

      public int VacationDays { get; set; }

      public Employee(string name, double income, int vacationDays)
      {
         Name = name;
         Income = income;
         VacationDays = vacationDays;
      }

      public void Accept(IVisitor visitor)
      {
         visitor.Visit(this);
      }
   }
}