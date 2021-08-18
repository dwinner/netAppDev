using System;
using System.Linq;
using System.Reflection;

namespace KnownTypes
{
   public static class MessageProcessorKnownTypesProvider
   {
      private static Type[] _knownTypes;

      public static Type[] GetMessageTypes(ICustomAttributeProvider attributeTarget)
         => _knownTypes ??
            (_knownTypes =
               Assembly.GetAssembly(typeof(MessageProcessorKnownTypesProvider))
                  .GetTypes()
                  .Where(type => typeof(Message).IsAssignableFrom(type))
                  .ToArray());
   }
}