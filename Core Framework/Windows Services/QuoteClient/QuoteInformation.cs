using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuoteClient
{
   public class QuoteInformation : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;      

      private string _quote;

      public string Quote
      {
         get
         {
            return _quote;
         }
         internal set
         {            
            SetProperty(ref _quote, value);
         }
      }

      private bool _enableRequest;

      public bool EnableRequest
      {
         get
         {
            return _enableRequest;
         }
         internal set
         {
            SetProperty(ref _enableRequest, value);
         }
      }

      public QuoteInformation()
      {
         EnableRequest = true;
      }

      private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
      {
         if (!EqualityComparer<T>.Default.Equals(field, value))
         {
            field = value;
            var handler = PropertyChanged;
            if (handler != null)
            {
               handler(this, new PropertyChangedEventArgs(propertyName));
            }
         }
      }
   }
}