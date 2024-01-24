using System.Xml.Linq;

var config = XElement.Parse(
   """
   <configuration>
   	<client enabled='true'>
   		<timeout>30</timeout>
   	</client>
   </configuration>
   """);

foreach (var child in config.Elements())
{
   Console.WriteLine(child.Name);
}

var client = config.Element("client")!;

var enabled = (bool)client.Attribute("enabled")!; // Read attribute
Console.WriteLine(enabled);

client.Attribute("enabled")?.SetValue(!enabled); // Update attribute

var timeout = (int)client.Element("timeout")!; // Read element
Console.WriteLine(timeout);

client.Element("timeout")?.SetValue(timeout * 2); // Update element

client.Add(new XElement("retries", 3)); // Add new elememt

Console.WriteLine(config);