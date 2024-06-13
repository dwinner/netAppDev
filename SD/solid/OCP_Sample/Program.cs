// we introduce two new interfaces that are open for extension

using OCP_Sample;
using static System.Console;

var apple = new Product("Apple", Color.Green, Size.Small);
var tree = new Product("Tree", Color.Green, Size.Large);
var house = new Product("House", Color.Blue, Size.Large);

Product[] products = { apple, tree, house };

var pf = new ProductFilter();
var largeGreenSpec = new ColorSpecificationBase(Color.Green)
                     & new SizeSpecificationBase(Size.Large);
//var largeGreenSpec = Color.Green.And(Size.Large);
WriteLine("Green products (old):");
foreach (var p in pf.FilterByColor(products, Color.Green))
{
   WriteLine($" - {p.Name} is green");
}

// ^^ BEFORE

// vv AFTER
var bf = new BetterFilter();
WriteLine("Green products (new):");
foreach (var p in bf.Filter(products, new ColorSpecificationBase(Color.Green)))
{
   WriteLine($" - {p.Name} is green");
}

WriteLine("Large products");
foreach (var p in bf.Filter(products, new SizeSpecificationBase(Size.Large)))
{
   WriteLine($" - {p.Name} is large");
}

WriteLine("Large blue items");
foreach (var p in bf.Filter(products,
            new AndSpecificationBase<Product>(new ColorSpecificationBase(Color.Blue),
               new SizeSpecificationBase(Size.Large)))
        )
{
   WriteLine($" - {p.Name} is big and blue");
}