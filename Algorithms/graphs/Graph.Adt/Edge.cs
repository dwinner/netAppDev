namespace Graph.Adt;

/// <summary>
///    The <see cref="Edge" /> class represents a weighted edge in an
///    <see cref="EdgeWeightedGraph" />. Each edge consists of two integers
///    (naming the two vertices) and a real-value weight. The data type
///    provides methods for accessing the two endpoints of the edge and
///    the weight. The natural order for this data type is by
///    ascending order of weight.
/// </summary>
public sealed class Edge : IComparable<Edge>, IEquatable<Edge>
{
   private static readonly Comparer<Edge> DefaultComparer = Comparer<Edge>.Default;

   /// <summary>
   ///    Initializes an edge between vertices {@code v} and {@code w} of the given {@code weight}.
   /// </summary>
   /// <param name="startVertex">one vertex</param>
   /// <param name="endVertex">the other vertex</param>
   /// <param name="weight">the weight of this edge</param>
   /// <exception cref="ArgumentException">
   ///    if either <paramref name="startVertex" /> or <paramref name="endVertex" /> is a negative integer, or if
   ///    <paramref name="weight" /> is NaN
   /// </exception>
   public Edge(int startVertex, int endVertex, double weight = default)
   {
      if (startVertex < 0)
      {
         throw new ArgumentException("vertex index must be a non-negative integer", nameof(startVertex));
      }

      if (endVertex < 0)
      {
         throw new ArgumentException("vertex index must be a non-negative integer", nameof(endVertex));
      }

      if (double.IsNaN(weight))
      {
         throw new ArgumentException("Weight is NaN", nameof(weight));
      }

      StartVertex = startVertex;
      EndVertex = endVertex;
      Weight = weight;
   }

   public static IEqualityComparer<Edge> DefaultEqualityComparer { get; } = new EqualityComparer();

   public int StartVertex { get; }

   public int EndVertex { get; }

   public double Weight { get; }

   public int CompareTo(Edge? other) =>
      ReferenceEquals(this, other)
         ? 0
         : ReferenceEquals(null, other)
            ? 1
            : Weight.CompareTo(other.Weight);

   public bool Equals(Edge? other) =>
      !ReferenceEquals(null, other) && (ReferenceEquals(this, other)
                                        ||
                                        (StartVertex == other.StartVertex
                                         && EndVertex == other.EndVertex
                                         && Weight.Equals(other.Weight)));

   /// <summary>
   ///    Returns the endpoint of this edge that is different from the given vertex.
   /// </summary>
   /// <param name="vertex">one endpoint of this edge</param>
   /// <returns>the other endpoint of this edge</returns>
   /// <exception cref="ArgumentException">if the vertex is not one of the endpoints of this edge</exception>
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

      throw new ArgumentException("Illegal endpoint", nameof(vertex));
   }

   /// <summary>
   ///    Returns either endpoint of this edge
   /// </summary>
   /// <returns>either endpoint of this edge</returns>
   public int GetEither() => StartVertex;

   public void Deconstruct(out int eitherVertex, out int otherVertex)
   {
      eitherVertex = GetEither();
      otherVertex = GetOther(eitherVertex);
   }

   public override string ToString() => $"{StartVertex:D}-{EndVertex:D} {Weight:F}";

   public static bool operator <(Edge? left, Edge? right) => DefaultComparer.Compare(left, right) < 0;

   public static bool operator >(Edge? left, Edge? right) => DefaultComparer.Compare(left, right) > 0;

   public static bool operator <=(Edge? left, Edge? right) => DefaultComparer.Compare(left, right) <= 0;

   public static bool operator >=(Edge? left, Edge? right) => DefaultComparer.Compare(left, right) >= 0;

   public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is Edge other && Equals(other));

   public override int GetHashCode() => HashCode.Combine(StartVertex, EndVertex, Weight);

   public static bool operator ==(Edge? left, Edge? right) => Equals(left, right);

   public static bool operator !=(Edge? left, Edge? right) => !Equals(left, right);

   private sealed class EqualityComparer : IEqualityComparer<Edge>
   {
      public bool Equals(Edge? x, Edge? y) => x != null && x.Equals(y);

      public int GetHashCode(Edge edge) => edge.GetHashCode();
   }
}