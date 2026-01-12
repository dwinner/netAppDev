// See https://aka.ms/new-console-template for more information

namespace UseOfDeclarativeStyle;

// Shows show to convert a Procedural wa of thinking -> Pipeline thinking
internal abstract class Program
{
   private static void Main(string[] args)
   {
      // 2 Boxes!
      var boxOfIntegers = new Box<int[]>(new[] { 3, 5, 7, 9, 11, 13, 15 });
      var boxOfNewIntegers = new Box<int[]>(new[] { 3, 5, 88, 29, 155, 123, 1 });

      var portfolioIds = new[] { 77, 88, 99 };
      var HoldingFrom = new DateTime(2019, 07, 25);

      /* Procedural */

      // 1. Go get the portfolio names for the following portfolio ids:
      var portfolios = GetPortfoliosByIds(portfolioIds);

      // 2. Get the holdings for these portfolios which are dated later than xyz
      var portfoliosWithHoldings = PopulatePortfolioHoldings(portfolios, HoldingFrom);

      // 3. do something with these populated portfolios
      var result = DoSomething(portfoliosWithHoldings);

      /* Pipline way of thinking */

      // Linq Fluent syntax way
      var result2 =
         GetPortfoliosByIds1(portfolioIds) // note same input, portfolioIds can be used but this is returning a box now
            .Bind(ports =>
               PopulatePortfolioHoldings1(ports,
                  HoldingFrom)) // notice you can use the same name of the transformed item as the last(ports)
            .Map(ports => DoSomething(ports).ToArray());

      // Linq Expression syntax way
      Box<Portfolio[]> result3 = from ports in GetPortfoliosByIds1(portfolioIds)
         from ports1 in
            PopulatePortfolioHoldings1(ports, HoldingFrom) // notice you cant use the name of last transform again
         select DoSomething(ports1);
   }

   /// <summary>
   ///    do something
   /// </summary>
   private static Portfolio[] DoSomething(Portfolio[] portfoliosWithHoldings)
   {
      return portfoliosWithHoldings.Select(p =>
      {
         p.Name = p.Name.ToUpper();
         return p;
      }).ToArray();
   }

   private static Portfolio[] PopulatePortfolioHoldings(Portfolio[] portfolios, DateTime holdingFrom)
   {
      foreach (var portfolio in portfolios)
      {
         portfolio.Holdings = GetPortfolioHoldingsFrom(portfolio, holdingFrom);
      }

      return portfolios;
   }

   /// <summary>
   ///    Returns a Box
   /// </summary>
   private static Box<Portfolio[]> PopulatePortfolioHoldings1(Portfolio[] portfolios, DateTime holdingFrom)
   {
      var listOfPortfolios = portfolios.Select(p =>
      {
         p.Holdings = GetPortfolioHoldingsFrom(p, holdingFrom);
         return p;
      }).ToArray();
      return new Box<Portfolio[]>(listOfPortfolios);
   }


   private static DateTime[] GetPortfolioHoldingsFrom(Portfolio portfolio, DateTime holdingFrom)
   {
      var portfolioHoldings = new Dictionary<int, List<DateTime>>
      {
         { 1, new List<DateTime> { new(2019, 07, 23), new(2019, 07, 24), new(2019, 07, 25), new(2019, 07, 26) } },
         { 2, new List<DateTime> { new(2019, 07, 23), new(2019, 07, 24), new(2019, 07, 25), new(2019, 07, 26) } },
         { 77, new List<DateTime> { new(2019, 07, 23), new(2019, 07, 24), new(2019, 07, 25), new(2019, 07, 26) } },
         { 88, new List<DateTime> { new(2019, 07, 23), new(2019, 07, 24), new(2019, 07, 25), new(2019, 07, 26) } },
         { 99, new List<DateTime> { new(2019, 07, 23), new(2019, 07, 24), new(2019, 07, 25), new(2019, 07, 26) } }
      };

      return portfolioHoldings
         .Where(x => x.Key == portfolio.Id)
         .SelectMany(x => x.Value)
         .Where(x => x >= holdingFrom).ToArray();
   }

   public static Portfolio[] GetPortfoliosByIds(int[] portfolioIds)
   {
      var portfolios = new Dictionary<int, Portfolio>
      {
         { 1, new Portfolio("Portfolio1", 1) },
         { 2, new Portfolio("Portfolio2", 2) },
         { 77, new Portfolio("Portfolio77", 77) },
         { 88, new Portfolio("Portfolio88", 88) },
         { 99, new Portfolio("Portfolio99", 99) }
      };

      return portfolios.Where(x => portfolioIds.Contains(x.Key))
         .Select(x => x.Value)
         .ToArray();
   }

   public static Box<Portfolio[]> GetPortfoliosByIds1(int[] portfolioIds)
   {
      var portfolios = new Dictionary<int, Portfolio>
      {
         { 1, new Portfolio("Portfolio1", 1) },
         { 2, new Portfolio("Portfolio2", 2) },
         { 77, new Portfolio("Portfolio77", 77) },
         { 88, new Portfolio("Portfolio88", 88) },
         { 99, new Portfolio("Portfolio99", 99) }
      };

      var listofPortfolios = portfolios.Where(x => portfolioIds.Contains(x.Key))
         .Select(x => x.Value)
         .ToList();
      return new Box<Portfolio[]>(listofPortfolios.ToArray());
   }
}

internal class Portfolio
{
   public Portfolio(string name, int id)
   {
      Id = id;
      Name = name;
   }

   public string Name { get; set; }
   public int Id { get; set; }
   public DateTime[] Holdings { get; set; }
}