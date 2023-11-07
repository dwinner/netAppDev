using System.Diagnostics;

namespace AppContext.Augmenting;

/// <summary>
///    The <see cref="FordFulkerson" /> class represents a data type for computing a
///    <em>maximum st-flow</em> and <em>minimum st-cut</em> in a flow
///    network.
///    <p>
///       This implementation uses the <em>Ford-Fulkerson</em> algorithm with
///       the <em>shortest augmenting path</em> heuristic.
///       The constructor takes <em>O</em>(<em>E V</em> (<em>E</em> + <em>V</em>))
///       time, where <em>V</em> is the number of vertices and <em>E</em> is
///       the number of edges. In practice, the algorithm will run much faster.
///       The inCut() and value() methods take Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the network).
///    </p>
///    <p>
///       This correctly computes the maxflow and mincut if all arithmetic
///       performed is without floating-point rounding error or arithmetic
///       overflow. This is guaranteed to be the case if all edge capacities
///       and initial flow values are integers and the value of the maxflow
///       does not exceed 2<sup>52</sup>.
///    </p>
/// </summary>
public sealed class FordFulkerson
{
   private readonly FlowNetwork _flowNetwork;
   private readonly int _sourceVertex;
   private readonly int _targetVertex;
   private readonly int _verticeCount; // number of vertices
   private bool[] _markedVertices = null!; // marked[v] = true if s->v path in residual graph
   private FlowEdge[] _toEdges = null!; // edgeTo[v] = last edge on shortest residual s->v path

   /// <summary>
   ///    Creating
   /// </summary>
   /// <param name="flowNetwork">The flow network</param>
   /// <param name="sourceVertex">The source vertex</param>
   /// <param name="targetVertex">The target vertex</param>
   /// <exception cref="ArgumentException">In the case of invalid flow network input</exception>
   public FordFulkerson(FlowNetwork flowNetwork, int sourceVertex, int targetVertex)
   {
      _verticeCount = flowNetwork.VerticeCount;
      Validate(sourceVertex);
      Validate(targetVertex);
      if (sourceVertex == targetVertex)
      {
         throw new ArgumentException("Source equals sink", nameof(sourceVertex));
      }

      if (!IsFeasible(flowNetwork, sourceVertex, targetVertex, out var errorMessage))
      {
         throw new ArgumentException($"Initial flow is infeasible: {errorMessage}", nameof(targetVertex));
      }

      _flowNetwork = flowNetwork;
      _sourceVertex = sourceVertex;
      _targetVertex = targetVertex;
   }

   /// <summary>
   ///    The value of the maximum flow
   /// </summary>
   public double MaxFlowValue { get; private set; }

   /// <summary>
   ///    Compute a maximum flow and minimum cut in the network <see cref="_flowNetwork" /> from vertex
   ///    <see cref="_sourceVertex" /> to vertex <see cref="_targetVertex" />.
   /// </summary>
   public void Apply()
   {
      // while there exists an augmenting path, use it
      MaxFlowValue = Excess(_flowNetwork, _targetVertex);
      while (HasAugmentingPath(_flowNetwork, _sourceVertex, _targetVertex))
      {
         // compute bottleneck capacity
         var bottle = double.PositiveInfinity;
         for (var vtx = _targetVertex;
              vtx != _sourceVertex;
              vtx = _toEdges[vtx].GetOther(vtx))
         {
            bottle = Math.Min(bottle, _toEdges[vtx].GetResidualCapacityTo(vtx));
         }

         // augment flow
         for (var vtx = _targetVertex;
              vtx != _sourceVertex;
              vtx = _toEdges[vtx].GetOther(vtx))
         {
            _toEdges[vtx].AddResidualFlowTo(vtx, bottle);
         }

         MaxFlowValue += bottle;
      }

      // check optimality conditions
#if DEBUG
      Debug.Assert(Check(_flowNetwork, _sourceVertex, _targetVertex, out var errorMsg), errorMsg);
#endif
   }

   // is there an augmenting path?
   // if so, upon termination edgeTo[] will contain a parent-link representation of such a path
   // this implementation finds a shortest augmenting path (fewest number of edges),
   // which performs well both in theory and in practice
   private bool HasAugmentingPath(FlowNetwork flowNetwork, int srcVertex, int dstVertex)
   {
      var vtxCount = flowNetwork.VerticeCount;
      _toEdges = new FlowEdge[vtxCount];
      _markedVertices = new bool[vtxCount];

      // breadth-first search
      var queue = new Queue<int>();
      queue.Enqueue(srcVertex);
      _markedVertices[srcVertex] = true;
      while (queue.Count > 0 && !_markedVertices[dstVertex])
      {
         var dequeuedVtx = queue.Dequeue();
         var adjList = flowNetwork.GetAdjacencyList(dequeuedVtx);
         foreach (var edge in adjList)
         {
            var otherVtx = edge.GetOther(dequeuedVtx);

            // if residual capacity from v to w
            if (edge.GetResidualCapacityTo(otherVtx) > 0 && !_markedVertices[otherVtx])
            {
               _toEdges[otherVtx] = edge;
               _markedVertices[otherVtx] = true;
               queue.Enqueue(otherVtx);
            }
         }
      }

      // is there an augmenting path?
      return _markedVertices[dstVertex];
   }

   /// <summary>
   ///    Returns true if the specified vertex is on the source side of the mincut.
   /// </summary>
   /// <param name="vertex">vertex</param>
   /// <returns>true if vertex <paramref name="vertex" /> is on the source side of the mincut; false otherwise</returns>
   public bool InCut(int vertex)
   {
      Validate(vertex);
      return _markedVertices[vertex];
   }

   private void Validate(int vertex)
   {
      if (vertex < 0 || vertex >= _verticeCount)
      {
         throw new ArgumentOutOfRangeException(nameof(vertex),
            $"vertex {vertex} is not between 0 and {_verticeCount - 1}");
      }
   }

   // return excess flow at vertex v
   private static double Excess(FlowNetwork flowNetwork, int vertex)
   {
      double excess = default;
      foreach (var edge in flowNetwork.GetAdjacencyList(vertex))
      {
         if (vertex == edge.StartVertex)
         {
            excess -= edge.Flow;
         }
         else
         {
            excess += edge.Flow;
         }
      }

      return excess;
   }

   // return excess flow at vertex v
   private bool IsFeasible(FlowNetwork flowNetwork, int sourceVtx, int targetVtx, out string errorMessage)
   {
      // check that capacity constraints are satisfied
      for (var vtx = 0; vtx < flowNetwork.VerticeCount; vtx++)
      {
         foreach (var edge in flowNetwork.GetAdjacencyList(vtx))
         {
            if (edge.Flow < -double.Epsilon || edge.Flow > edge.Capacity + double.Epsilon)
            {
               errorMessage = $"Edge does not satisfy capacity constraints: {edge}";
               return false;
            }
         }
      }

      // check that net flow into a vertex equals zero, except at source and sink
      if (Math.Abs(MaxFlowValue + Excess(flowNetwork, sourceVtx)) > double.Epsilon)
      {
         errorMessage = $"Excess at source = {Excess(flowNetwork, sourceVtx)}. Max flow = {MaxFlowValue}";
         return false;
      }

      if (Math.Abs(MaxFlowValue - Excess(flowNetwork, targetVtx)) > double.Epsilon)
      {
         errorMessage = $"Excess at sink = {Excess(flowNetwork, targetVtx)}. Max flow = {MaxFlowValue}";
         return false;
      }

      for (var vtx = 0; vtx < flowNetwork.VerticeCount; vtx++)
      {
         if (vtx == sourceVtx || vtx == targetVtx)
         {
            continue;
         }

         if (Math.Abs(Excess(flowNetwork, vtx)) > double.Epsilon)
         {
            errorMessage = $"Net flow out of {vtx} doesn't equal zero";
            return false;
         }
      }

      errorMessage = string.Empty;
      return true;
   }

   // check optimality conditions
   private bool Check(FlowNetwork flowNetwork, int sourceVtx, int targetVtx, out string errorMessage)
   {
      // check that flow is feasible
      if (!IsFeasible(flowNetwork, sourceVtx, targetVtx, out errorMessage))
      {
         return false;
      }

      // check that s is on the source side of min cut and that t is not on source side
      if (!InCut(sourceVtx))
      {
         errorMessage = $"source {sourceVtx} is not on source side of min cut";
         return false;
      }

      if (InCut(targetVtx))
      {
         errorMessage = $"sink {targetVtx} is on source side of min cut";
         return false;
      }

      // check that value of min cut = value of max flow
      var mincutValue = 0.0;
      for (var vtx = 0; vtx < flowNetwork.VerticeCount; vtx++)
      {
         mincutValue += flowNetwork.GetAdjacencyList(vtx)
            .Where(edge => vtx == edge.StartVertex
                           && InCut(edge.StartVertex)
                           && !InCut(edge.EndVertex))
            .Sum(edge => edge.Capacity);
      }

      if (Math.Abs(mincutValue - MaxFlowValue) > double.Epsilon)
      {
         errorMessage = $"Max flow value = {MaxFlowValue}, min cut value = {mincutValue}";
         return false;
      }

      errorMessage = string.Empty;
      return true;
   }
}