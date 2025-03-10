﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Northwind.Wpf.Annotations;

namespace Northwind.Wpf.Infrastructure
{
   public abstract class ViewModel : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}