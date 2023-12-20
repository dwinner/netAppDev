namespace TheorySamples;

[TestFixture(typeof(int))]
[TestFixture(typeof(double))]
public class TheorySampleTestsGeneric<T>
{
   [Datapoint] public double[] ArrayDouble1 = { 1.2, 3.4 };

   [Datapoint] public double[] ArrayDouble2 = { 5.6, 7.8 };

   [Datapoint] public int[] ArrayInt = { 0, 1, 2, 3 };

   [Theory]
   public void TestGenericForArbitraryArray(T[] array)
   {
      Assert.That(array.Length, Is.EqualTo(4));
   }
}