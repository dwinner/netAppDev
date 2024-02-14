using System.Xml.Linq;

namespace VisitorDispatch;

internal class ToXElementPersonVisitor : PersonVisitor<XElement>
{
   protected override XElement Visit(Person aPerson)
   {
      return new XElement("Person", new XAttribute("Type", aPerson.GetType().Name),
         new XElement("FirstName", aPerson.FirstName),
         new XElement("LastName", aPerson.LastName),
         aPerson.Friends.Select(DynamicVisit)
      );
   }

   protected override XElement Visit(Customer aCustomer)
   {
      var xElement = base.Visit(aCustomer);
      xElement.Add(new XElement("CreditLimit", aCustomer.CreditLimit));
      return xElement;
   }

   protected override XElement Visit(Employee anEmployee)
   {
      var xElement = base.Visit(anEmployee);
      xElement.Add(new XElement("Salary", anEmployee.Salary));
      return xElement;
   }
}