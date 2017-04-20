namespace Visitor
{
   public class Employee : IElement
   {
      public Employee(string name, double income, int vacationDays)
      {
         Name = name;
         Income = income;
         VacationDays = vacationDays;
      }

      public string Name { get; }

      public double Income { get; set; }

      public int VacationDays { get; }

      public void Accept(IVisitor visitor) => visitor.Visit(this);
   }
}