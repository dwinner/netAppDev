using System.Xml;

var settings = new XmlReaderSettings
{
   IgnoreWhitespace = true
};

const string customerXml = "customerCredit.xml";
using var reader = XmlReader.Create(customerXml, settings);

reader.MoveToContent(); // Skip over the XML declaration
reader.ReadStartElement("customer");
var firstName = reader.ReadElementContentAsString("firstname", "");
var lastName = reader.ReadElementContentAsString("lastname", "");
var creditLimit = reader.ReadElementContentAsDecimal("creditlimit", "");

reader.MoveToContent(); // Skip over that pesky comment
reader.ReadEndElement(); // Read the closing customer tag

Console.WriteLine($"{firstName} {lastName} credit limit: {creditLimit}");