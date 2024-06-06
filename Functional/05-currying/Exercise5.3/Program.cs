using CustomLibrary;
using static System.Console;

WriteLine("*** Using the concept of currying.***");
Product product1 = new(100);
var product1Price = IO.GetPriceAfterDiscount(product1);
WriteLine($"MRP: ${product1.MRP}, Final price: ${product1Price}\n");

Product product2 = new(200);
var product2Price = IO.GetPriceAfterDiscount(product2);
WriteLine($"MRP: ${product2.MRP}, Final price: ${product2Price}\n");

internal class Product
{
   // Price calculator after discounts
   public Func<double, int, int, double> GetFinalCost =
      (mrp, seasonal, coupon) => mrp - mrp * seasonal / 100 - mrp * coupon / 100;

   public Product(double mrp) => MRP = mrp;

   public double MRP { get; }
}

internal class IO
{
   // The following function does not use the Curryfy library
   public static double GetPriceAfterDiscount(Product product)
   {
      // Using a non-generic version
      // var mrp = product
      //           .GetFinalCost
      //           .NonGenericCurry()(product.MRP);
      // Using the generic version
      var mrp = product
         .GetFinalCost
         .GenericCurry()(product.MRP);
      // Get a seasonal discount
      var seasonalDiscount = new Random().Next(1, 15);
      // Calculate cost after seasonal discount
      WriteLine($"Seasonal Discount={seasonalDiscount}%");
      var afterSeasonalDiscount = mrp(seasonalDiscount);

      // Get a coupon discount
      var couponDiscount = seasonalDiscount < 10
         ? 5
         : new Random().Next(1, 3);
      // Calculate cost after coupon discount
      WriteLine($"Coupon discount={couponDiscount}%");
      var afterCouponDiscount =
         afterSeasonalDiscount(couponDiscount);
      return afterCouponDiscount;
   }
}

namespace CustomLibrary
{
   public static class CurryExtensions
   {
      public static Func<double, Func<int, Func<int, double>>> NonGenericCurry(this Func<double, int, int, double> f)
      {
         return x => y => z => f(x, y, z);
      }
      // Alternative syntax
      //public static Func<double, Func<int, Func<int, double>>> NonGenericCurry(this Func<double, int, int, double> f)=>       
      //    x => y => z => f(x, y, z);

      public static Func<T1, Func<T2, Func<T3, TResult>>> GenericCurry<T1, T2, T3, TResult>(
         this Func<T1, T2, T3, TResult> f)
      {
         return x => y => z => f(x, y, z);
      }
   }
}