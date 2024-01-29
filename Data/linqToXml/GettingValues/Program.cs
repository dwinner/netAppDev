using System.Xml.Linq;

var e = new XElement("now", DateTime.Now);
var dt = (DateTime)e;

var a = new XAttribute("resolution", 1.234);
var res = (double)a;

Console.WriteLine(dt);
Console.WriteLine(res);