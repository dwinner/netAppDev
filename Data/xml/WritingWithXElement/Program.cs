using System.Xml;
using System.Xml.Linq;

var settings = new XmlWriterSettings { Indent = true }; // Otherwise the XML is written as one very long line.
// Saves space but makes it more difficult for humans.

using var writer = XmlWriter.Create("logfile.xml", settings);

writer.WriteStartElement("log");

for (var i = 0; i < 1_000_000; i++)
{
   var element = new XElement("logentry",
      new XAttribute("id", i),
      new XElement("date", DateTime.Today.AddDays(-1)),
      new XElement("source", "test"));
   element.WriteTo(writer);
}

writer.WriteEndElement();

writer.Dispose();

using var reader = File.OpenText("logfile.xml");
for (var i = 0; i < 10; i++)
{
   Console.WriteLine(reader.ReadLine());
}