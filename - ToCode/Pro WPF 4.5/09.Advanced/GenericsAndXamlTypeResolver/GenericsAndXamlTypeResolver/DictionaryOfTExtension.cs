using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace GenericsAndXamlTypeResolver
{
    //
    // MarkupExtension that creates an Dictionary<Object,T>
    // (Items cannot be the [ContentProperty]).
    //

    public class DictionaryOfTExtension : CollectionOfTExtensionBase<IDictionary>
    {
        protected override Type GetCollectionType(Type typeArgument)
        {
            return typeof(Dictionary<,>).MakeGenericType(typeof(Object), typeArgument);
        }

        protected virtual void CopyItems(IDictionary oldItems)
        {
            IDictionary oldItemsAsDictionary = oldItems as IDictionary;
            IDictionary newItemsAsDictionary = Items as IDictionary;

            foreach (DictionaryEntry entry in oldItemsAsDictionary)
            {
                newItemsAsDictionary[entry.Key] = oldItemsAsDictionary[entry.Key];
            }
        }
    }

}
