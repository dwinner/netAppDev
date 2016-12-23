using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Markup;

namespace GenericsAndXamlTypeResolver
{
    //
    // MarkupExtension that is base for an extension that creates
    // Collection<T>, List<T>, or Dictionary<T>.
    // (CollectionType is either IList or IDictionary).

    public abstract class CollectionOfTExtensionBase<CollectionType> : MarkupExtension
        where CollectionType : class
    {
        public CollectionOfTExtensionBase(Type typeArgument)
        {
            _typeArgument = typeArgument;
        }

        // Default the collection to typeof(Object)
        public CollectionOfTExtensionBase()
            : this(typeof(Object))
        {
        }

        // Items is the actual collection we'll return from ProvideValue.
        protected CollectionType _items;
        public CollectionType Items
        {
            get
            {
                if (_items == null)
                {
                    Type collectionType = GetCollectionType(TypeArgument);
                    _items = Activator.CreateInstance(collectionType) as CollectionType;
                }
                return _items;
            }
        }

        // TypeArgument is the "T" in e.g. Collection<T>
        private Type _typeArgument;
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
        protected virtual void CopyItems(object oldItems)
        {
            IList oldItemsAsList = oldItems as IList;
            IList newItemsAsList = Items as IList;

            for (int i = 0; i < oldItemsAsList.Count; i++)
            {
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
