using System.Dynamic;

namespace CustomBindingSample;

public class Duck : DynamicObject
{
   public override bool TryInvokeMember(
      InvokeMemberBinder binder, object?[]? args, out object result)
   {
      Console.WriteLine($"{binder.Name} method was called");
      result = null!;
      return true;
   }
}