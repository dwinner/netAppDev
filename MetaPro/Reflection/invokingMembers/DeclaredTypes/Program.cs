namespace DeclaredTypes;

internal class Program
{
   private static void Main()
   {
      var test = typeof(Program).GetMethod("ToString");
      var obj = typeof(object).GetMethod("ToString");

      Console.WriteLine(test.DeclaringType); // System.Object
      Console.WriteLine(obj.DeclaringType); // System.Object

      Console.WriteLine(test.ReflectedType); // Program
      Console.WriteLine(obj.ReflectedType); // System.Object

      Console.WriteLine(test == obj); // False

      Console.WriteLine(test.MethodHandle == obj.MethodHandle); // True

      Console.WriteLine(test.MetadataToken == obj.MetadataToken // True
                        && test.Module == obj.Module);
   }
}