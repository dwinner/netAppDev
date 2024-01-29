using System.Xml;
using System.Xml.Schema;

var settings = new XmlReaderSettings
{
   ValidationType = ValidationType.Schema
};
settings.Schemas.Add(null, "customers.xsd");

using var reader = XmlReader.Create("customer.xml", settings);
try
{
   while (reader.Read())
   {
   }
}
catch (XmlSchemaValidationException ex)
{
   Console.WriteLine($"Invalid XML according to schema: {ex.Message}");
}

//"Finished processing XML".Dump();