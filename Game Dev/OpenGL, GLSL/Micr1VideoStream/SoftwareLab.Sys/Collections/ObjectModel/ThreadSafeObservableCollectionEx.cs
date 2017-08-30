using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace SoftwareLab.Sys.Collections.ObjectModel
{
    public class ThreadSafeObservableCollection<T> : ObservableCollection<T>
    {
        // Override the event so this class can access it
        public override event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Be nice - use BlockReentrancy like MSDN said
            using (BlockReentrancy())
            {
                System.Collections.Specialized.NotifyCollectionChangedEventHandler eventHandler = CollectionChanged;
                if (eventHandler == null)
                    return;

                Delegate[] delegates = eventHandler.GetInvocationList();
                // Walk thru invocation list
                foreach (System.Collections.Specialized.NotifyCollectionChangedEventHandler handler in delegates)
                {
                    DispatcherObject dispatcherObject = handler.Target as DispatcherObject;
                    // If the subscriber is a DispatcherObject and different thread
                    if (dispatcherObject != null && dispatcherObject.CheckAccess() == false)
                    {
                        // Invoke handler in the target dispatcher's thread
                        dispatcherObject.Dispatcher.Invoke(DispatcherPriority.DataBind, handler, this, e);
                    }
                    else // Execute handler as is
                        handler(this, e);
                }
            }
        }

        ///// <summary> 
        ///// Adds the elements of the specified collection to the end of the ObservableCollection(Of T). 
        ///// </summary> 
        //public void AddRange(IEnumerable<T> collection)
        //{
        //    foreach (var i in collection) Items.Add(i);
        //    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection));
        //}

        ///// <summary> 
        ///// Removes the first occurence of each item in the specified collection from ObservableCollection(Of T). 
        ///// </summary> 
        //public void RemoveRange(IEnumerable<T> collection)
        //{
        //    foreach (var i in collection) Items.Remove(i);
        //    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
        //}

        ///// <summary> 
        ///// Clears the current collection and replaces it with the specified item. 
        ///// </summary> 
        //public void Replace(T item)
        //{
        //    ReplaceRange(new T[] { item });
        //}
        ///// <summary> 
        ///// Clears the current collection and replaces it with the specified collection. 
        ///// </summary> 
        //public void ReplaceRange(IEnumerable<T> collection)
        //{
        //    List<T> old = new List<T>(Items);
        //    Items.Clear();
        //    foreach (var i in collection) Items.Add(i);
        //    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
        //}

    }
}
