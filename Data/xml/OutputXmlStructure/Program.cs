using System.Xml;

const string customerXml = "customers.xml";

var settings = new XmlReaderSettings
{
   IgnoreWhitespace = true
};

using var reader = XmlReader.Create(customerXml, settings);
while (reader.Read())
{
   Console.Write(new string(' ', reader.Depth * 2)); // Write indentation
   Console.Write(reader.NodeType.ToString());

   switch (reader.NodeType)
   {
      case XmlNodeType.Element:
      case XmlNodeType.EndElement:
         Console.Write(" Name=" + reader.Name);
         break;
      case XmlNodeType.Text:
         Console.Write(" Value=" + reader.Value);
         break;
   }

   Console.WriteLine();
}