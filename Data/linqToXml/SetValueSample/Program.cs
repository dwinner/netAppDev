using System.Xml.Linq;

XElement settings =
   new XElement("settings",
      new XElement("timeout", 30)
   );

Console.WriteLine(settings);
settings.SetValue("blah");
Console.WriteLine(settings);