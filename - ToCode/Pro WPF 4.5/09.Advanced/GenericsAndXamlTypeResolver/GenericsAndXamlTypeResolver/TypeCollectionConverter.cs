using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GenericsAndXamlTypeResolver
{
    //
    // Type converter that converts a string which is a list
    // of Xaml type names, e.g. "sys:String,sys:Int32", to a Collection<Type>.
    //

    public class TypeCollectionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            IXamlTypeResolver xamlTypeResolver = context.GetService(typeof(IXamlTypeResolver)) as IXamlTypeResolver;

            if (sourceType == typeof(Type) && xamlTypeResolver != null)
                return true;

            return base.CanConvertFrom(context, sourceType);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            IXamlTypeResolver xamlTypeResolver = context.GetService(typeof(IXamlTypeResolver)) as IXamlTypeResolver;

            string[] stringArray = (value as string).Split(',');
            Collection<Type> types = new Collection<Type>();

            for (int i = 0; i < stringArray.Length; i++)
                types.Add(xamlTypeResolver.Resolve(stringArray[i].Trim()));

            return types;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
