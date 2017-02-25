using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteApp.Models;

namespace SQLiteApp.Infrastructure
{
	public interface IPhoneStoreRepository
	{
		IEnumerable<Phone> Phones { get; }
		Task<Phone> AddAsync(Phone aPhone);
		Task<Phone> UpdateAsync(Phone aPhone);
		Task<Phone> RemoveAsync(Phone aPhone);
	}
}