using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Markup;

namespace GenericsAndXamlTypeResolver
{
   /// <summary>
   ///    MarkupExtension that creates a Collection'T
   /// </summary>
   [ContentProperty("Items")]
   public class CollectionOfTExtension : CollectionOfTExtensionBase<IList>
   {
      protected override Type GetCollectionType(Type typeArgument)
      {
         return typeof (Collection<>).MakeGenericType(typeArgument);
      }
   }
}