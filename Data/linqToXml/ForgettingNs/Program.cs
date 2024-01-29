using System.Xml.Linq;

XNamespace ns = "http://domain.com/xmlspace";

var data =
   new XElement(ns + "data",
      new XElement("customer", "Bloggs"),
      new XElement("purchase", "Bicycle")
   );
Console.WriteLine(data);

data =
   new XElement(ns + "data",
      new XElement(ns + "customer", "Bloggs"),
      new XElement(ns + "purchase", "Bicycle")
   );

var x = data.Element(ns + "customer"); // OK
var y = data.Element("customer");

Console.WriteLine(y);