using System.Data.Entity;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SQLiteApp.Infrastructure;

namespace SQLiteApp.ViewModels
{
	public sealed class MainWindowViewModel : ReactiveObject
	{
		public MainWindowViewModel(IPhoneStoreRepository repository)
		{
			var appCtx = new SqLiteDatabaseContext();
			appCtx.Phones.Load();
			Phones = new ReactiveList<PhoneViewModel>( /*repository.Phones.Select(phone => new PhoneViewModel(phone))*/);
			SetupAddCommand();
			SetupDeleteMethod();
			SetupEditCommand();
		}

		[Reactive]
		public ReactiveList<PhoneViewModel> Phones { get; set; }

		[Reactive]
		public PhoneViewModel SelectedPhone { get; set; }

		public ReactiveCommand<Unit, Unit> AddCommand { get; private set; }

		public ReactiveCommand<Unit, Unit> DeleteCommand { get; private set; }

		public ReactiveCommand<Unit, Unit> EditCommand { get; private set; }

		private void SetupEditCommand()
		{
			EditCommand = ReactiveCommand.Create(() =>
			{
				if (SelectedPhone != null)
				{
					var phoneWindow = new PhoneWindow(new PhoneViewModel(SelectedPhone));
					if (phoneWindow.ShowDialog() == true)
					{
						SelectedPhone.Price = phoneWindow.PhoneViewModel.Price;
						SelectedPhone.Company = phoneWindow.PhoneViewModel.Company;
						SelectedPhone.Title = phoneWindow.PhoneViewModel.Title;
						// TODO: Add cross cutting notification for affected model + view model
						//await repository.UpdateAsync(phoneWindow.PhoneViewModel).ConfigureAwait(true);
					}
				}
			});
		}

		private void SetupDeleteMethod()
		{
			DeleteCommand = ReactiveCommand.Create(() =>
			{
				if (SelectedPhone != null)
					Phones.Remove(SelectedPhone);
			});
		}

		private void SetupAddCommand()
		{
			AddCommand = ReactiveCommand.Create(() =>
			{
				var phoneWindow = new PhoneWindow(new PhoneViewModel());
				if (phoneWindow.ShowDialog() == true)
				{
					var newPhone = phoneWindow.PhoneViewModel;
					Phones.Add(newPhone);
					// TODO: Add cross cutting notification for affected model
					//await repository.AddAsync(newPhone).ConfigureAwait(true);
				}
			});
		}
	}
}