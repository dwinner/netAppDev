﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ReturningOfMonads;

// You can chain multiple Bind/Map() functions
internal static class Program
{
   private static void Main()
   {
      var portfolioIds = new[] { 77, 88, 99 };
      var HoldingFrom = new DateTime(2019, 07, 25);

      var portfolios = GetPortfoliosByIds(portfolioIds);
      var portfoliosWithHoldings = PopulatePortfolioHoldings(portfolios, HoldingFrom);
      var result = DoSomething(portfoliosWithHoldings);

      // Notice how the result of the Bind or map always returns a Box<>             
      var pipelineResult1 = new Box<Portfolio[]>(GetPortfoliosByIds(portfolioIds))
         .Bind(ports =>
            new Box<Portfolio[]>(PopulatePortfolioHoldings(ports,
               HoldingFrom))) // Notice how bind must return a box during transformation
         .Map(ports =>
            DoSomething(ports)
               .ToArray()); // note how map doesn't require the transforming function to return a Box<>, it will so do automatically

      var pipelineResult2 = from ports in new Box<Portfolio[]>(GetPortfoliosByIds(portfolioIds))
         from ports1 in
            new Box<Portfolio[]>(PopulatePortfolioHoldings(ports,
               HoldingFrom)) // this transforms the extracted item form Box, Portfolio[] and puts back into box
         select
            DoSomething(
               ports1); // This is a projection() run via the Select() function within Bind() SelectMany() implementation, so it will automatically lift to a Box<>. Switch to Box class to see
   }

   /* Some interesting observations:
    * Map and Bind both extract and validate the item within the box (that it's not empty) ie do VETL and then proceeds to run the transform function on it if its not empty, otherwise returns empty box.
    * Map and Both are equivalent in as much as they perform VETL but differ in what form they require their transform function to either lift or not lift the transformation (the function proptypes must match what a Map ot Bind() function requires)
    * In the Linq Syntax query method, Box's SelectMany() is used to transform successive transformations.
    * You have access to each of the transformed results in subsequent transformations below(scope)
    * The final select statement is run via the Box's Select() function (technically this is the projection() function) and therefor it will automatically be lifted and you dont need to do it.
    * The Fluent mechanism, used Box's Map and Bind functions.
    * Each fluent style Map and Bind has access to the last transformation before it, and unlike the linq expression syntax cannot see beyond the last transformation (as that is the input it gets)
    * Transformations from a call to Bind and Map must result in a Box<> either explicitly via Bind() or automatically via Map()
    * Your logical planning or thinking of logical programming tasks in your design can equally be represented procedurally and using pipelining.
    */

   // Attach all the holdings for each portfolio where the holding is greated than holdingFrom 
   private static Portfolio[] PopulatePortfolioHoldings(Portfolio[] portfolios, DateTime holdingFrom)
   {
      foreach (var portfolio in portfolios)
      {
         portfolio.Holdings = GetPortfolioHoldingsFrom(portfolio, holdingFrom);
      }

      return portfolios;
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

      // Recap of the Linq Fluent syntax
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

      // Why not throw in some linq expression syntax 
      return (from portfolio in portfolios
            let portfolioId = portfolio.Key
            let actual_portfolio = portfolio.Value
            where portfolioIds.Contains(portfolioId)
            select actual_portfolio
         ).ToArray();
   }

   public static Box<Portfolio[]> GetPortfoliosByIds1(int[] portfolioIds) =>
      new(GetPortfoliosByIds(
         portfolioIds)); // note how we've just wrapped this into a Box<> - this is a perfect Bind() transformation function thusly


   // Scottish for do some thing        
   private static Portfolio[] DoSomething(Portfolio[] portfoliosWithHoldings)
   {
      Console.WriteLine("Good lord, a set of portfolios! What will we do with them?");
      foreach (var portfolio in portfoliosWithHoldings)
      {
         Console.WriteLine($"These are the holdings from the date specificed for portfolio '{portfolio.Name}'");
         foreach (var holding in portfolio.Holdings)
         {
            Console.WriteLine($"\t{holding}");
         }
      }

      return portfoliosWithHoldings;
   }
}

internal class Portfolio(string name, int id)
{
   public string Name { get; set; } = name;
   public int Id { get; set; } = id;
   public DateTime[] Holdings { get; set; } // A portfolio's holdings are just a series of dates
}