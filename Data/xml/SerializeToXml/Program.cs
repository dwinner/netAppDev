using System.Xml;

namespace SerializeToXml;

internal static class Program
{
   private static void Main()
   {
      SerializeContacts();
      var contacts = DeserializeContacts();
      Console.WriteLine(contacts);
   }

   private static void SerializeContacts()
   {
      var settings = new XmlWriterSettings
      {
         Indent = true // To make visual inspection easier
      };

      using var writer = XmlWriter.Create("contacts.xml", settings);
      var contacts = new Contacts
      {
         Customers = new List<Customer>
         {
            new() { Id = 1, FirstName = "Sara", LastName = "Wells" },
            new() { Id = 2, FirstName = "Dylan", LastName = "Lockwood" }
         },
         Suppliers = new List<Supplier>
         {
            new() { Name = "Ian Weemes" }
         }
      };

      writer.WriteStartElement("contacts");
      contacts.WriteXml(writer);
      writer.WriteEndElement();
   }

   private static Contacts DeserializeContacts()
   {
      var settings = new XmlReaderSettings
      {
         IgnoreWhitespace = true,
         IgnoreComments = true,
         IgnoreProcessingInstructions = true
      };

      using var reader = XmlReader.Create("contacts.xml", settings);
      reader.MoveToContent();

      var contacts = new Contacts();
      contacts.ReadXml(reader);

      return contacts;
   }
}