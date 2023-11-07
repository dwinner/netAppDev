using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class AcyclicPathTests
{
   [Test]
   public void AcyclicShortestPathTest()
   {
      const int verticeCount = 13;
      var graph = new EdgeWeightedDigraph(verticeCount);
      graph.AddEdge(new DirectedEdge(5, 4, 0.35));
      graph.AddEdge(new DirectedEdge(4, 7, 0.37));
      graph.AddEdge(new DirectedEdge(5, 7, 0.28));
      graph.AddEdge(new DirectedEdge(5, 1, 0.32));
      graph.AddEdge(new DirectedEdge(4, 0, 0.38));
      graph.AddEdge(new DirectedEdge(0, 2, 0.26));
      graph.AddEdge(new DirectedEdge(3, 7, 0.39));
      graph.AddEdge(new DirectedEdge(1, 3, 0.29));
      graph.AddEdge(new DirectedEdge(7, 2, 0.34));
      graph.AddEdge(new DirectedEdge(6, 2, 0.40));
      graph.AddEdge(new DirectedEdge(3, 6, 0.52));
      graph.AddEdge(new DirectedEdge(6, 0, 0.58));
      graph.AddEdge(new DirectedEdge(6, 4, 0.93));

      // find the shortest path from source to each other vertex in DAG
      const int sourceVertex = 5;
      var shortestPath = new AcyclicShortestPath(graph, sourceVertex);
      shortestPath.Apply();
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         if (shortestPath.HasPathTo(vtx))
         {
            Console.Write("{0:D} to {1:D} ({2:F})  ",
               sourceVertex,
               vtx,
               shortestPath.GetDistanceTo(vtx));
            foreach (var edge in shortestPath.GetPathTo(vtx))
            {
               Console.Write($"{edge}   ");
            }

            Console.WriteLine();
         }
         else
         {
            Console.WriteLine("{0:D} To {1:D} has no path", sourceVertex, vtx);
         }
      }
   }

   [Test]
   public void AcyclicLongestPathTest()
   {
      const int verticeCount = 8;
      const int sourceVertex = 5;
      var graph = new EdgeWeightedDigraph(verticeCount);
      graph.AddEdge(new DirectedEdge(5, 4, 0.35));
      graph.AddEdge(new DirectedEdge(4, 7, 0.37));
      graph.AddEdge(new DirectedEdge(5, 7, 0.28));
      graph.AddEdge(new DirectedEdge(5, 1, 0.32));
      graph.AddEdge(new DirectedEdge(4, 0, 0.38));
      graph.AddEdge(new DirectedEdge(0, 2, 0.26));
      graph.AddEdge(new DirectedEdge(3, 7, 0.39));
      graph.AddEdge(new DirectedEdge(1, 3, 0.29));
      graph.AddEdge(new DirectedEdge(7, 2, 0.34));
      graph.AddEdge(new DirectedEdge(6, 2, 0.40));
      graph.AddEdge(new DirectedEdge(3, 6, 0.52));
      graph.AddEdge(new DirectedEdge(6, 0, 0.58));
      graph.AddEdge(new DirectedEdge(6, 4, 0.93));

      var longestPath = new AcyclicLongestPath(graph, sourceVertex);
      longestPath.Apply();

      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         if (longestPath.HasPathTo(vtx))
         {
            Console.Write("{0:D} To {1:D} ({2:F}): ", sourceVertex, vtx, longestPath.GetDistanceTo(vtx));
            foreach (var edge in longestPath.GetPathTo(vtx))
            {
               Console.Write($"{edge} ");
            }

            Console.WriteLine();
         }
         else
         {
            Console.WriteLine("There is no path from {0:D} To {1:D}", sourceVertex, vtx);
         }
      }
   }

   /// <summary>
   ///    The CPM class provides a client that solves the
   ///    parallel precedence-constrained job scheduling problem
   ///    via the <em>critical path method</em>. It reduces the problem
   ///    to the longest-paths problem in edge-weighted DAGs.
   ///    It builds an edge-weighted digraph (which must be a DAG)
   ///    from the job-scheduling problem specification,
   ///    finds the longest-paths tree, and computes the longest-paths
   ///    lengths (which are precisely the start times for each job).
   ///    <p>
   ///       This implementation uses <see cref="AcyclicShortestPath" /> to find a longest
   ///       path in a DAG.
   ///       The program takes Theta (<em>V</em> + <em>E</em>) time in
   ///       the worst case, where <em>V</em> is the number of jobs and
   ///       <em>E</em> is the number of precedence constraints.
   ///    </p>
   /// </summary>
   [Test(Description = "Critical path method.")]
   public void CpmTest()
   {
      const int numberOfJobs = 10;

      // source and sink
      const int source = 2 * numberOfJobs;
      const int sink = 2 * numberOfJobs + 1;

      var precedences = new[]
      {
         (41.0, new List<int> { 1, 7, 9 }),
         (51.0, new List<int> { 2 }),
         (50.0, new List<int>(0)),
         (36.0, new List<int>(0)),
         (38.0, new List<int>(0)),
         (45.0, new List<int>(0)),
         (21.0, new List<int> { 3, 8 }),
         (32.0, new List<int> { 3, 8 }),
         (32.0, new List<int> { 2 }),
         (29.0, new List<int> { 4, 6 })
      };

      // build network
      var graph = new EdgeWeightedDigraph(2 * numberOfJobs + 2);
      for (var i = 0; i < numberOfJobs; i++)
      {
         var (duration, constraints) = precedences[i];
         graph.AddEdge(new DirectedEdge(source, i, 0.0));
         graph.AddEdge(new DirectedEdge(i + numberOfJobs, sink, 0.0));
         graph.AddEdge(new DirectedEdge(i, i + numberOfJobs, duration));

         // precedence constraints
         constraints.ForEach(precedent =>
            graph.AddEdge(new DirectedEdge(numberOfJobs + i, precedent, 0.0)));
      }

      // compute the longest path
      var longestPath = new AcyclicLongestPath(graph, source);
      longestPath.Apply();

      // print results
      Console.WriteLine(" job   start  finish");
      Console.WriteLine("--------------------");
      for (var i = 0; i < numberOfJobs; i++)
      {
         Console.WriteLine("{0,4:D} {1,7:F1} {2,7:F1}",
            i,
            longestPath.GetDistanceTo(i),
            longestPath.GetDistanceTo(i + numberOfJobs));
      }

      Console.WriteLine("Finish time: {0,7:F1}", longestPath.GetDistanceTo(sink));
   }
}