using NutshellDb;

var customers = new List<Customer>();
var purchases = new List<Purchase>();

var joinQuery = from c in customers.AsEnumerable()
   join p in purchases on c.Id equals p.CustomerId
      into custPurchases
   where custPurchases.Any()
   select new
   {
      CustName = c.Name,
      custPurchases
   };

foreach (var item in joinQuery)
{
   Console.WriteLine(item);
}