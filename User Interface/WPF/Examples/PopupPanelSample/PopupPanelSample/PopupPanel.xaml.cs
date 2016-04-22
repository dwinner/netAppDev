using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PopupPanelSample
{
   /// <summary>
   ///    Panel for handling Popups:
   ///    - Control with name PART_DefaultFocusControl will have default focus
   ///    - Can define PopupParent to determine if this popup should be hosted in a parent panel or not
   ///    - Can define the property EnterKeyCommand to specifify what command to run when the Enter key is pressed
   ///    - Can define the property EscapeKeyCommand to specify what command to run when the Escape key is pressed
   ///    - Can define BackgroundOpacity to specify how opaque the background will be. Value is between 0 and 1.
   /// </summary>
   public partial class PopupPanel
   {
      #region Constructors

      public PopupPanel()
      {
         InitializeComponent();
         DataContextChanged += OnPopupDataContextChanged;

         // Register a PropertyChanged event on IsPopupVisible
         var propertyDescriptor = DependencyPropertyDescriptor.FromProperty(IsPopupVisibleProperty, typeof (PopupPanel));
         if (propertyDescriptor != null)
            propertyDescriptor.AddValueChanged(this, delegate { OnIsPopupVisibleChanged(); });

         propertyDescriptor = DependencyPropertyDescriptor.FromProperty(ContentProperty, typeof (PopupPanel));
         if (propertyDescriptor != null)
            propertyDescriptor.AddValueChanged(this, delegate { Content_Changed(); });
      }

      #endregion // Constructors

      #region Fields

      bool isLoading; // Flag to tell identify when DataContext changes
      UIElement lastFocusControl; // Last control that had focus when popup visibility changes, but isn't closed

      #endregion // Fields

      #region Events

      #region Property Change Events

      // When DataContext changes
      void OnPopupDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
      {
         DisableAnimationWhileLoading();
      }

      // When Content Property changes
      void Content_Changed()
      {
         DisableAnimationWhileLoading();
      }

      // Sets an IsLoading flag so storyboard doesn't run while loading
      void DisableAnimationWhileLoading()
      {
         isLoading = true;
         Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(delegate { isLoading = false; }));
      }

      // Run storyboard when IsPopupVisible property changes to true
      void OnIsPopupVisibleChanged()
      {
         var isShown = GetIsPopupVisible(this);

         if (isShown && !isLoading)
         {
            var panel = VisualHelpers.FindChild<FrameworkElement>(this, "PopupPanelContent");
            if (panel != null)
            {
               // Run Storyboard
               var animation = (Storyboard) panel.FindResource("ShowEditPanelStoryboard");
               animation.Begin();
            }
         }

         // When hiding popup, clear the LastFocusControl
         if (!isShown)
         {
            lastFocusControl = null;
         }
      }

      #endregion // Change Events

      #region Popup Events

      // When visibility is changed, set the default focus
      void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
      {
         if (!(bool) e.NewValue)
            return;

         var popupControl = VisualHelpers.FindChild<ContentControl>(this, "PopupContentControl");
         Dispatcher.BeginInvoke(DispatcherPriority.Render,
            new Action(delegate
            {
               // Verify object really is visible because sometimes it's not once we switch to Render
               if (!GetIsPopupVisible(this))
               {
                  return;
               }

               if (lastFocusControl != null && lastFocusControl.Focusable)
               {
                  lastFocusControl.Focus();
               }
               else
               {
                  lastFocusControl = VisualHelpers.FindChild<UIElement>(popupControl, "PART_DefaultFocusControl");

                  // If we can find the part named PART_DefaultFocusControl, set focus to it 
                  if (lastFocusControl != null && lastFocusControl.Focusable)
                  {
                     lastFocusControl.Focus();
                  }
                  else
                  {
                     lastFocusControl = VisualHelpers.FindFirstFocusableChild(popupControl);

                     // If no DefaultFocusControl found, try and set focus to the first focusable element found in popup 
                     if (lastFocusControl != null)
                     {
                        lastFocusControl.Focus();
                     }
                     else
                     {
                        // Just give the Popup UserControl focus so it can handle keyboard input
                        popupControl.Focus();
                     }
                  }
               }
            }
               )
            );
      }

      // When popup loses focus but isn't hidden, store the last element that had focus so we can put it back later
      void OnLostFocus(object sender, RoutedEventArgs e)
      {
         var focusScope = FocusManager.GetFocusScope(this);
         lastFocusControl = FocusManager.GetFocusedElement(focusScope) as UIElement;
      }

      // Keyboard Events
      void OnPreviewKeyDown(object sender, KeyEventArgs e)
      {
         var popup = VisualHelpers.FindAncester<PopupPanel>((DependencyObject) sender);
         var cmd = GetPopupEscapeKeyCommand(popup);

         switch (e.Key)
         {
            case Key.Escape:
               if (cmd != null && cmd.CanExecute(null))
               {
                  cmd.Execute(null);
                  e.Handled = true;
               }
               else
               {
                  // By default the Escape Key closes the popup when pressed
                  var expression = GetBindingExpression(IsPopupVisibleProperty);
                  if (expression != null)
                  {
                     var dataType = expression.DataItem.GetType();
                     dataType.GetProperties().Single(x => x.Name == expression.ParentBinding.Path.Path)
                        .SetValue(expression.DataItem, false, null);
                  }
               }
               break;

            case Key.Enter:
               // Don't want to run Enter command if focus is in a TextBox with AcceptsReturn = True
               if (
                  !(e.KeyboardDevice.FocusedElement is TextBox && ((TextBox) e.KeyboardDevice.FocusedElement).AcceptsReturn) &&
                  cmd != null && cmd.CanExecute(null))
               {
                  cmd.Execute(null);
                  e.Handled = true;
               }
               break;
         }
      }

      #endregion // Popup Events

      #endregion // Events

      #region Dependency Properties

      // Parent for Popup

      #region PopupParent

      public static readonly DependencyProperty PopupParentProperty =
         DependencyProperty.Register("PopupParent", typeof (FrameworkElement),
            typeof (PopupPanel), new PropertyMetadata(null, null, CoercePopupParent));

      static object CoercePopupParent(DependencyObject obj, object value)
      {
         // If PopupParent is null, return the Window object
         return value ?? VisualHelpers.FindAncester<Window>(obj);
      }

      public FrameworkElement PopupParent
      {
         get { return (FrameworkElement) GetValue(PopupParentProperty); }
         set { SetValue(PopupParentProperty, value); }
      }

      // Providing Get/Set methods makes them show up in the XAML designer
      public static FrameworkElement GetPopupParent(DependencyObject obj)
      {
         return (FrameworkElement) obj.GetValue(PopupParentProperty);
      }

      public static void SetPopupParent(DependencyObject obj, FrameworkElement value)
      {
         obj.SetValue(PopupParentProperty, value);
      }

      #endregion

      // Popup Visibility - If popup is shown or not

      #region IsPopupVisibleProperty

      public static readonly DependencyProperty IsPopupVisibleProperty =
         DependencyProperty.Register("IsPopupVisible", typeof (bool),
            typeof (PopupPanel), new PropertyMetadata(false, null));

      public static bool GetIsPopupVisible(DependencyObject obj)
      {
         return (bool) obj.GetValue(IsPopupVisibleProperty);
      }

      public static void SetIsPopupVisible(DependencyObject obj, bool value)
      {
         obj.SetValue(IsPopupVisibleProperty, value);
      }

      #endregion // IsPopupVisibleProperty

      // Transparency level for the background filler outside the popup

      #region BackgroundOpacityProperty

      public static readonly DependencyProperty BackgroundOpacityProperty =
         DependencyProperty.Register("BackgroundOpacity", typeof (double),
            typeof (PopupPanel), new PropertyMetadata(.5, null));

      public static double GetBackgroundOpacity(DependencyObject obj)
      {
         return (double) obj.GetValue(BackgroundOpacityProperty);
      }

      public static void SetBackgroundOpacity(DependencyObject obj, double value)
      {
         obj.SetValue(BackgroundOpacityProperty, value);
      }

      #endregion ShowBackgroundProperty

      // Command to execute when Enter key is pressed

      #region PopupEnterKeyCommandProperty

      public static readonly DependencyProperty PopupEnterKeyCommandProperty =
         DependencyProperty.RegisterAttached("PopupEnterKeyCommand", typeof (ICommand),
            typeof (PopupPanel), new PropertyMetadata(null, null));

      public static ICommand GetPopupEnterKeyCommand(DependencyObject obj)
      {
         return (ICommand) obj.GetValue(PopupEnterKeyCommandProperty);
      }

      public static void SetPopupEnterKeyCommand(DependencyObject obj, ICommand value)
      {
         obj.SetValue(PopupEnterKeyCommandProperty, value);
      }

      #endregion PopupEnterKeyCommandProperty

      // Command to execute when Enter key is pressed

      #region PopupEscapeKeyCommandProperty

      public static readonly DependencyProperty PopupEscapeKeyCommandProperty =
         DependencyProperty.RegisterAttached("PopupEscapeKeyCommand", typeof (ICommand),
            typeof (PopupPanel), new PropertyMetadata(null, null));

      static ICommand GetPopupEscapeKeyCommand(DependencyObject obj)
      {
         return (ICommand) obj.GetValue(PopupEscapeKeyCommandProperty);
      }

      public static void SetPopupEscapeKeyCommand(DependencyObject obj, ICommand value)
      {
         obj.SetValue(PopupEscapeKeyCommandProperty, value);
      }

      #endregion PopupEscapeKeyCommandProperty

      #endregion Dependency Properties      
   }   
}