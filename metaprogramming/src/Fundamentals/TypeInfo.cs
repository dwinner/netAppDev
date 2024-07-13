using System.Reflection;

namespace Fundamentals;

/// <summary>
///    Represents an implementation of <see cref="ITypeInfo" />.
/// </summary>
/// <typeparam name="T">Type it holds info for.</typeparam>
public class TypeInfo<T> : ITypeInfo
{
   /// <summary>
   ///    Gets a singleton instance of the TypeInfo.
   /// </summary>
   public static readonly TypeInfo<T> Instance = new();

   private TypeInfo()
   {
      var type = typeof(T);
      var typeInfo = type.GetTypeInfo();
      var defaultConstructor = typeInfo.DeclaredConstructors.Any(ctorInfo => ctorInfo.GetParameters().Length == 0);
      HasDefaultConstructor = typeInfo.IsValueType || defaultConstructor;
   }

   /// <inheritdoc />
   public bool HasDefaultConstructor { get; }
}