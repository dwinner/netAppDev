using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CalculatorUtils.Annotations;

namespace CalculatorUtils
{
	public abstract class BindableBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		protected void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = "")
		{
			if (!EqualityComparer<T>.Default.Equals(item, value))
			{
				OnPropertyChanged(propertyName);
			}
		}
	}
}