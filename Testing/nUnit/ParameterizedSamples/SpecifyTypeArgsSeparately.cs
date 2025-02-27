﻿namespace ParameterizedSamples;

[TestFixture(100.0, 42, TypeArgs = new[] { typeof(double), typeof(int) })]
[TestFixture(42, 100.0, TypeArgs = new[] { typeof(int), typeof(double) })]
public class SpecifyTypeArgsSeparately<T1, T2>
   where T1 : notnull
   where T2 : notnull
{
   private readonly T1 _t1;
   private readonly T2 _t2;

   public SpecifyTypeArgsSeparately(T1 t1, T2 t2)
   {
      _t1 = t1;
      _t2 = t2;
   }

   [TestCase(5, 7)]
   public void TestMyArgTypes(T1 t1, T2 t2)
   {
      Assert.That(t1, Is.TypeOf<T1>());
      Assert.That(t1, Is.LessThan(_t1));

      Assert.That(t2, Is.TypeOf<T2>());
      Assert.That(t2, Is.LessThan(_t2));
   }
}