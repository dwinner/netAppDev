using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SQLiteApp.Models;

namespace SQLiteApp.Infrastructure
{
	public sealed class SqlitePhoneStoreRepository : IPhoneStoreRepository
	{
		private readonly SqLiteDatabaseContext _context = new SqLiteDatabaseContext();

		public SqlitePhoneStoreRepository()
		{
			_context.Phones.Load();
		}

		public async Task<Phone> AddAsync(Phone aPhone)
		{
			var newPhone = _context.Phones.Add(aPhone);
			await _context.SaveChangesAsync().ConfigureAwait(false);
			return newPhone;
		}

		public async Task<Phone> UpdateAsync(Phone aPhone)
		{
			var phoneToUpdate = await _context.Phones.FindAsync(aPhone.Id).ConfigureAwait(false);
			if (phoneToUpdate != null)
			{
				phoneToUpdate.Company = aPhone.Company;
				phoneToUpdate.Title = aPhone.Title;
				phoneToUpdate.Price = aPhone.Price;
				_context.Entry(phoneToUpdate).State = EntityState.Modified;
				await _context.SaveChangesAsync().ConfigureAwait(false);
				return phoneToUpdate;
			}

			return aPhone;
		}

		public async Task<Phone> RemoveAsync(Phone aPhone)
		{
			var phoneToDelete = await _context.Phones.FindAsync(aPhone.Id).ConfigureAwait(false);
			if (phoneToDelete != null)
			{
				var deletedPhone = _context.Phones.Remove(phoneToDelete);
				await _context.SaveChangesAsync().ConfigureAwait(false);
				return deletedPhone;
			}

			return aPhone;
		}

		public IEnumerable<Phone> Phones => _context.Phones.Local.ToBindingList();
	}
}