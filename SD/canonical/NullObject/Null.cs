using System.Diagnostics;
using System.Dynamic;
using ImpromptuInterface;

namespace NullObject;

public class Null<T> : DynamicObject where T : class
{
   public static T Instance
   {
      get
      {
         if (!typeof(T).IsInterface)
         {
            throw new ArgumentException("I must be an interface type", nameof(T));
         }

         return new Null<T>().ActLike<T>();
      }
   }

   public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
   {
      var name = binder.Name;
      Debug.WriteLine($"Binder is {name}");
      var returnType = binder?.ReturnType;
      if (returnType != null)
      {
         result = Activator.CreateInstance(returnType);
         return true;
      }

      result = default;
      return false;
   }
}