using System.Data.Entity;
using System.Linq;
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
			Phones = new ReactiveList<PhoneViewModel>(repository.Phones.Select(phone => new PhoneViewModel(phone)));

			AddCommand = ReactiveCommand.CreateFromTask(async () =>
			{
				var phoneWindow = new PhoneWindow(new PhoneViewModel());
				if (phoneWindow.ShowDialog() == true)
				{
					var newPhone = phoneWindow.PhoneViewModel;
					Phones.Add(newPhone);
					await repository.AddAsync(newPhone).ConfigureAwait(true);	// TODO: Add cross cutting notification for affected model
				}
			});			

			DeleteCommand = ReactiveCommand.CreateFromTask(async () =>
			{
				if (SelectedPhone != null)
				{
					Phones.Remove(SelectedPhone);
					await repository.RemoveAsync(SelectedPhone).ConfigureAwait(true); // TODO: Add cross cutting notification for affected model
				}
			});

			EditCommand = ReactiveCommand.CreateFromTask(async () =>
			{
				if (SelectedPhone != null)
				{
					var phoneWindow = new PhoneWindow(new PhoneViewModel(SelectedPhone));
					if (phoneWindow.ShowDialog() == true)
					{
						await repository.UpdateAsync(phoneWindow.PhoneViewModel).ConfigureAwait(true);   // TODO: Add cross cutting notification for affected model + view model
					}
				}
			});
		}

		[Reactive]
		public ReactiveList<PhoneViewModel> Phones { get; set; }

		[Reactive]
		public PhoneViewModel SelectedPhone { get; set; }

		public ReactiveCommand<Unit, Unit> AddCommand { get; }

		public ReactiveCommand<Unit, Unit> DeleteCommand { get; }

		public ReactiveCommand<Unit, Unit> EditCommand { get; }
	}
}