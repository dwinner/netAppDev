using System.Xml.Linq;
using MoreLinq;

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

var toolboxWithNailgun =
   from toolbox in bench.Elements()
   where toolbox.Elements().Any(tool => tool.Value == "Nailgun")
   select toolbox.Value;

var handTools =
   from toolbox in bench.Elements()
   from tool in toolbox.Elements()
   where tool.Name == "handtool"
   select tool.Value;

var toolboxCount = bench.Elements("toolbox").Count();

var handTools2 =
   from tool in bench.Elements("toolbox").Elements("handtool")
   select tool.Value.ToUpper();

toolboxWithNailgun.ForEach(element => Console.WriteLine(element));
Console.WriteLine();

handTools.ForEach(element => Console.WriteLine(element));
Console.WriteLine();

Console.WriteLine(toolboxCount);

handTools2.ForEach(element => Console.WriteLine(element));

var count = bench.Descendants("handtool").Count();
Console.WriteLine(count);

foreach (var node in bench.DescendantNodes())
{
   Console.WriteLine(node.ToString(SaveOptions.DisableFormatting));
}

(from c in bench.DescendantNodes().OfType<XComment>()
   where c.Value.Contains("careful")
   orderby c.Value
   select c.Value).ForEach(element => Console.WriteLine(element));