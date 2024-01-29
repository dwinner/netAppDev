using System.Xml.Linq;

var summary =
   new XElement("summary",
      new XText("An XAttribute is "),
      new XElement("bold", "not"),
      new XText(" an XNode")
   );

Console.WriteLine(summary);