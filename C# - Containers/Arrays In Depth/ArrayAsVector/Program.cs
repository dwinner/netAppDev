/**
 * Сравнение векторов и массивов
 */


namespace _04_ArrayAsVector
{
   class Program
   {
      static void Main(string[] args)
      {
         int val = 123;
         int newVal;
         int[] vector = new int[1];    // IL_0005:  newarr     [mscorlib]System.Int32
         int[,] array = new int[1, 1]; // IL_000d:  newobj     instance void int32[0...,0...]::.ctor(int32, int32)
         vector[0] = val;        // IL_0016:  stelem.i4
         array[0, 0] = val;      // IL_001b:  call       instance void int32[0...,0...]::Set(int32, int32, int32)
         newVal = vector[0];     // IL_0022:  ldelem.i4
         newVal = array[0, 0];   // IL_0027:  call       instance int32 int32[0...,0...]::Get(int32, int32)
      }
   }
}
