using static System.Console;

namespace Visitor
{
   public class IncomeVisitor : IVisitor
   {
      public void Visit(IElement element)
      {
         var employee = element as Employee;
         if (employee != null)
         {
            employee.Income *= 1.10;
            WriteLine("{0} {1}'s new income: {2:C}",
               employee.GetType().Name,
               employee.Name,
               employee.Income);
         }
      }
   }
}