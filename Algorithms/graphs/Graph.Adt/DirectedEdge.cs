namespace Graph.Adt;

/// <summary>
///    The <see cref="DirectedEdge" /> class represents a weighted edge in an directed graph.
/// </summary>
public sealed class DirectedEdge
{
   public DirectedEdge(int fromVertex, int toVertex, double weight)
   {
      FromVertex = fromVertex;
      ToVertex = toVertex;
      Weight = weight;
   }

   public int FromVertex { get; }

   public int ToVertex { get; }

   public double Weight { get; }

   public override string ToString() => $"{FromVertex}->{ToVertex}:{Weight:F}";

   public void Deconstruct(out int fromVertex, out int toVertex, out double weight)
   {
      fromVertex = FromVertex;
      toVertex = ToVertex;
      weight = Weight;
   }
}