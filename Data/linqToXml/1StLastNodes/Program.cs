using System.Xml.Linq;

var bench =
   new XElement("bench",
      new XElement("toolbox",
         new XElement("handtool", "Hammer"),
         new XElement("handtool", "Rasp")
      ),
      new XElement("toolbox",
         new XElement("handtool", "Saw"),
         new XElement("powertool", "Nailgun")
      ),
      new XComment("Be careful with the nailgun")
   );

var firstNode = bench.FirstNode;
var lastNode = bench.LastNode;

Console.WriteLine(firstNode);
Console.WriteLine(lastNode);

foreach (var node in bench.Nodes())
{
   Console.WriteLine(node.ToString(SaveOptions.DisableFormatting) + ".");
}