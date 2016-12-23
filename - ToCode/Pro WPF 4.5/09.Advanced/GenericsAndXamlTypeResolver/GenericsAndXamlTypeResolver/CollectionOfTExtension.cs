using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Collections.ObjectModel;
using System.Collections;

namespace GenericsAndXamlTypeResolver
{
    //
    // MarkupExtension that creates a Collection<T>
    //

    [ContentProperty("Items")]
    public class CollectionOfTExtension : CollectionOfTExtensionBase<IList>
    {
        protected override Type GetCollectionType(Type typeArgument)
        {
            return typeof(Collection<>).MakeGenericType(typeArgument);
        }

    }

}
