using System.Xml.Linq;

XNamespace ns1 = "http://domain.com/space1";
XNamespace ns2 = "http://domain.com/space2";

var mix =
   new XElement(ns1 + "data",
      new XElement(ns2 + "element", "value"),
      new XElement(ns2 + "element", "value"),
      new XElement(ns2 + "element", "value")
   );

// Without prefixes
Console.WriteLine(mix);

mix.SetAttributeValue(XNamespace.Xmlns + "ns1", ns1);
mix.SetAttributeValue(XNamespace.Xmlns + "ns2", ns2);

// With prefixes
Console.WriteLine(mix);