using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Collections;

namespace GenericsAndXamlTypeResolver
{
    //
    // MarkupExtension that creates a List<T>
    //

    [ContentProperty("Items")]
    public class ListOfTExtension : CollectionOfTExtensionBase<IList>
    {
        protected override Type GetCollectionType(Type typeArgument)
        {
            return typeof(List<>).MakeGenericType(typeArgument);
        }
    }

}
