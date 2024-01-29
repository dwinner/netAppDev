using System.Xml.Linq;

var e1 = new XElement("test", "Hello");
e1.Add("World");
var e2 = new XElement("test", "Hello", "World");
var e3 = new XElement("test", new XText("Hello"), new XText("World"));

Console.WriteLine(e1);
Console.WriteLine(e2);
Console.WriteLine(e3);
Console.WriteLine(e1.Nodes().Count());
Console.WriteLine(e2.Nodes().Count());
Console.WriteLine(e3.Nodes().Count());