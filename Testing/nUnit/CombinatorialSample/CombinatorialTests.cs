namespace CombinatorialSample;

public class CombinatorialTests
{
   [SetUp]
   public void Setup()
   {
   }

   [Test]
   [Combinatorial]
   public void Combo_Test(
      [Values(1, 2, 3)] int intVal,
      [Values("A", "B")] string strVal)
   {
      Console.WriteLine($"{intVal} {strVal}");
      Assert.Pass();
   }
}