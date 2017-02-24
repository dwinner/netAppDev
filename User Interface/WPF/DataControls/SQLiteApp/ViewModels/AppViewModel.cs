using System.Data.Entity;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SQLiteApp.Infrastructure;

namespace SQLiteApp.ViewModels
{
	public sealed class AppViewModel : ReactiveObject
	{
		public AppViewModel()
		{
			var appCtx = new ApplicationContext();
			appCtx.Phones.Load();
			Phones = new ReactiveList<Phone>(appCtx.Phones.Local.ToBindingList());

			AddCommand = ReactiveCommand.CreateFromTask(async () =>
			{
				var phoneWindow = new PhoneWindow(new Phone());
				if (phoneWindow.ShowDialog() == true)
				{
					var newPhone = phoneWindow.Phone;
					appCtx.Phones.Add(newPhone);
					await appCtx.SaveChangesAsync().ConfigureAwait(true);
				}
			});

			DeleteCommand = ReactiveCommand.CreateFromTask(async () =>
			{
				if (SelectedPhone != null)
				{
					appCtx.Phones.Remove(SelectedPhone);
					await appCtx.SaveChangesAsync().ConfigureAwait(true);
				}
			});

			EditCommand = ReactiveCommand.CreateFromTask(async () =>
			{
				if (SelectedPhone != null)
				{
					var phoneWindow = new PhoneWindow(new Phone
					{
						Id = SelectedPhone.Id,
						Company = SelectedPhone.Company,
						Price = SelectedPhone.Price,
						Title = SelectedPhone.Title
					});

					if (phoneWindow.ShowDialog() == true)
					{
						SelectedPhone = await appCtx.Phones.FindAsync(phoneWindow.Phone.Id).ConfigureAwait(true);
						if (SelectedPhone != null)
						{
							SelectedPhone.Company = phoneWindow.Phone.Company;
							SelectedPhone.Title = phoneWindow.Phone.Title;
							SelectedPhone.Price = phoneWindow.Phone.Price;
							appCtx.Entry(SelectedPhone).State = EntityState.Modified;
							await appCtx.SaveChangesAsync().ConfigureAwait(true);
						}
					}
				}
			});
		}

		[Reactive]
		public ReactiveList<Phone> Phones { get; set; }

		[Reactive]
		public Phone SelectedPhone { get; set; }

		public ReactiveCommand<Unit, Unit> AddCommand { get; }

		public ReactiveCommand<Unit, Unit> DeleteCommand { get; }

		public ReactiveCommand<Unit, Unit> EditCommand { get; }
	}
}