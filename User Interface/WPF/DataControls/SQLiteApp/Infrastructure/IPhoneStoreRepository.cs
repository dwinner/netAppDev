using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteApp.Models;

namespace SQLiteApp.Infrastructure
{
	public interface IPhoneStoreRepository
	{
		Task<bool> AddAsync(Phone aPhone);
		Task<bool> UpdateAsync(Phone aPhone);
		Task<bool> RemoveAsync(Phone aPhone);
		IEnumerable<Phone> Phones { get; }
	}
}