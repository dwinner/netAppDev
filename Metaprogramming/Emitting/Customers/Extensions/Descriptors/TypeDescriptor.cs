using System;
using System.Reflection;

namespace Customers.Extensions.Descriptors
{
   internal sealed class TypeDescriptor : Descriptor
   {
      private TypeDescriptor()
      {
      }

      internal TypeDescriptor(Type type)
         : this(type, type?.Assembly, true)
      {
      }

      internal TypeDescriptor(Type type, bool showKind)
      {
         CreateDescriptor(type, type.Assembly, showKind);
      }

      internal TypeDescriptor(Type type, Assembly containingAssembly)
         : this(type, containingAssembly, true)
      {
      }

      internal TypeDescriptor(Type type, Assembly containingAssembly, bool showKind)
      {
         CreateDescriptor(type, containingAssembly, showKind);
      }

      private void CreateDescriptor(Type type, Assembly containingAssembly, bool showKind)
      {
      }
   }
}