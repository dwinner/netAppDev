using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;

namespace GenericsAndXamlTypeResolver
{
   /// <summary>
   ///    MarkupExtension that creates a List'T
   /// </summary>
   [ContentProperty("Items")]
   public class ListOfTExtension : CollectionOfTExtensionBase<IList>
   {
      protected override Type GetCollectionType(Type typeArgument)
      {
         return typeof (List<>).MakeGenericType(typeArgument);
      }
   }
}