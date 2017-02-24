using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLiteApp.Annotations;
using PostSharp.Patterns.Contracts;

namespace SQLiteApp.Models
{
	public sealed class Phone : INotifyPropertyChanged
	{
		private string _company;
		private int _price;
		private string _title;

		public int Id { get; set; }

		[Required]
		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				OnPropertyChanged();
			}
		}

		[Required]
		public string Company
		{
			get { return _company; }
			set
			{
				_company = value;
				OnPropertyChanged();
			}
		}

		[StrictlyPositive]
		public int Price
		{
			get { return _price; }
			set
			{
				_price = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}