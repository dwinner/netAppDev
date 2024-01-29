using System.Xml.Linq;

var settings = new XElement("settings");

settings.SetElementValue("timeout", 30);
Console.WriteLine(settings);

settings.SetElementValue("timeout", 60);
Console.WriteLine(settings);