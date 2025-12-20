namespace DefiningComplexEqOperators;

internal class Counter
{
   public int Value { get; set; }

   public virtual void operator += (int n)
   {
      Value += n;
   }

   public void operator += (Counter aCounter)
   {
      Value += aCounter.Value;
   }

   public void operator ++() => ++Value;
}

internal class DoubleCounter : Counter
{
   public new int Value { get; set; }

   public override void operator += (int n)
   {
      Value += 2 * n;
   }
}