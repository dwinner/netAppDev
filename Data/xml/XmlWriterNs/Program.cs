using System.Xml;

var settings = new XmlWriterSettings
{
   Indent = true
};

using var writer = XmlWriter.Create("foo.xml", settings);

writer.WriteStartElement("o", "customer", "http://oreilly.com");
writer.WriteElementString("o", "firstname", "http://oreilly.com", "Jim");
writer.WriteElementString("o", "lastname", "http://oreilly.com", "Bo");

writer.WriteEndElement();

writer.Dispose();
var xml = File.ReadAllText("foo.xml");
Console.WriteLine(xml);