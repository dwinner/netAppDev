using System;
using System.Linq;
using System.Xml.Linq;

namespace VisitorViaDLR
{
   public class ToXElementPersonVisitor : PersonVisitor<XElement>
   {
      protected override XElement Visit(Person aPerson)
         => new XElement(
            nameof(Person),
            new XAttribute(nameof(Type), aPerson.GetType().Name),
            new XElement(nameof(Person.FirstName), aPerson.FirstName),
            new XElement(nameof(Person.LastName), aPerson.LastName),
            aPerson.Friends.Select(DynamicVisit));

      protected override XElement Visit(Customer aCustomer) // Specialized logic for customers
      {
         var xElement = base.Visit(aCustomer);
         xElement.Add(new XElement(nameof(Customer.CreditLimit), aCustomer.CreditLimit));
         return xElement;
      }

      protected override XElement Visit(Employee anEmployee) // Specialized logic for employees
      {
         var xElement = base.Visit(anEmployee);
         xElement.Add(new XElement(nameof(Employee.Salary), anEmployee.Salary));
         return xElement;
      }
   }
}