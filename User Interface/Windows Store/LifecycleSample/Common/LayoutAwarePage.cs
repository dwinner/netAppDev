using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace LifecycleSample.Common
{
   [WebHostHidden]
   public class LayoutAwarePage : Page
   {
      public static readonly DependencyProperty DefaultViewModelProperty =
         DependencyProperty.Register("DefaultViewModel", typeof(IObservableMap<string, object>),
            typeof(LayoutAwarePage), null);

      private List<Control> _layoutAwareControls;

      protected LayoutAwarePage()
      {
         if (DesignMode.DesignModeEnabled) return;

         DefaultViewModel = new ObservableDictionary<string, object>();

         Loaded += (sender, e) =>
         {
            StartLayoutUpdates(sender);

            if (!(Math.Abs(ActualHeight - Window.Current.Bounds.Height) < double.Epsilon) ||
                !(Math.Abs(ActualWidth - Window.Current.Bounds.Width) < double.Epsilon)) return;

            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated +=
               CoreDispatcher_AcceleratorKeyActivated;

            Window.Current.CoreWindow.PointerPressed +=
               CoreWindow_PointerPressed;
         };

         Unloaded += (sender, e) =>
         {
            StopLayoutUpdates(sender);
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated -=
               CoreDispatcher_AcceleratorKeyActivated;
            Window.Current.CoreWindow.PointerPressed -=
               CoreWindow_PointerPressed;
         };
      }

      protected IObservableMap<string, object> DefaultViewModel
      {
         get { return GetValue(DefaultViewModelProperty) as IObservableMap<string, object>; }
         private set { SetValue(DefaultViewModelProperty, value); }
      }

      private class ObservableDictionary<TKey, TValue> : IObservableMap<TKey, TValue>
      {
         private readonly Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();
         public event MapChangedEventHandler<TKey, TValue> MapChanged;

         public void Add(TKey key, TValue value)
         {
            _dictionary.Add(key, value);
            InvokeMapChanged(key);
         }

         public void Add(KeyValuePair<TKey, TValue> item)
         {
            Add(item.Key, item.Value);
         }

         public bool Remove(TKey key)
         {
            if (_dictionary.Remove(key))
            {
               InvokeMapChanged(key);
               return true;
            }
            return false;
         }

         public bool Remove(KeyValuePair<TKey, TValue> item)
         {
            TValue currentValue;
            if (_dictionary.TryGetValue(item.Key, out currentValue) &&
                Equals(item.Value, currentValue) && _dictionary.Remove(item.Key))
            {
               InvokeMapChanged(item.Key);
               return true;
            }

            return false;
         }

         public TValue this[TKey key]
         {
            get { return _dictionary[key]; }
            set
            {
               _dictionary[key] = value;
               InvokeMapChanged(key);
            }
         }

         public void Clear()
         {
            var priorKeys = _dictionary.Keys.ToArray();
            _dictionary.Clear();
            foreach (var key in priorKeys)
            {
               InvokeMapChanged(key);
            }
         }

         public ICollection<TKey> Keys
         {
            get { return _dictionary.Keys; }
         }

         public bool ContainsKey(TKey key)
         {
            return _dictionary.ContainsKey(key);
         }

         public bool TryGetValue(TKey key, out TValue value)
         {
            return _dictionary.TryGetValue(key, out value);
         }

         public ICollection<TValue> Values
         {
            get { return _dictionary.Values; }
         }

         public bool Contains(KeyValuePair<TKey, TValue> item)
         {
            return _dictionary.Contains(item);
         }

         public int Count
         {
            get { return _dictionary.Count; }
         }

         public bool IsReadOnly
         {
            get { return false; }
         }

         public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
         {
            return _dictionary.GetEnumerator();
         }

         IEnumerator IEnumerable.GetEnumerator()
         {
            return _dictionary.GetEnumerator();
         }

         public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
         {
            var arraySize = array.Length;
            foreach (var pair in _dictionary)
            {
               if (arrayIndex >= arraySize) break;
               array[arrayIndex++] = pair;
            }
         }

         private void InvokeMapChanged(TKey key)
         {
            var eventHandler = MapChanged;
            if (eventHandler != null)
            {
               eventHandler(this, new ObservableDictionaryChangedEventArgs(CollectionChange.ItemInserted, key));
            }
         }

         private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<TKey>
         {
            public ObservableDictionaryChangedEventArgs(CollectionChange change, TKey key)
            {
               CollectionChange = change;
               Key = key;
            }

            public CollectionChange CollectionChange { get; private set; }
            public TKey Key { get; private set; }
         }
      }

      #region Navigation support

      /*
            protected void GoHome(object sender, RoutedEventArgs e)
            {         
               if (Frame != null)
               {
                  while (Frame.CanGoBack) Frame.GoBack();
               }
            }
      */

      protected void GoBack(object sender, RoutedEventArgs e)
      {
         if (Frame != null && Frame.CanGoBack) Frame.GoBack();
      }

      private void GoForward()
      {
         if (Frame != null && Frame.CanGoForward) Frame.GoForward();
      }

      private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender,
         AcceleratorKeyEventArgs args)
      {
         var virtualKey = args.VirtualKey;

         if ((args.EventType == CoreAcceleratorKeyEventType.SystemKeyDown ||
              args.EventType == CoreAcceleratorKeyEventType.KeyDown) &&
             (virtualKey == VirtualKey.Left || virtualKey == VirtualKey.Right ||
              (int)virtualKey == 166 || (int)virtualKey == 167))
         {
            var coreWindow = Window.Current.CoreWindow;
            const CoreVirtualKeyStates downState = CoreVirtualKeyStates.Down;
            var menuKey = (coreWindow.GetKeyState(VirtualKey.Menu) & downState) == downState;
            var controlKey = (coreWindow.GetKeyState(VirtualKey.Control) & downState) == downState;
            var shiftKey = (coreWindow.GetKeyState(VirtualKey.Shift) & downState) == downState;
            var noModifiers = !menuKey && !controlKey && !shiftKey;
            var onlyAlt = menuKey && !controlKey && !shiftKey;

            if (((int)virtualKey == 166 && noModifiers) ||
                (virtualKey == VirtualKey.Left && onlyAlt))
            {
               args.Handled = true;
               GoBack(this, new RoutedEventArgs());
            }
            else if (((int)virtualKey == 167 && noModifiers) ||
                     (virtualKey == VirtualKey.Right && onlyAlt))
            {
               args.Handled = true;
               GoForward();
            }
         }
      }

      private void CoreWindow_PointerPressed(CoreWindow sender,
         PointerEventArgs args)
      {
         var properties = args.CurrentPoint.Properties;

         // Ignore button chords with the left, right, and middle buttons
         if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed ||
             properties.IsMiddleButtonPressed) return;

         // If back or foward are pressed (but not both) navigate appropriately
         var backPressed = properties.IsXButton1Pressed;
         var forwardPressed = properties.IsXButton2Pressed;
         if (backPressed ^ forwardPressed)
         {
            args.Handled = true;
            if (backPressed) GoBack(this, new RoutedEventArgs());
            if (forwardPressed) GoForward();
         }
      }

      #endregion

      #region Visual state switching

      private void StartLayoutUpdates(object sender)
      {
         var control = sender as Control;
         if (control == null) return;
         if (_layoutAwareControls == null)
         {
            Window.Current.SizeChanged += WindowSizeChanged;
            _layoutAwareControls = new List<Control>();
         }
         _layoutAwareControls.Add(control);

         VisualStateManager.GoToState(control, DetermineVisualState(ApplicationView.Value), false);
      }

      private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
      {
         InvalidateVisualState();
      }

      private void StopLayoutUpdates(object sender)
      {
         var control = sender as Control;
         if (control == null || _layoutAwareControls == null) return;
         _layoutAwareControls.Remove(control);
         if (_layoutAwareControls.Count == 0)
         {
            _layoutAwareControls = null;
            Window.Current.SizeChanged -= WindowSizeChanged;
         }
      }

      private string DetermineVisualState(ApplicationViewState viewState)
      {
         return viewState.ToString();
      }

      private void InvalidateVisualState()
      {
         if (_layoutAwareControls != null)
         {
            var visualState = DetermineVisualState(ApplicationView.Value);
            foreach (var layoutAwareControl in _layoutAwareControls)
            {
               VisualStateManager.GoToState(layoutAwareControl, visualState, false);
            }
         }
      }

      #endregion

      #region Process lifetime management

      private string _pageKey;

      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         if (_pageKey != null) return;

         var frameState = SuspensionManager.SessionStateForFrame(Frame);
         _pageKey = string.Format("Page-{0}", Frame.BackStackDepth);

         if (e.NavigationMode == NavigationMode.New)
         {
            var nextPageKey = _pageKey;
            var nextPageIndex = Frame.BackStackDepth;
            while (frameState.Remove(nextPageKey))
            {
               nextPageIndex++;
               nextPageKey = string.Format("Page-{0}", nextPageIndex);
            }

            LoadState(null);
         }
         else
         {
            LoadState((Dictionary<string, object>)frameState[_pageKey]);
         }
      }

      protected override void OnNavigatedFrom(NavigationEventArgs e)
      {
         var frameState = SuspensionManager.SessionStateForFrame(Frame);
         var pageState = new Dictionary<string, object>();
         SaveState(pageState);
         frameState[_pageKey] = pageState;
      }

      protected virtual void LoadState(Dictionary<string, object> pageState)
      {
      }

      protected virtual void SaveState(Dictionary<string, object> pageState)
      {
      }

      #endregion
   }
}