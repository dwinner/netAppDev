using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SQLiteApp.Models;

namespace SQLiteApp.Infrastructure
{
	public sealed class SqlitePhoneStoreRepository : IPhoneStoreRepository
	{
		private readonly SqLiteDatabaseContext _context = new SqLiteDatabaseContext();

		public async Task<bool> AddAsync(Phone aPhone)
		{
			_context.Phones.Add(aPhone);
			var affected = await _context.SaveChangesAsync().ConfigureAwait(false);
			return affected == 1;
		}

		public async Task<bool> UpdateAsync(Phone aPhone)
		{
			var phoneToUpdate = await _context.Phones.FindAsync(aPhone.Id).ConfigureAwait(false);
			if (phoneToUpdate != null)
			{
				phoneToUpdate.Company = aPhone.Company;
				phoneToUpdate.Title = aPhone.Title;
				phoneToUpdate.Price = aPhone.Price;
				_context.Entry(phoneToUpdate).State = EntityState.Modified;
				var affected = await _context.SaveChangesAsync().ConfigureAwait(false);
				return affected == 1;
			}

			return false;
		}

		public async Task<bool> RemoveAsync(Phone aPhone)
		{
			_context.Phones.Remove(aPhone);
			var affected = await _context.SaveChangesAsync().ConfigureAwait(false);
			return affected > 0;
		}

		public IEnumerable<Phone> Phones => _context.Phones.Local.ToBindingList();
	}
}