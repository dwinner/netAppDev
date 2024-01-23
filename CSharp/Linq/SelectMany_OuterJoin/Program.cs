using MoreLinq;
using SelectMany_OuterJoin;

var customers = new List<Customer>
{
   new()
   {
      Purchases = new HashSet<Purchase>
      {
         new() { Description = "desc1", Price = 0M }
      }
   }
};

var localCustomerCollection = customers.ToArray();

var query =
   from c in localCustomerCollection
   from p in c.Purchases.DefaultIfEmpty()
   select new
   {
      Descript = p?.Description,
      Price = p?.Price
   };

query.ForEach(obj => Console.WriteLine($"{obj.Price}: {obj.Descript}"));