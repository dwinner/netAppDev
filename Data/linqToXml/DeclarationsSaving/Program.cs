using System.Xml.Linq;

var doc =
   new XDocument(
      new XDeclaration("1.0", "utf-16", "yes"),
      new XElement("test", "data")
   );

var tempPath = Path.Combine(Path.GetTempPath(), "test.xml");
doc.Save(tempPath);
var content = File.ReadAllText(tempPath);
Console.WriteLine(content);