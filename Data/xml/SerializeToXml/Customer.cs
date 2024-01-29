using System.Xml;

namespace SerializeToXml;

public class Customer
{
   public const string XmlName = "customer";
   public string? FirstName, LastName;
   public int? Id;

   public Customer()
   {
   }

   public Customer(XmlReader r)
   {
      ReadXml(r);
   }

   public void ReadXml(XmlReader reader)
   {
      if (reader.MoveToAttribute("id"))
      {
         Id = reader.ReadContentAsInt();
      }

      reader.ReadStartElement();
      FirstName = reader.ReadElementContentAsString("firstname", "");
      LastName = reader.ReadElementContentAsString("lastname", "");
      reader.ReadEndElement();
   }

   public void WriteXml(XmlWriter writer)
   {
      if (Id.HasValue)
      {
         writer.WriteAttributeString("id", "", Id.ToString());
      }

      writer.WriteElementString("firstname", FirstName);
      writer.WriteElementString("lastname", LastName);
   }
}