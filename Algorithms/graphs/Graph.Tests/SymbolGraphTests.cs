using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class SymbolGraphTests
{
   [SetUp]
   public void SetUp()
   {
      _symbolGraph = new SymbolGraph(new[]
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
      });
   }

   private SymbolGraph _symbolGraph = null!;

   [Test]
   public void SymbolGraphTest()
   {
      const string firstPoint = "JFK";
      CheckPoint(firstPoint);

      const string secondPoint = "LAX";
      CheckPoint(secondPoint);

      void CheckPoint(string point)
      {
         if (!_symbolGraph.Contains(point))
         {
            return;
         }

         var pointIdx = _symbolGraph[point];
         Console.Write($"From {point}: ");
         foreach (var vtx in _symbolGraph.Graph.GetAdjacentList(pointIdx))
         {
            Console.Write($"{_symbolGraph[vtx]} ");
         }

         Console.WriteLine();
      }
   }

   [Test]
   public void DegreesOfSeparationTest()
   {
      const string source = "JFK";
      const string destination = "LAS";
      var srcVertex = _symbolGraph[source];
      var bfs = new Breadth1StPath(_symbolGraph.Graph, srcVertex);
      bfs.Search();

      if (_symbolGraph.Contains(destination))
      {
         var sinkIndex = _symbolGraph[destination];
         if (bfs.HasPathTo(sinkIndex))
         {
            Console.Write($"The shortest path From '{source}' To '{destination}': ");
            foreach (var pathVtx in bfs.GetPathTo(sinkIndex))
            {
               Console.Write($" {_symbolGraph[pathVtx]}");
            }
         }
         else
         {
            Console.WriteLine("Not connected");
         }
      }
      else
      {
         Console.WriteLine("Not in database");
      }
   }
}