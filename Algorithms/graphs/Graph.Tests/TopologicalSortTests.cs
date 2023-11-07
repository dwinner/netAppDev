using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class TopologicalSortTests
{
   [Test]
   public void TopologicalSortTest()
   {
      (string fromVtx, IEnumerable<string> toVertices)[] vertices =
      {
         ("Algorithms", new List<string>
         {
            "Theoretical CS",
            "Databases",
            "Scientific Computing"
         }.AsEnumerable()),
         ("Introduction to CS", new List<string>
         {
            "Advanced Programming",
            "Algorithms"
         }.AsEnumerable()),
         ("Advanced Programming", new List<string>
         {
            "Scientific Computing"
         }.AsEnumerable()),
         ("Scientific Computing", new List<string>
         {
            "Computational Biology"
         }.AsEnumerable()),
         ("Theoretical CS", new List<string>
         {
            "Computational Biology",
            "Artificial Intelligence"
         }.AsEnumerable()),
         ("Linear Algebra",
            new List<string>
            {
               "Theoretical CS"
            }.AsEnumerable()),
         ("Calculus", new List<string>
         {
            "Linear Algebra"
         }.AsEnumerable()),
         ("Artificial Intelligence", new List<string>
         {
            "Neural Networks",
            "Robotics",
            "Machine Learning"
         }.AsEnumerable()),
         ("Machine Learning", new List<string>
         {
            "Neural Networks"
         }.AsEnumerable())
      };

      var digraph = new SymbolDigraph(vertices);
      var graph = digraph.Graph;
      var topologicalSort = new TopologicalSort(graph.VerticeCount);
      topologicalSort.Search(graph);
      foreach (var vtx in topologicalSort.TopologicalOrder)
      {
         Console.WriteLine(digraph[vtx]);
      }
   }
}