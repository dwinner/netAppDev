using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DataMigration.Business;
using DataMigration.Business.Model;

namespace DataMigration.XmlDataAccess
{
   public class ShippersXmlRepository : IShippersRepository
   {
      private readonly string _documentPath;

      public ShippersXmlRepository([Configuration] string xmlRepositoryPath) => _documentPath = xmlRepositoryPath;

      public IEnumerable<Shipper> GetShippers()
      {
         var document = XDocument.Load(_documentPath);
         return from e in document.Elements("Shipper")
            select new Shipper
            {
               ShipperId = Convert.ToInt32(e.Element("ShipperID").Value),
               CompanyName = e.Element("CompanyName").Value
            };
      }

      public void AddShipper(Shipper shipper)
      {
         var document = XDocument.Load(_documentPath);
         document.Root.Add(new XElement("Shipper",
            new XElement("ShipperID", shipper.ShipperId),
            new XElement("CompanyName", shipper.CompanyName)));
         document.Save(_documentPath);
      }
   }
}