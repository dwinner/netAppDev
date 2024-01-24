using System.Xml.Linq;

var customer = new XElement("customer", new XAttribute("id", 123),
   new XElement("firstname", "joe"),
   new XElement("lastname", "bloggs",
      new XComment("nice name")
   )
);

Console.WriteLine(customer);