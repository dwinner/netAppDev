namespace ParameterizedSamples;

[TestFixture(typeof(double), typeof(int), 100.0, 42)]
[TestFixture(typeof(int), typeof(double), 42, 100.0)]
public class SpecifyBothSetsOfArgs<T1, T2>
   where T1 : notnull
   where T2 : notnull
{
   private readonly T1 _t1;
   private readonly T2 _t2;

   public SpecifyBothSetsOfArgs(T1 t1, T2 t2)
   {
      _t1 = t1;
      _t2 = t2;
   }

   [TestCase(5, 7)]
   public void TestMyArgTypes(T1 t1, T2 t2)
   {
      Assert.That(t1, Is.TypeOf<T1>());
      //Assert.That(t1, Is.LessThan(_t1));

      Assert.That(t2, Is.TypeOf<T2>());
      //Assert.That(t2, Is.LessThan(_t2));
   }
}