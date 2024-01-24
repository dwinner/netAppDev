using System.Xml.Linq;

var lastName = new XElement("lastname", "Bloggs");
lastName.Add(new XComment("nice name"));

var customer = new XElement("customer");
customer.Add(new XAttribute("id", 123));
customer.Add(new XElement("firstname", "Joe"));
customer.Add(lastName);

Console.WriteLine(customer);