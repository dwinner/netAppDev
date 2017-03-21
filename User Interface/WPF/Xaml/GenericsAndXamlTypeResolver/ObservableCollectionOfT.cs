using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Markup;

namespace GenericsAndXamlTypeResolver
{   
   /// <summary>
   /// MarkupExtension that creates an ObservableCollection'T
   /// </summary>
   [ContentProperty("Items")]
   public class ObservableCollectionOfTExtension : CollectionOfTExtensionBase<IList>
   {
      protected override Type GetCollectionType(Type typeArgument)
      {
         return typeof (ObservableCollection<>).MakeGenericType(typeArgument);
      }
   }
}