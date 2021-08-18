using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SQLiteApp.Infrastructure;
using SQLiteApp.Models;
using static ReactiveUI.RxApp;

namespace SQLiteApp.ViewModels
{
	public sealed class MainWindowViewModel : ReactiveObject
	{
		private readonly IPhoneStoreRepository _repository;

		public MainWindowViewModel(IPhoneStoreRepository repository)
		{
			_repository = repository;
			Phones = new ReactiveList<PhoneViewModel>(_repository.Phones.Select(phone => new PhoneViewModel(phone)));
			SetupAddCommand();
			SetupDeleteMethod();
			SetupEditCommand();
		}

		[Reactive]
		public ReactiveList<PhoneViewModel> Phones { get; set; }

		[Reactive]
		public PhoneViewModel SelectedPhone { get; set; }

		public ReactiveCommand<Unit, Phone> AddCommand { get; private set; }

		public ReactiveCommand<Unit, Unit> DeleteCommand { get; private set; }

		public ReactiveCommand<Unit, Phone> EditCommand { get; private set; }

		private void SetupAddCommand()
		{
			AddCommand = ReactiveCommand.CreateFromTask<Unit, Phone>(async unit =>
			{
				var phoneWindow = new PhoneWindow(new PhoneViewModel());
				if (phoneWindow.ShowDialog() == true)
				{
					var newPhone = phoneWindow.PhoneViewModel;
					var addedPhone = await _repository.AddAsync(newPhone).ConfigureAwait(true);
					return addedPhone;
				}

				return null;
			});

			AddCommand.ObserveOn(MainThreadScheduler).Subscribe(newPhone => { Phones.Add(newPhone); });
			AddCommand.ThrownExceptions.Subscribe(ex => { MessageBox.Show(ex.Message); });
		}

		private void SetupEditCommand()
		{
			EditCommand = ReactiveCommand.CreateFromTask<Unit, Phone>(async unit =>
			{
				var phoneWindow = new PhoneWindow(new PhoneViewModel(SelectedPhone));
				if (phoneWindow.ShowDialog() == true)
				{
					var phone = await _repository.UpdateAsync(new Phone(phoneWindow.PhoneViewModel)).ConfigureAwait(false);
					return phone;
				}

				return new Phone(SelectedPhone);
			}, this.WhenAny(vm => vm.SelectedPhone, change => change.Value != null));

			EditCommand.ObserveOn(MainThreadScheduler).Subscribe(updatedPhone =>
			{
				SelectedPhone.Price = updatedPhone.Price;
				SelectedPhone.Company = updatedPhone.Company;
				SelectedPhone.Title = updatedPhone.Title;
			});

			EditCommand.ThrownExceptions.Subscribe(ex => { MessageBox.Show(ex.Message); });
		}

		private void SetupDeleteMethod()
		{
			DeleteCommand =
				ReactiveCommand.CreateFromTask<Unit, Unit>(
					async dummy =>
					{
						await _repository.RemoveAsync(SelectedPhone).ConfigureAwait(false);
						return Unit.Default;
					},
					this.WhenAny(vm => vm.SelectedPhone, change => change.Value != null));

			DeleteCommand.ObserveOn(MainThreadScheduler).Subscribe(dummy => { Phones.Remove(SelectedPhone); });
			DeleteCommand.ThrownExceptions.Subscribe(ex => { MessageBox.Show(ex.Message); });
		}
	}
}