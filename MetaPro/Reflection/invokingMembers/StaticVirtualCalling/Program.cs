using System.Reflection;

Console.WriteLine(ParseAny(typeof(float), ".2")); // 0.2
return;

MethodInfo GetImplementedInterfaceMethod(Type concreteType, Type interfaceType, string methodName, Type[] paramTypes)
{
   var map = concreteType.GetInterfaceMap(interfaceType);

   return map.InterfaceMethods
      .Zip(map.TargetMethods)
      .Single(m => m.First.Name == methodName &&
                   m.First.GetParameters().Select(p => p.ParameterType)
                      .SequenceEqual(paramTypes))
      .Second;
}

object ParseAny(Type type, string value)
{
   var parseMethod = GetImplementedInterfaceMethod(type,
      type.GetInterface("IParsable`1"),
      "Parse",
      new[] { typeof(string), typeof(IFormatProvider) });

   return parseMethod.Invoke(null, new[] { value, null });
}