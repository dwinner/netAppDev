using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AsyncLib.Annotations;

namespace AsyncLib
{
   /// <summary>
   /// Базовый класс для поддержки механизма уведомлений для WPF
   /// </summary>
   public class BindableBase : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         var propertyChanged = PropertyChanged;
         if (propertyChanged != null)
         {
            propertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
      }

      protected void SetProperty<T>(ref T prop, T value, [CallerMemberName] string callerName = "")
      {
         if (!EqualityComparer<T>.Default.Equals(prop, value))
         {
            prop = value;
            OnPropertyChanged(callerName);
         }
      }
   }
}