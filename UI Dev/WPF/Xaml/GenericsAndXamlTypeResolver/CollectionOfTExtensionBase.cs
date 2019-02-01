using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Markup;

namespace GenericsAndXamlTypeResolver
{
   /// <summary>
   /// MarkupExtension that is base for an extension that creates
   /// Collection, List, or Dictionary
   /// </summary>
   /// <typeparam name="TCollectionType">Collection type</typeparam>
   public abstract class CollectionOfTExtensionBase<TCollectionType> : MarkupExtension
      where TCollectionType : class
   {
      // Items is the actual collection we'll return from ProvideValue.
      private TCollectionType _items;

      // TypeArgument is the "T" in e.g. Collection<T>
      private Type _typeArgument;

      private CollectionOfTExtensionBase(Type typeArgument)
      {
         _typeArgument = typeArgument;
      }

      // Default the collection to typeof(Object)
      protected CollectionOfTExtensionBase()
         : this(typeof (object))
      {
      }

      public TCollectionType Items
      {
         get
         {
            if (_items == null)
            {
               var collectionType = GetCollectionType(TypeArgument);
               _items = Activator.CreateInstance(collectionType) as TCollectionType;
            }

            return _items;
         }
      }

      public Type TypeArgument
      {
         get { return _typeArgument; }
         set
         {
            _typeArgument = value;

            // If the TypeArgument doesn't get set until after
            // items have been added, we need to re-create items
            // to be the right type.
            if (_items != null)
            {
               object oldItems = _items;
               _items = null;
               CopyItems(oldItems);
            }
         }
      }

      // Default implementation of CopyItems that works for Collection/List
      // (but not Dictionary).
      private void CopyItems(object oldItems)
      {
         var oldItemsAsList = oldItems as IList;
         var newItemsAsList = Items as IList;
         Debug.Assert(oldItemsAsList != null);

         for (var i = 0; i < oldItemsAsList.Count; i++)
         {
            if (newItemsAsList != null)
               newItemsAsList.Add(oldItemsAsList[i]);
         }
      }

      // Get the generic type, e.g. typeof(Collection<>), aka Collection`1.
      protected abstract Type GetCollectionType(Type typeArgument);

      // Provide the concrete collection instance.
      public override object ProvideValue(IServiceProvider serviceProvider)
      {
         return _items;
      }
   }
}