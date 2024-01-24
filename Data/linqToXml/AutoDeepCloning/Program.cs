using System.Xml.Linq;

var address =
   new XElement("address",
      new XElement("street", "Lawley St"),
      new XElement("town", "North Beach")
   );

var customer1 = new XElement("customer1", address);
var customer2 = new XElement("customer2", address);

customer1.Element("address")!.Element("street")!.Value = "Another St";
Console.WriteLine(customer2.Element("address")!.Element("street")!.Value);

Console.WriteLine(customer1);
Console.WriteLine(customer2);