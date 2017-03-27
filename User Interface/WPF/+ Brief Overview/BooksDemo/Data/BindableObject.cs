using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BooksDemo.Data
{
   public abstract class BindableObject : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName = null)
         => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      protected void SetProperty<T>(ref T anItem, T aNewValue, [CallerMemberName] string aPropertyName = null)
      {
         if (!EqualityComparer<T>.Default.Equals(anItem, aNewValue))
         {
            anItem = aNewValue;
            OnPropertyChanged(aPropertyName);
         }
      }
   }
}