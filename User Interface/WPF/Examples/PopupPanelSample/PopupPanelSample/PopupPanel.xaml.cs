using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

      public PopupPanel( )
      {
         InitializeComponent();
         DataContextChanged += Popup_DataContextChanged;

         // Register a PropertyChanged event on IsPopupVisible
         var dpd = DependencyPropertyDescriptor.FromProperty( IsPopupVisibleProperty, typeof ( PopupPanel ) );
         if ( dpd != null ) dpd.AddValueChanged( this, delegate { IsPopupVisible_Changed(); } );

         dpd = DependencyPropertyDescriptor.FromProperty( ContentProperty, typeof ( PopupPanel ) );
         if ( dpd != null ) dpd.AddValueChanged( this, delegate { Content_Changed(); } );
      }

      #endregion // Constructors

      #region Fields

      bool isLoading; // Flag to tell identify when DataContext changes
      UIElement lastFocusControl; // Last control that had focus when popup visibility changes, but isn't closed

      #endregion // Fields

      #region Events

      #region Property Change Events

      // When DataContext changes
      void Popup_DataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
      {
         DisableAnimationWhileLoading();
      }

      // When Content Property changes
      void Content_Changed( )
      {
         DisableAnimationWhileLoading();
      }

      // Sets an IsLoading flag so storyboard doesn't run while loading
      void DisableAnimationWhileLoading( )
      {
         isLoading = true;
         Dispatcher.BeginInvoke( DispatcherPriority.Render,
            new Action( delegate { isLoading = false; } ) );
      }

      // Run storyboard when IsPopupVisible property changes to true
      void IsPopupVisible_Changed( )
      {
         var isShown = GetIsPopupVisible( this );

         if ( isShown && !isLoading )
         {
            var panel = FindChild< FrameworkElement >( this, "PopupPanelContent" );
            if ( panel != null )
            {
               // Run Storyboard
               var animation = ( Storyboard ) panel.FindResource( "ShowEditPanelStoryboard" );
               animation.Begin();
            }
         }

         // When hiding popup, clear the LastFocusControl
         if ( !isShown )
         {
            lastFocusControl = null;
         }
      }

      #endregion // Change Events

      #region Popup Events

      // When visibility is changed, set the default focus
      void OnVisibleChanged( object sender, DependencyPropertyChangedEventArgs e )
      {
         if ( ( bool ) e.NewValue )
         {
            var popupControl = FindChild< ContentControl >( this, "PopupContentControl" );
            Dispatcher.BeginInvoke( DispatcherPriority.Render,
               new Action( delegate
               {
                  // Verify object really is visible because sometimes it's not once we switch to Render
                  if ( !GetIsPopupVisible( this ) )
                  {
                     return;
                  }

                  if ( lastFocusControl != null && lastFocusControl.Focusable )
                  {
                     lastFocusControl.Focus();
                  }
                  else
                  {
                     lastFocusControl = FindChild< UIElement >( popupControl, "PART_DefaultFocusControl" );

                     // If we can find the part named PART_DefaultFocusControl, set focus to it 
                     if ( lastFocusControl != null && lastFocusControl.Focusable )
                     {
                        lastFocusControl.Focus();
                     }
                     else
                     {
                        lastFocusControl = FindFirstFocusableChild( popupControl );

                        // If no DefaultFocusControl found, try and set focus to the first focusable element found in popup 
                        if ( lastFocusControl != null )
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
      }

      // When popup loses focus but isn't hidden, store the last element that had focus so we can put it back later
      void OnLostFocus( object sender, RoutedEventArgs e )
      {
         var focusScope = FocusManager.GetFocusScope( this );
         lastFocusControl = FocusManager.GetFocusedElement( focusScope ) as UIElement;
      }

      // Keyboard Events
      void OnPreviewKeyDown( object sender, KeyEventArgs e )
      {
         if ( e.Key == Key.Escape )
         {
            var popup = FindAncester< PopupPanel >( ( DependencyObject ) sender );
            var cmd = GetPopupEscapeKeyCommand( popup );
            if ( cmd != null && cmd.CanExecute( null ) )
            {
               cmd.Execute( null );
               e.Handled = true;
            }
            else
            {
               // By default the Escape Key closes the popup when pressed
               var expression = GetBindingExpression( IsPopupVisibleProperty );
               if ( expression != null )
               {
                  var dataType = expression.DataItem.GetType();
                  dataType.GetProperties().Single( x => x.Name == expression.ParentBinding.Path.Path )
                     .SetValue( expression.DataItem, false, null );
               }
            }
         }
         else if ( e.Key == Key.Enter )
         {
            // Don't want to run Enter command if focus is in a TextBox with AcceptsReturn = True
            if ( !( e.KeyboardDevice.FocusedElement is TextBox &&
                    ( ( TextBox ) e.KeyboardDevice.FocusedElement ).AcceptsReturn ) )
            {
               var popup = FindAncester< PopupPanel >( ( DependencyObject ) sender );
               var cmd = GetPopupEnterKeyCommand( popup );
               if ( cmd != null && cmd.CanExecute( null ) )
               {
                  cmd.Execute( null );
                  e.Handled = true;
               }
            }
         }
      }

      #endregion // Popup Events

      #endregion // Events

      #region Dependency Properties

      // Parent for Popup

      #region PopupParent

      public static readonly DependencyProperty PopupParentProperty =
         DependencyProperty.Register( "PopupParent", typeof ( FrameworkElement ),
            typeof ( PopupPanel ), new PropertyMetadata( null, null, CoercePopupParent ) );

      static object CoercePopupParent( DependencyObject obj, object value )
      {
         // If PopupParent is null, return the Window object
         return value ?? FindAncester< Window >( obj );
      }

      public FrameworkElement PopupParent
      {
         get { return ( FrameworkElement ) GetValue( PopupParentProperty ); }
         set { SetValue( PopupParentProperty, value ); }
      }

      // Providing Get/Set methods makes them show up in the XAML designer
      public static FrameworkElement GetPopupParent( DependencyObject obj )
      {
         return ( FrameworkElement ) obj.GetValue( PopupParentProperty );
      }

      public static void SetPopupParent( DependencyObject obj, FrameworkElement value )
      {
         obj.SetValue( PopupParentProperty, value );
      }

      #endregion

      // Popup Visibility - If popup is shown or not

      #region IsPopupVisibleProperty

      public static readonly DependencyProperty IsPopupVisibleProperty =
         DependencyProperty.Register( "IsPopupVisible", typeof ( bool ),
            typeof ( PopupPanel ), new PropertyMetadata( false, null ) );

      public static bool GetIsPopupVisible( DependencyObject obj )
      {
         return ( bool ) obj.GetValue( IsPopupVisibleProperty );
      }

      public static void SetIsPopupVisible( DependencyObject obj, bool value )
      {
         obj.SetValue( IsPopupVisibleProperty, value );
      }

      #endregion // IsPopupVisibleProperty

      // Transparency level for the background filler outside the popup

      #region BackgroundOpacityProperty

      public static readonly DependencyProperty BackgroundOpacityProperty =
         DependencyProperty.Register( "BackgroundOpacity", typeof ( double ),
            typeof ( PopupPanel ), new PropertyMetadata( .5, null ) );

      public static double GetBackgroundOpacity( DependencyObject obj )
      {
         return ( double ) obj.GetValue( BackgroundOpacityProperty );
      }

      public static void SetBackgroundOpacity( DependencyObject obj, double value )
      {
         obj.SetValue( BackgroundOpacityProperty, value );
      }

      #endregion ShowBackgroundProperty

      // Command to execute when Enter key is pressed

      #region PopupEnterKeyCommandProperty

      public static readonly DependencyProperty PopupEnterKeyCommandProperty =
         DependencyProperty.RegisterAttached( "PopupEnterKeyCommand", typeof ( ICommand ),
            typeof ( PopupPanel ), new PropertyMetadata( null, null ) );

      public static ICommand GetPopupEnterKeyCommand( DependencyObject obj )
      {
         return ( ICommand ) obj.GetValue( PopupEnterKeyCommandProperty );
      }

      public static void SetPopupEnterKeyCommand( DependencyObject obj, ICommand value )
      {
         obj.SetValue( PopupEnterKeyCommandProperty, value );
      }

      #endregion PopupEnterKeyCommandProperty

      // Command to execute when Enter key is pressed

      #region PopupEscapeKeyCommandProperty

      public static readonly DependencyProperty PopupEscapeKeyCommandProperty =
         DependencyProperty.RegisterAttached( "PopupEscapeKeyCommand", typeof ( ICommand ),
            typeof ( PopupPanel ), new PropertyMetadata( null, null ) );

      public static ICommand GetPopupEscapeKeyCommand( DependencyObject obj )
      {
         return ( ICommand ) obj.GetValue( PopupEscapeKeyCommandProperty );
      }

      public static void SetPopupEscapeKeyCommand( DependencyObject obj, ICommand value )
      {
         obj.SetValue( PopupEscapeKeyCommandProperty, value );
      }

      #endregion PopupEscapeKeyCommandProperty

      #endregion Dependency Properties

      #region Visual Tree Helpers

      public static UIElement FindFirstFocusableChild( DependencyObject parent )
      {
         // Confirm parent is valid. 
         if ( parent == null ) return null;

         UIElement foundChild = null;

         var childrenCount = VisualTreeHelper.GetChildrenCount( parent );
         for ( var i = 0; i < childrenCount; i++ )
         {
            var child = VisualTreeHelper.GetChild( parent, i ) as UIElement;

            // This is returning me things like ContentControls, so for now filtering to buttons/textboxes only
            if ( child != null && child.Focusable && child.IsVisible )
            {
               foundChild = child;
               break;
            }
            // recursively drill down the tree
            foundChild = FindFirstFocusableChild( child );

            // If the child is found, break so we do not overwrite the found child. 
            if ( foundChild != null ) break;
         }
         return foundChild;
      }

      public static T FindAncester< T >( DependencyObject current )
         where T : DependencyObject
      {
         // Need this call to avoid returning current object if it is the same type as parent we are looking for
         current = VisualTreeHelper.GetParent( current );

         while ( current != null )
         {
            if ( current is T )
            {
               return ( T ) current;
            }
            current = VisualTreeHelper.GetParent( current );
         }
         ;
         return null;
      }

      /// <summary>
      ///    Looks for a child control within a parent by name
      /// </summary>
      public static T FindChild< T >( DependencyObject parent, string childName )
         where T : DependencyObject
      {
         // Confirm parent and childName are valid. 
         if ( parent == null ) return null;

         T foundChild = null;

         var childrenCount = VisualTreeHelper.GetChildrenCount( parent );
         for ( var i = 0; i < childrenCount; i++ )
         {
            var child = VisualTreeHelper.GetChild( parent, i );
            // If the child is not of the request child type child
            var childType = child as T;
            if ( childType == null )
            {
               // recursively drill down the tree
               foundChild = FindChild< T >( child, childName );

               // If the child is found, break so we do not overwrite the found child. 
               if ( foundChild != null ) break;
            }
            else if ( !string.IsNullOrEmpty( childName ) )
            {
               var frameworkElement = child as FrameworkElement;
               // If the child's name is set for search
               if ( frameworkElement != null && frameworkElement.Name == childName )
               {
                  // if the child's name is of the request name
                  foundChild = ( T ) child;
                  break;
               }
               // recursively drill down the tree
               foundChild = FindChild< T >( child, childName );

               // If the child is found, break so we do not overwrite the found child. 
               if ( foundChild != null ) break;
            }
            else
            {
               // child element found.
               foundChild = ( T ) child;
               break;
            }
         }

         return foundChild;
      }

      #endregion
   }

   // Converter for Popup positioning
   public class ValueDividedByParameterConverter : IValueConverter
   {
      public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
      {
         double n, d;
         if ( double.TryParse( value.ToString(), out n )
              && double.TryParse( parameter.ToString(), out d )
              && d != 0 )
         {
            return n/d;
         }

         return 0;
      }

      public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
      {
         throw new NotImplementedException();
      }
   }
}