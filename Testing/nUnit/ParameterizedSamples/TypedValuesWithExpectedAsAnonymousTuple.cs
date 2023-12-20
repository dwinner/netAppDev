namespace ParameterizedSamples;

[TestFixture]
public class TypedValuesWithExpectedAsAnonymousTuple
{
   [TestCaseSource(nameof(TestCases))]
   public void TestOfPersonAge((Person person, bool expected) td)
   {
      var res = td.person.IsOldEnoughToBuyAlcohol();
      Assert.That(res, Is.EqualTo(td.expected));
   }

   public static IEnumerable<(Person, bool)> TestCases()
   {
      yield return (new Person { Name = "John", Age = 10 }, false);
      yield return (new Person { Name = "Jane", Age = 30 }, true);
   }
}

public class Person
{
   public string Name { get; set; } = string.Empty;
   public int Age { get; set; }

   public bool IsOldEnoughToBuyAlcohol() => Age >= 18;
}