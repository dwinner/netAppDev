using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.Collections;

namespace GenericsAndXamlTypeResolver
{
    //
    // MarkupExtension that creates an ObservableCollection<T>
    //

    [ContentProperty("Items")]
    public class ObservableCollectionOfTExtension : CollectionOfTExtensionBase<IList>
    {
        protected override Type GetCollectionType(Type typeArgument)
        {
            return typeof(ObservableCollection<>).MakeGenericType(typeArgument);
        }
    }


}
