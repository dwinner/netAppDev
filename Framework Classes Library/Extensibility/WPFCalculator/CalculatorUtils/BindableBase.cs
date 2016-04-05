using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CalculatorUtils.Properties;

namespace CalculatorUtils
{
   /// <summary>
   /// Базовый класс для поддержки механизмов уведомлений
   /// </summary>
   public abstract class BindableBase : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
      }

      protected void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = "")
      {
         if (!EqualityComparer<T>.Default.Equals(item, value))
         {
            item = value;
            OnPropertyChanged();
         }
      }
   }
}
