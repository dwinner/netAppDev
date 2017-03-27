using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ValidationDemo.Properties;

namespace ValidationDemo
{
   public abstract class BindableObject : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      private void OnPropertyChanged([CallerMemberName] string propertyName = null)
         => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      protected bool SetProperty<T>(ref T anItem, T aNewValue, [CallerMemberName] string aPropertyName = null)
      {
         if (!EqualityComparer<T>.Default.Equals(anItem, aNewValue))
         {
            anItem = aNewValue;
            OnPropertyChanged(aPropertyName);
            return true;
         }

         return false;
      }
   }
}