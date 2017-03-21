using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsAndXamlTypeResolver
{
   /// <summary>
   ///    MarkupExtension that creates an Dictionary['Object'T] (Items cannot be the [ContentProperty]).
   /// </summary>
   public sealed class DictionaryOfTExtension : CollectionOfTExtensionBase<IDictionary>
   {
      protected override Type GetCollectionType(Type typeArgument)
      {
         return typeof (Dictionary<,>).MakeGenericType(typeof (object), typeArgument);
      }

      private void CopyItems(IDictionary oldItems)
      {
         var oldItemsAsDictionary = oldItems;
         var newItemsAsDictionary = Items;

         foreach (DictionaryEntry entry in oldItemsAsDictionary)
         {
            newItemsAsDictionary[entry.Key] = oldItemsAsDictionary[entry.Key];
         }
      }
   }
}