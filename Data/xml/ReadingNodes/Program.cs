using System.Xml;

var settings = new XmlReaderSettings
{
   IgnoreWhitespace = true,
   DtdProcessing = DtdProcessing.Parse // Required to read DTDs
};

const string customersXml = "customerWithCDATA.xml";
using var reader = XmlReader.Create(customersXml, settings);
while (reader.Read())
{
   Console.Write(reader.NodeType.ToString().PadRight(17, '-'));
   Console.Write("> ".PadRight(reader.Depth * 3));

   switch (reader.NodeType)
   {
      case XmlNodeType.Element:
      case XmlNodeType.EndElement:
         Console.WriteLine(reader.Name);
         break;

      case XmlNodeType.Text:
      case XmlNodeType.CDATA:
      case XmlNodeType.Comment:
      case XmlNodeType.XmlDeclaration:
         Console.WriteLine(reader.Value);
         break;

      case XmlNodeType.DocumentType:
         Console.WriteLine($"{reader.Name} - {reader.Value}");
         break;
   }
}