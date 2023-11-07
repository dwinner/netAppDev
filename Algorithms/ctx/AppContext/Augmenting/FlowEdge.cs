namespace AppContext.Augmenting;

/// <summary>
///    The <see cref="FlowEdge" /> class represents a capacitated edge with a
///    flow in a <see cref="FlowNetwork"/>. Each edge consists of two integers
///    (naming the two vertices), a real-valued capacity, and a real-valued
///    flow. The data type provides methods for accessing the two endpoints
///    of the directed edge and the weight. It also provides methods for
///    changing the amount of flow on the edge and determining the residual
///    capacity of the edge.
/// </summary>
public sealed class FlowEdge
{
   /// <summary>
   ///    Initializes an edge from vertex <paramref name="endVertex"/> to vertex <paramref name="startVertex"/> with the given <paramref name="capacity"/> and <paramref name="flow"/>.
   /// </summary>
   /// <param name="startVertex">The start vertex</param>
   /// <param name="endVertex">The end vertex</param>
   /// <param name="capacity">The capacity of the edge</param>
   /// <param name="flow">The flow on the edge</param>
   /// <exception cref="ArgumentException">
   ///    If either <paramref name="startVertex" /> or <paramref name="endVertex" /> is a negative integer;
   ///    if <paramref name="capacity" /> is negative;
   ///    unless <paramref name="flow" /> is between 0.0 and <paramref name="capacity" />.
   /// </exception>
   public FlowEdge(int startVertex, int endVertex, double capacity, double flow = default)
   {
      if (startVertex < 0)
      {
         throw new ArgumentException("vertex index must be a non-negative integer", nameof(startVertex));
      }

      if (endVertex < 0)
      {
         throw new ArgumentException("vertex index must be a non-negative integer", nameof(endVertex));
      }

      if (!(capacity >= 0.0))
      {
         throw new ArgumentException("edge capacity must be non-negative", nameof(capacity));
      }

      if (!(flow <= capacity))
      {
         throw new ArgumentException("flow exceeds capacity", nameof(flow));
      }

      if (!(flow >= 0.0))
      {
         throw new ArgumentException("flow must be non-negative", nameof(flow));
      }

      StartVertex = startVertex;
      EndVertex = endVertex;
      Capacity = capacity;
      Flow = flow;
   }

   /// <summary>
   ///    Initializes a flow edge from another flow edge.
   /// </summary>
   /// <param name="flowEdge">The edge to copy</param>
   public FlowEdge(FlowEdge flowEdge)
   {
      StartVertex = flowEdge.StartVertex;
      EndVertex = flowEdge.EndVertex;
      Capacity = flowEdge.Capacity;
      Flow = flowEdge.Flow;
   }

   /// <summary>
   ///    The tail vertex of the edge
   /// </summary>
   public int StartVertex { get; }

   /// <summary>
   ///    The head vertex of the edge.
   /// </summary>
   public int EndVertex { get; }

   /// <summary>
   ///    The capacity of the edge.
   /// </summary>
   public double Capacity { get; }

   /// <summary>
   ///    The flow on the edge.
   /// </summary>
   public double Flow { get; private set; }

   /// <summary>
   ///    Returns the endpoint of the edge that is different from the given vertex (unless the edge represents a self-loop in
   ///    which case it returns the same vertex).
   /// </summary>
   /// <param name="vertex">One endpoint of the edge</param>
   /// <returns>
   ///    The endpoint of the edge that is different from the given vertex (unless the edge represents a self-loop in
   ///    which case it returns the same vertex)
   /// </returns>
   /// <exception cref="ArgumentException">If <paramref name="vertex" /> is not one of the endpoints of the edge</exception>
   public int GetOther(int vertex)
   {
      if (vertex == StartVertex)
      {
         return EndVertex;
      }

      if (vertex == EndVertex)
      {
         return StartVertex;
      }

      throw new ArgumentException($"Invalid endpoint: {vertex}", nameof(vertex));
   }

   /// <summary>
   ///    Returns the residual capacity of the edge in the direction to the given <paramref name="vertex" />
   /// </summary>
   /// <param name="vertex">One endpoint of the edge</param>
   /// <returns>
   ///    The residual capacity of the edge in the direction to the given vertex. If <paramref name="vertex" /> is the
   ///    tail vertex, the residual capacity equals <code>capacity() - flow()</code>; if <paramref name="vertex" /> is the
   ///    head vertex, the residual capacity equals <code>flow()</code>.
   /// </returns>
   /// <exception cref="ArgumentException">If <paramref name="vertex" /> is not one of the endpoints of the edge</exception>
   public double GetResidualCapacityTo(int vertex)
   {
      if (vertex == StartVertex)
      {
         return Flow; // backward edge
      }

      if (vertex == EndVertex)
      {
         return Capacity - Flow; // forward edge
      }

      throw new ArgumentException($"Invalid endpoint: {vertex}", nameof(vertex));
   }

   /// <summary>
   ///    Increases the flow on the edge in the direction to the given vertex.
   /// </summary>
   /// <remarks>
   ///    If <paramref name="vertex" /> is the tail vertex, this increases the flow on the edge by <paramref name="delta" />;
   ///    if <paramref name="vertex" /> is the head vertex, this decreases the flow on the edge by <paramref name="delta" />.
   /// </remarks>
   /// <param name="vertex">One endpoint of the edge</param>
   /// <param name="delta">Amount by which to increase flow</param>
   /// <exception cref="ArgumentException">If <paramref name="vertex" /> is not one of the endpoints of the edge</exception>
   /// <exception cref="InvalidOperationException">
   ///    If <paramref name="delta" /> makes the flow on the edge either negative or
   ///    larger than its capacity; if <paramref name="delta" /> is <code><see cref="double.NaN" /></code>
   /// </exception>
   public void AddResidualFlowTo(int vertex, double delta)
   {
      if (!(delta >= 0.0))
      {
         throw new ArgumentException("Delta must be non-negative", nameof(vertex));
      }

      if (vertex == StartVertex)
      {
         Flow -= delta; // backward edge
      }
      else if (vertex == EndVertex)
      {
         Flow += delta; // forward edge
      }
      else
      {
         throw new ArgumentException($"Invalid endpoint: {vertex}", nameof(vertex));
      }

      // round flow to 0 or capacity if within floating-point precision
      if (Math.Abs(Flow) <= double.Epsilon)
      {
         Flow = 0;
      }

      if (Math.Abs(Flow - Capacity) <= double.Epsilon)
      {
         Flow = Capacity;
      }

      if (!(Flow >= 0.0))
      {
         throw new InvalidOperationException("Flow is negative");
      }

      if (!(Flow <= Capacity))
      {
         throw new InvalidOperationException("Flow exceeds capacity");
      }
   }

   public override string ToString() => $"{StartVertex}->{EndVertex} {Flow}/{Capacity}";
}