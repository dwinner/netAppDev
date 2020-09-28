using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ComboBoxSample
{
   public sealed class InvokeIfSameElementSelectedBehavior : Behavior<ComboBox>
   {
      private static readonly PropertyMetadata _CommandMetadata = new PropertyMetadata(default(ICommand));

      public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
         nameof(Command),
         typeof(ICommand),
         typeof(InvokeIfSameElementSelectedBehavior),
         _CommandMetadata);

      private Popup _popup;
      private bool _popupMouseClicked;
      private object _previousValue;
      private bool _skipSelectionChanged;

      public ICommand Command
      {
         get => (ICommand) GetValue(CommandProperty);
         set => SetValue(CommandProperty, value);
      }

      protected override void OnAttached()
      {
         base.OnAttached();

         if (AssociatedObject.IsLoaded)
         {
            AttachAllEvents();
         }
         else
         {
            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
         }
      }

      private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
      {
         AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
         AttachAllEvents();
      }

      protected override void OnDetaching()
      {
         base.OnDetaching();

         AssociatedObject.SelectionChanged -= AssociatedObjectOnSelectionChanged;
         AssociatedObject.DropDownOpened -= AssociatedObjectOnDropDownOpened;
         AssociatedObject.DropDownClosed -= AssociatedObjectOnDropDownClosed;

         if (_popup != null)
         {
            _popup.PreviewMouseLeftButtonDown -= PopupOnPreviewMouseLeftButtonDown;
         }
      }

      private void AttachAllEvents()
      {
         AssociatedObject.SelectionChanged += AssociatedObjectOnSelectionChanged;
         AssociatedObject.DropDownOpened += AssociatedObjectOnDropDownOpened;
         AssociatedObject.DropDownClosed += AssociatedObjectOnDropDownClosed;

         AssociatedObject.ApplyTemplate();
         _popup = (Popup) AssociatedObject.Template.FindName("PART_Popup", AssociatedObject);
         if (_popup != null)
         {
            _popup.PreviewMouseLeftButtonDown += PopupOnPreviewMouseLeftButtonDown;
         }
      }

      private void AssociatedObjectOnDropDownOpened(object sender, EventArgs e)
      {
         _popupMouseClicked = false;
         _previousValue = AssociatedObject.SelectedItem;
      }

      private void AssociatedObjectOnDropDownClosed(object sender, EventArgs e)
      {
         try
         {
            // SelectionChanged handles it if value are not the same
            if (_popupMouseClicked && Equals(AssociatedObject.SelectedItem, _previousValue))
            {
               InvokeChangeCommand(AssociatedObject.SelectedItem);
            }
         }
         finally
         {
            _popupMouseClicked = false;
         }
      }

      private void PopupOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         // ignore clicks on the scrollbars
         if (e.Source is ScrollViewer)
         {
            return;
         }

         _popupMouseClicked = true;
      }

      private void AssociatedObjectOnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         if (_skipSelectionChanged)
         {
            return;
         }

         try
         {
            _skipSelectionChanged = true;

            if (e.AddedItems.Count != 1)
            {
               return;
            }

            InvokeChangeCommand(e.AddedItems[0]);
         }
         finally
         {
            _skipSelectionChanged = false;
         }
      }

      private void InvokeChangeCommand(object item)
      {
         if (Command == null)
         {
            return;
         }

         if (!Command.CanExecute(item))
         {
            return;
         }

         Command.Execute(item);
      }
   }
}