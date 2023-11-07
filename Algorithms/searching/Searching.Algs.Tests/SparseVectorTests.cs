namespace Searching.Algs.Tests;

[TestFixture]
public class SparseVectorTests
{
   [Test]
   public void SparseVectorTest()
   {
      var vector1 = new SparseVector(10);
      var vector2 = new SparseVector(10);

      vector1[3] = 0.50;
      vector1[9] = 0.75;
      vector1[6] = 0.11;
      vector1[6] = 0.00;

      vector2[3] = 0.60;
      vector2[4] = 0.90;

      var dotProduct = vector1.Dot(vector2);
      var sumVector = vector1 + vector2;

      Console.WriteLine($"Dot product: {dotProduct}");
      Console.WriteLine($"Sum vector: {sumVector}");
   }
}