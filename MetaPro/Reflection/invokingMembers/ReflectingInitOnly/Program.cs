using System.Reflection;

namespace ReflectingInitOnly;

internal class Program
{
   public int Test { get; init; }

   private static void Main()
   {
      var program = new Program();
      var isInitOnly = program.IsInitOnly(program.GetType().GetProperty("Test"));
      Console.WriteLine(isInitOnly);
   }

   private bool IsInitOnly(PropertyInfo pi) => pi
      .GetSetMethod().ReturnParameter.GetRequiredCustomModifiers()
      .Any(t => t.Name == "IsExternalInit");
}