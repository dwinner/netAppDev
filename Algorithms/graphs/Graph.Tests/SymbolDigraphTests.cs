using Graph.Adt;

namespace Graph.Tests;

[TestFixture]
public class SymbolDigraphTests
{
   [Test]
   public void SymbolDigraphTest()
   {
      (string fromVtx, string toVtx)[] vertices =
      {
         ("JFK", "MCO"),
         ("ORD", "DEN"),
         ("ORD", "HOU"),
         ("DFW", "PHX"),
         ("JFK", "ATL"),
         ("ORD", "DFW"),
         ("ORD", "PHX"),
         ("ATL", "HOU"),
         ("DEN", "PHX"),
         ("PHX", "LAX"),
         ("JFK", "ORD"),
         ("DEN", "LAS"),
         ("DFW", "HOU"),
         ("ORD", "ATL"),
         ("LAS", "LAX"),
         ("ATL", "MCO"),
         ("HOU", "MCO"),
         ("LAS", "PHX")
      };

      var symbolDigraph = new SymbolDigraph(vertices);
      var graph = symbolDigraph.Graph;
      const string inputCity = "JFK";
      Console.Write($"From {inputCity}: ");
      foreach (var vtx in graph.GetAdjacentList(symbolDigraph[inputCity]))
      {
         Console.Write($"{symbolDigraph[vtx]} ");
      }
   }
}