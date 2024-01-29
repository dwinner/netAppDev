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

foreach (var e in bench.Elements())
{
   Console.WriteLine($"{e.Name}={e.Value}");
}