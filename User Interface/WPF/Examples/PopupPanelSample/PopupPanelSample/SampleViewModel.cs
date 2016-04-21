using System.Collections.Generic;
using System.ComponentModel;

namespace PopupPanelSample
{
   public sealed class SampleViewModel : INotifyPropertyChanged
   {
      public SampleViewModel( )
      {
         PopupContentOptions = new List< object >
         {
            new Address(),
            new Phone(),
            new Email()
         };
      }

      #region Fields

      object popupContent;
      bool isPopupVisible;
      List< object > popupContentOptions;

      #endregion

      #region Properties

      public object PopupContent
      {
         get { return popupContent; }
         set
         {
            if ( value != popupContent )
            {
               popupContent = value;
               OnPropertyChanged( "PopupContent" );
            }
         }
      }

      public List< object > PopupContentOptions
      {
         get { return popupContentOptions; }
         set
         {
            if ( value != popupContentOptions )
            {
               popupContentOptions = value;
               OnPropertyChanged( "PopupContentOptions" );
            }
         }
      }

      public bool IsPopupVisible
      {
         get { return isPopupVisible; }
         set
         {
            if ( value != isPopupVisible )
            {
               isPopupVisible = value;
               OnPropertyChanged( "IsPopupVisible" );
            }
         }
      }

      #endregion

      #region INotifyPropertyChanged Members

      public event PropertyChangedEventHandler PropertyChanged;

      void OnPropertyChanged( string propertyName )
      {
         var handler = PropertyChanged;
         if ( handler != null )
         {
            var e = new PropertyChangedEventArgs( propertyName );
            handler( this, e );
         }
      }

      #endregion // INotifyPropertyChanged Members
   }
}