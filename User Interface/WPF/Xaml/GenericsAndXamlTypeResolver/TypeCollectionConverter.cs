using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Markup;

namespace GenericsAndXamlTypeResolver
{
   /// <summary>
   ///    Type converter that converts a string which is a list
   ///    of Xaml type names, e.g. "sys:String,sys:Int32", to a Collection'Type.
   /// </summary>
   public class TypeCollectionConverter : TypeConverter
   {
      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
         var xamlTypeResolver = context.GetService(typeof (IXamlTypeResolver)) as IXamlTypeResolver;
         return sourceType == typeof (Type) && xamlTypeResolver != null || base.CanConvertFrom(context, sourceType);
      }

      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
      {
         var xamlTypeResolver = context.GetService(typeof (IXamlTypeResolver)) as IXamlTypeResolver;
         var s = value as string;
         if (s != null)
         {
            var stringArray = s.Split(',');
            var types = new Collection<Type>();

            for (var i = 0; i < stringArray.Length; i++)
               if (xamlTypeResolver != null)
                  types.Add(xamlTypeResolver.Resolve(stringArray[i].Trim()));

            return types;
         }

         return null;
      }
   }
}