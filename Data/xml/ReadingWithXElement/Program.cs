using System.Xml;
using System.Xml.Linq;

var settings = new XmlReaderSettings
{
   IgnoreWhitespace = true
};

using var reader = XmlReader.Create("logfile.xml", settings);
reader.ReadStartElement("log");
while (reader.Name == "logentry")
{
   var logEntry = (XElement)XNode.ReadFrom(reader);
   var id = (int)logEntry.Attribute("id")!;
   var date = (DateTime)logEntry.Element("date")!;
   var source = (string)logEntry.Element("source")!;
   Console.WriteLine($"{id} {date} {source}");
}

reader.ReadEndElement();