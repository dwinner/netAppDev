using System.Xml.Linq;

XNamespace ns = "http://domain.com/xmlspace";

var data =
   new XElement(ns + "data",
      new XElement(ns + "customer", "Bloggs"),
      new XElement(ns + "purchase", "Bicycle")
   );

Console.WriteLine(data);
Console.WriteLine(data.Element(ns + "customer"));