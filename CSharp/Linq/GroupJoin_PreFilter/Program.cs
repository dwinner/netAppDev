using NutshellDb;

var customers = new List<Customer>();
var purchases = new List<Purchase>();

var grpJoinQuery = 
   from c in customers.AsEnumerable()
   join p in purchases.Where(p2 => p2.Price > 1000)
      on c.Id equals p.CustomerId
      into custPurchases
   where custPurchases.Any()
   select new
   {
      CustName = c.Name,
      custPurchases
   };

foreach (var grpItem in grpJoinQuery)
{
   Console.WriteLine(grpItem.CustName);
   foreach (var purchase in grpItem.custPurchases)
   {
      Console.WriteLine(purchase);
   }
}