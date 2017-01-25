using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Collections.ObjectModel;

namespace GenericsAndXamlTypeResolver
{
    //
    // Markup extension that creates a concrete object from a generic type.
    //

    [ContentProperty("TypeArguments")]
    public class GenericExtension : MarkupExtension
    {
        // The collection of type arguments for the generic type
        private Collection<Type> _typeArguments = new Collection<Type>();
        public Collection<Type> TypeArguments
        {
            get { return _typeArguments; }
        }

        // The generic type name (e.g. Dictionary, for the Dictionary<K,V> case)
        private string _typeName;
        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        // Constructors
        public GenericExtension()
        {
        }
        public GenericExtension(string typeName)
        {
            TypeName = typeName;
        }

        // ProvideValue, which returns the concrete object of the generic type
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IXamlTypeResolver xamlTypeResolver = serviceProvider.GetService(typeof(IXamlTypeResolver)) as IXamlTypeResolver;
            if (xamlTypeResolver == null)
                throw new Exception("The Generic markup extension requires an IXamlTypeResolver service provider");

            // Get e.g. "Collection`1" type
            Type genericType = xamlTypeResolver.Resolve(
                _typeName + "`" + TypeArguments.Count.ToString());

            // Get an array of the type arguments
            Type[] typeArgumentArray = new Type[TypeArguments.Count];
            TypeArguments.CopyTo(typeArgumentArray, 0);

            // Create the conrete type, e.g. Collection<String>
            Type concreteType = genericType.MakeGenericType(typeArgumentArray);

            // Create an instance of that type
            return Activator.CreateInstance(concreteType);
        }

    }

}
