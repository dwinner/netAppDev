namespace _04_ClosureViaFisrtCSharp
{
   public unsafe class MyClosure
   {
      public delegate int IncDelegate();

      private readonly int* _counter;

      public MyClosure(int* counter) => _counter = counter;

      public IncDelegate GetDelegate() => IncrementFunction;

      private int IncrementFunction() => (*_counter)++;
   }
}