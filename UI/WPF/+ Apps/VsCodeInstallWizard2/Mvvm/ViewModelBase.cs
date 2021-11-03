using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MultiStudio.VsCodeInstallWizard.Mvvm
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(int propertyValue)
        {
            VerifyPropertyName(propertyValue);
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyValue.ToString()));
        }

        protected void SetProperty<T>(ref T targetValue, T value, [CallerMemberName] string callerName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(targetValue, value))
            {
                targetValue = value;
                OnPropertyChanged(callerName);
            }
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new ArgumentNullException($"{GetType().Name} does not contain property: {propertyName}");
            }
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(int propertyValue)
        {
            if (TypeDescriptor.GetProperties(this)[propertyValue] == null)
            {
                throw new ArgumentNullException($"{GetType().Name} does not contain property: {propertyValue}");
            }
        }
    }
}