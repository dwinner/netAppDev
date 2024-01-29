using System.Text;
using System.Xml;
using System.Xml.Linq;

var doc =
   new XDocument(
      new XDeclaration("1.0", "utf-8", "yes"),
      new XElement("test", "data")
   );

var output = new StringBuilder();
var settings = new XmlWriterSettings { Indent = true };

using (var xw = XmlWriter.Create(output, settings))
{
   doc.Save(xw);
}

// Notice the encoding is utf-16 and not utf-8
Console.WriteLine(output.ToString());