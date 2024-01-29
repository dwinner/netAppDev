using System.Xml.Linq;

var e = new XElement("test");

e.AddAnnotation("Hello");
Console.WriteLine(e.Annotation<string>());

e.RemoveAnnotations<string>();
Console.WriteLine(e.Annotation<string>() ?? "none");