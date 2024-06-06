using Curryfy;
using static System.Console;

WriteLine("*** Using the concept of currying.***");
Product product1 = new(100);
var product1Price = Io.GetPriceAfterDiscount(product1);
WriteLine($"MRP: ${product1.MRP}, Final price: ${product1Price}\n");

Product product2 = new(200);
var product2Price = Io.GetPriceAfterDiscount(product2);
WriteLine($"MRP: ${product2.MRP}, Final price: ${product2Price}\n");

internal class Product
{
   // Price calculator after discounts
   public Func<double, int, int, double> GetFinalCost =
      (mrp, seasonal, coupon) => mrp - mrp * seasonal / 100 - mrp * coupon / 100;

   public Product(double mrp) => MRP = mrp;

   public double MRP { get; }
}

internal class Io
{
   public static double GetPriceAfterDiscount(Product product)
   {
      var mrp = product.GetFinalCost.Curry()(product.MRP);
      // Get a seasonal discount
      var seasonalDiscount = new Random().Next(1, 15);
      // Calculate cost after seasonal discount
      WriteLine($"Seasonal Discount={seasonalDiscount}%");
      var afterSeasonalDiscount = mrp(seasonalDiscount);

      // Get a coupon discount
      var couponDiscount = seasonalDiscount < 10 ? 5 : new Random().Next(1, 3);
      // Calculate cost after coupon discount
      WriteLine($"Coupon discount={couponDiscount}%");
      var afterCouponDiscount = afterSeasonalDiscount(couponDiscount);
      return afterCouponDiscount;
   }
}