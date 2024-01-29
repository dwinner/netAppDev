using System.Xml.Linq;

var items = XElement.Parse("""

                           <items>
                           	<one/><two/><three/>
                           </items>
                           """
);

Console.WriteLine(items);

items.FirstNode?.ReplaceWith(new XComment("One was here"));

Console.WriteLine(items);