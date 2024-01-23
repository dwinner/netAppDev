using NutshellDb;

var customers = new List<Customer>();
var purchases = new List<Purchase>();

var querySyntax =
   from c in customers
   join p in purchases on c.Id equals p.CustomerId
   orderby p.Price
   select c.Name + " bought a " + p.Description + " for $" + p.Price;

var fluentSyntax =
   customers.Join( // outer collection
         purchases, // inner collection
         c => c.Id, // outer key selector
         p => p.CustomerId, // inner key selector
         (c, p) => new { c, p } // result selector 
      )
      .OrderBy(x => x.p.Price)
      .Select(x => x.c.Name + " bought a " + x.p.Description + " for $" + x.p.Price);

//querySyntax.Dump("Query syntax");
//fluentSyntax.Dump("Same query in fluent syntax");