using System.Collections.Generic;
using DataMigration.Business;

namespace DataMigration.SqlDataAccess
{
   public class ShippersSqlRepository : IShippersRepository
   {
      private readonly NorthwindContext _objectContext;

      public ShippersSqlRepository([Configuration] string northwindConnectionString) =>
         _objectContext = new NorthwindContext(northwindConnectionString);

      public IEnumerable<Business.Model.Shipper> GetShippers() =>
         from shipper in _objectContext.Shippers
         select new Business.Model.Shipper
         {
            ShipperId = shipper.Shipper_ID,
            CompanyName = shipper.Company_Name
         };

      public void AddShipper(Business.Model.Shipper shipper)
      {
         _objectContext.Shippers.AddObject(new Shipper
         {
            Shipper_ID = shipper.ShipperId,
            Company_Name = shipper.CompanyName
         });
         _objectContext.SaveChanges();
      }
   }
}