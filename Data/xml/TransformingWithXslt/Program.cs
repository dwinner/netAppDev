using System.Xml.Xsl;

var transform = new XslCompiledTransform();
transform.Load("customer.xslt");
transform.Transform("customer.xml", "customer.xhtml");

var content = File.ReadAllText("customer.xhtml");
Console.WriteLine(content);