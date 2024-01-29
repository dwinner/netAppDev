using System.Xml;

namespace SerializeToXml;

public class Contacts
{
   public IList<Customer> Customers = new List<Customer>();
   public IList<Supplier> Suppliers = new List<Supplier>();

   public void ReadXml(XmlReader reader)
   {
      var isEmpty = reader.IsEmptyElement; // This ensures we don't get
      reader.ReadStartElement(); // snookered by an empty
      if (isEmpty)
      {
         return; // <contacts/> element!
      }

      while (reader.NodeType == XmlNodeType.Element)
      {
         switch (reader.Name)
         {
            case Customer.XmlName:
               Customers.Add(new Customer(reader));
               break;
            case Supplier.XmlName:
               Suppliers.Add(new Supplier(reader));
               break;
            default:
               throw new XmlException($"Unexpected node: {reader.Name}");
         }
      }

      reader.ReadEndElement();
   }

   public void WriteXml(XmlWriter writer)
   {
      foreach (var customer in Customers)
      {
         writer.WriteStartElement(Customer.XmlName);
         customer.WriteXml(writer);
         writer.WriteEndElement();
      }

      foreach (var supplier in Suppliers)
      {
         writer.WriteStartElement(Supplier.XmlName);
         supplier.WriteXml(writer);
         writer.WriteEndElement();
      }
   }
}