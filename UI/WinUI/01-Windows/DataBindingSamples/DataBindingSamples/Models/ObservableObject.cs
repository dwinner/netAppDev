﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataBindingSamples.Models
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual bool SetProperty<T>(ref T item, T value, [CallerMemberName] string? propertyName = default)
        {
            if (EqualityComparer<T>.Default.Equals(item, value)) return false;
            item = value;   
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
