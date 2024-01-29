using System.Diagnostics;
using System.Xml.Linq;

var styleInstruction = new XProcessingInstruction(
   "xml-stylesheet", "href='styles.css' type='text/css'"
);

var docType = new XDocumentType("html",
   "-//W3C//DTD XHTML 1.0 Strict//EN",
   "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", null);

XNamespace ns = "http://www.w3.org/1999/xhtml";
var root =
   new XElement(ns + "html",
      new XElement(ns + "head",
         new XElement(ns + "title", "An XHTML page")),
      new XElement(ns + "body",
         new XElement(ns + "h1", "This is a heading."),
         new XElement(ns + "p", "This is some content."))
   );

var doc =
   new XDocument(
      new XDeclaration("1.0", "utf-8", "no"),
      new XComment("Reference a stylesheet"),
      styleInstruction,
      docType,
      root
   );

var tempPath = Path.Combine(Path.GetTempPath(), "sample.html");
doc.Save(tempPath);

// This will display the page in your default browser
Process.Start(new ProcessStartInfo(tempPath) { UseShellExecute = true });
var content = File.ReadAllText(tempPath);
Console.WriteLine(content);

// Root element's local name
var localName = doc.Root!.Name.LocalName;
Console.WriteLine(localName);

var bodyNode = doc.Root.Element(ns + "body");
Console.WriteLine(bodyNode!.Document == doc);

Console.WriteLine(doc.Root.Parent == null);

foreach (var node in doc.Nodes())
{
   Console.Write(node.Parent == null);
}