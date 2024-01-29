using System.Xml.Linq;

var e = new XElement("date", DateTime.Now);
e.SetValue(DateTime.Now.AddDays(1));
Console.WriteLine(e.Value);