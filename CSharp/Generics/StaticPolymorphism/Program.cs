namespace StaticPolymorphism;

internal class Program
{
   private static void Main()
   {
      // To call this method via the interface, we use a constrained type parameter.
      // The following method creates an array of test data using this approach:

      var testData = CreateTestData<Point>(50); // Create 50 random Points.
      Console.WriteLine(testData);

      return;

      // Our call to the static CreateRandom method in CreateTestData is polymorphic because it works
      // not just with Point, but with any type that implements ICreateRandom<T>. This is different to
      // instance polymorphism, because we don’t need an instance of ICreateRandom<T> on which to call
      // CreateRandom; we call CreateRandom on the type itself.

      T[] CreateTestData<T>(int count) where T : ICreateRandom<T>
      {
         var result = new T[count];
         for (var i = 0; i < count; i++)
         {
            result[i] = T.CreateRandom();
         }

         return result;
      }
   }
}

// Consider the following interface, which defines a static method to create a random instance of some type T:
internal interface ICreateRandom<out T>
{
   static abstract T CreateRandom(); // Create a random instance of T
}

// Let's implement that interface:
internal record Point(int X, int Y) : ICreateRandom<Point>
{
   private static readonly Random Rnd = new();

   public static Point CreateRandom() => new(Rnd.Next(), Rnd.Next());
}