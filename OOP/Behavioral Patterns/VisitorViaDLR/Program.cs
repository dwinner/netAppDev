/**
 * Visitor implementation via DLR
 */

using System;
using System.Xml.Linq;

namespace VisitorViaDLR
{
   internal static class Program
   {
      private static void Main()
      {
         var customer = new Customer
         {
            FirstName = "Joe",
            LastName = "Bloggs",
            CreditLimit = 123,
            Friends =
            {
               new Employee
               {
                  FirstName = "Sue",
                  LastName = "Brown",
                  Salary = 50000
               }
            }
         };
         PersonVisitor<XElement> visitor = new ToXElementPersonVisitor();
         var element = visitor.DynamicVisit(customer);
         Console.WriteLine(element);
      }
   }
}