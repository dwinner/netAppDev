namespace ParameterizedSamples;

[TestFixture]
public class TypedValuesWithExpectedInWrapper
{
   [TestCaseSource(nameof(TestCases))]
   public void TestOfPersonAge(TestDataWrapper<Person, bool> td)
   {
      var res = td.Value?.IsOldEnoughToBuyAlcohol();
      Assert.That(res, Is.EqualTo(td.Expected));
   }

   public static IEnumerable<TestDataWrapper<Person, bool>> TestCases()
   {
      yield return new TestDataWrapper<Person, bool>
         { Value = new Person { Name = "John", Age = 10 }, Expected = false };
      yield return new TestDataWrapper<Person, bool>
         { Value = new Person { Name = "Jane", Age = 30 }, Expected = true };
   }
}

public class TestDataWrapper<T, TExp>
{
   public T? Value { get; set; }
   public TExp? Expected { get; set; }
}