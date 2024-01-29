using System.Xml.Linq;

XNamespace ns = "http://domain.com/xmlspace";

var data =
   new XElement(ns + "data",
      new XElement("customer", "Bloggs"),
      new XElement("purchase", "Bicycle")
   );

Console.WriteLine(data);

foreach (var xElement in data.DescendantsAndSelf())
{
   if (xElement.Name.Namespace == string.Empty)
   {
      xElement.Name = ns + xElement.Name.LocalName;
   }
}

Console.WriteLine(data);