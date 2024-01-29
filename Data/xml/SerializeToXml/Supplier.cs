using System.Xml;

namespace SerializeToXml;

public class Supplier
{
   public const string XmlName = "supplier";
   public string? Name;

   public Supplier()
   {
   }

   public Supplier(XmlReader r)
   {
      ReadXml(r);
   }

   public void ReadXml(XmlReader reader)
   {
      reader.ReadStartElement();
      Name = reader.ReadElementContentAsString("name", "");
      reader.ReadEndElement();
   }

   public void WriteXml(XmlWriter writer)
   {
      writer.WriteElementString("name", Name);
   }
}