using NutshellDb;

var customerList = new List<Customer>();
var purchaseList = new List<Purchase>();

var customers = customerList.ToArray();
var purchases = purchaseList.ToArray();

var groupJoinQuery =
   from c in customers
   join p in purchases on c.Id equals p.CustomerId
      into custPurchases
   select new
   {
      CustName = c.Name,
      custPurchases
   };

var selectEquivalent =
   from c in customers
   select new
   {
      CustName = c.Name,
      custPurchases = purchases.Where(p => c.Id == p.CustomerId)
   };

/*
@"The GroupJoin query is more efficient in this case, because we're querying
arrays (i.e. local collections).".Dump();

groupJoinQuery.Dump("Group Join Query");
selectEquivalent.Dump("Equivalent with Select");
*/