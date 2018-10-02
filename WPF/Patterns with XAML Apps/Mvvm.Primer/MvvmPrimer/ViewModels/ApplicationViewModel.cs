using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmPrimer.Commands;
using MvvmPrimer.Infrastructure;
using MvvmPrimer.Models;
using MvvmPrimer.Services;

namespace MvvmPrimer.ViewModels
{
	public sealed class ApplicationViewModel : ViewModelBase
	{
		private RelayCommand _addCommand;
		private readonly IDialogService _dialogService;
		private RelayCommand _doubleCommand;
		private readonly IFileService _fileService;
		private RelayCommand _openCommand;
		private RelayCommand _removeCommand;
		private RelayCommand _saveCommand;
		private PhoneViewModel _selectedPhone;

		public ApplicationViewModel(IFileService fileService, IDialogService dialogService)
		{
			_fileService = fileService;
			_dialogService = dialogService;
		}

		public ObservableCollection<PhoneViewModel> Phones { get; } =
			new ObservableCollection<PhoneViewModel>(new List<PhoneViewModel>
			{
				new PhoneViewModel(new Phone {Title = "iPhone 7", Company = "Apple", Price = 56000}),
				new PhoneViewModel(new Phone {Title = "Galaxy S7 Edge", Company = "Samsung", Price = 60000}),
				new PhoneViewModel(new Phone {Title = "Elite X3", Company = "HP", Price = 56000}),
				new PhoneViewModel(new Phone {Title = "Mi5S", Company = "Xiaomi", Price = 35000})
			});

		public PhoneViewModel SelectedPhone
		{
			get { return _selectedPhone; }
			set
			{
				_selectedPhone = value;
				OnPropertyChanged();
			}
		}

		public RelayCommand AddCommand
		{
			get
			{
				return _addCommand ?? (_addCommand = new RelayCommand(obj =>
				{
					var phoneViewModel = new PhoneViewModel();
					Phones.Insert(0, phoneViewModel);
					SelectedPhone = phoneViewModel;
				}));
			}
		}

		public RelayCommand RemoveCommand
		{
			get
			{
				return _removeCommand ?? (_removeCommand = new RelayCommand(phoneObj =>
				{
					var phoneViewModel = phoneObj as PhoneViewModel;
					if (phoneViewModel != null)
						Phones.Remove(phoneViewModel);
				}, obj => Phones.Count > 0));
			}
		}

		public RelayCommand DoubleCommand
		{
			get
			{
				return _doubleCommand ?? (_doubleCommand = new RelayCommand(obj =>
				{
					var phoneViewModel = obj as PhoneViewModel;
					if (phoneViewModel != null)
					{
						var phoneCopy = new PhoneViewModel(phoneViewModel.Phone);
						Phones.Insert(0, phoneCopy);
					}
				}));
			}
		}

		public RelayCommand SaveCommand
		{
			get
			{
				return _saveCommand ?? (_saveCommand = new RelayCommand(obj =>
				{
					try
					{
						if (_dialogService.SaveFile())
						{
							_fileService.Save(_dialogService.FilePath, Phones.ToList());
							_dialogService.Show("File has been saved");
						}
					}
					catch (Exception ex)
					{
						_dialogService.Show(ex.Message);
					}
				}));
			}
		}

		public RelayCommand OpenCommand
		{
			get
			{
				return _openCommand ?? (_openCommand = new RelayCommand(obj =>
				{
					try
					{
						if (_dialogService.OpenFile())
						{
							var phones = _fileService.Open(_dialogService.FilePath);
							Phones.Clear();
							phones.ForEach(phoneVm => Phones.Add(phoneVm));
							_dialogService.Show("File has been opened");
						}
					}
					catch (Exception ex)
					{
						_dialogService.Show(ex.Message);
					}
				}));
			}
		}
	}
}