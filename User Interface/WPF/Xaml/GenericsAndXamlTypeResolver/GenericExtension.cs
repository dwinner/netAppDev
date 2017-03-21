using System;
using System.Collections.ObjectModel;
using System.Windows.Markup;

namespace GenericsAndXamlTypeResolver
{
   /// <summary>
   ///    Markup extension that creates a concrete object from a generic type.
   /// </summary>
   [ContentProperty("TypeArguments")]
   public class GenericExtension : MarkupExtension
   {
      // The collection of type arguments for the generic type
      private readonly Collection<Type> _typeArguments = new Collection<Type>();

      // The generic type name (e.g. Dictionary, for the Dictionary<K,V> case)      
      public Collection<Type> TypeArguments
      {
         get { return _typeArguments; }
      }

      public string TypeName { get; set; }

      // ProvideValue, which returns the concrete object of the generic type
      public override object ProvideValue(IServiceProvider serviceProvider)
      {
         var xamlTypeResolver = serviceProvider.GetService(typeof (IXamlTypeResolver)) as IXamlTypeResolver;
         if (xamlTypeResolver == null)
            throw new ArgumentException("xamlTypeResolver");

         // Get e.g. "Collection`1" type
         var genericType = xamlTypeResolver.Resolve(string.Format("{0}`{1}", TypeName, TypeArguments.Count));

         // Get an array of the type arguments
         var typeArgumentArray = new Type[TypeArguments.Count];
         TypeArguments.CopyTo(typeArgumentArray, 0);

         // Create the conrete type, e.g. Collection<String>
         var concreteType = genericType.MakeGenericType(typeArgumentArray);

         // Create an instance of that type
         return Activator.CreateInstance(concreteType);
      }
   }
}