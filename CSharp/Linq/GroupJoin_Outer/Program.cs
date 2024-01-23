using NutshellDb;

var customers = new List<Customer>();
var purchases = new List<Purchase>();

var outerJoin = from c in customers
   join p in purchases on c.Id equals p.CustomerId
      into custPurchases
   from cp in custPurchases.DefaultIfEmpty()
   select new
   {
      CustName = c.Name,
      cp?.Price
   };

foreach (var item in outerJoin)
{
   Console.WriteLine(item);
}