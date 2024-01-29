using System.Xml.Linq;

var items =
   new XElement("items",
      new XElement("one"),
      new XElement("three")
   );

Console.WriteLine(items);

items.FirstNode?.AddAfterSelf(new XElement("two"));
Console.WriteLine(items);