using System.Xml.Linq;

// 1. Simple removing
var contacts = XElement.Parse("""
                              <contacts>
                              	<customer name='Mary'/>
                              	<customer name='Chris' archived='true'/>
                              	<supplier name='Susan'>
                              		<phone archived='true'>012345678<!--confidential--></phone>
                              	</supplier>
                              </contacts>
                              """);

//Console.WriteLine(contacts);

contacts.Elements()
   .Where(e => (bool?)e.Attribute("archived") == true)
   .Remove();

//Console.WriteLine(contacts);

// 2. Recursive
contacts = XElement.Parse("""

                          <contacts>
                          	<customer name='Mary'/>
                          	<customer name='Chris' archived='true'/>
                          	<supplier name='Susan'>
                          		<phone archived='true'>012345678<!--confidential--></phone>
                          	</supplier>
                          </contacts>
                          """
);

//Console.WriteLine(contacts);

contacts.Descendants()
   .Where(e => (bool?)e.Attribute("archived") == true)
   .Remove();

//Console.WriteLine(contacts);

// 3. Resursive of type
contacts = XElement.Parse("""

                          <contacts>
                          	<customer name='Mary'/>
                          	<customer name='Chris' archived='true'/>
                          	<supplier name='Susan'>
                          		<phone archived='true'>012345678<!--confidential--></phone>
                          	</supplier>
                          </contacts>
                          """);

Console.WriteLine(contacts);

contacts.Elements()
   .Where(
      e => e.DescendantNodes().OfType<XComment>().Any(c => c.Value == "confidential")
   )
   .Remove();

Console.WriteLine(contacts);