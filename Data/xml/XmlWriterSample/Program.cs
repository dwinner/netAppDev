using System.Xml;

var settings = new XmlWriterSettings
{
   Indent = true
};

using var writer = XmlWriter.Create("foo.xml", settings);

writer.WriteStartElement("customer");
writer.WriteAttributeString("id", "1");
writer.WriteAttributeString("status", "archived");
writer.WriteElementString("firstname", "Jim");
writer.WriteElementString("lastname", "Bo");
writer.WriteEndElement();

writer.Dispose();
var xml = File.ReadAllText("foo.xml");
Console.WriteLine(xml);