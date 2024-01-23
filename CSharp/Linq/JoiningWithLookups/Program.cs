using NutshellDb;

var customerList = new List<Customer>();
var purchaseList = new List<Purchase>();

var customers = customerList.ToArray();
var purchases = purchaseList.ToArray();

var lookup = purchases.ToLookup(p => p.CustomerId, p => p);

var inner =
   from c in customers
   from p in lookup[c.Id]
   select new
   {
      c.Name,
      p.Description,
      p.Price
   };

//inner.Dump("Inner join equivalent");

var outer =
   from c in customers
   from p in lookup[c.Id].DefaultIfEmpty()
   select new
   {
      c.Name,
      Descript = p?.Description,
      p?.Price
   };

//outer.Dump("Outer join equivalent");

var groupJoin =
   from c in customers
   select new
   {
      CustName = c.Name,
      CustPurchases = lookup[c.Id]
   };

//groupJoin.Dump("GroupJoin equivalent");