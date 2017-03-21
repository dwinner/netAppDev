using System.Collections.Generic;
using MvvmPrimer.ViewModels;

namespace MvvmPrimer.Services
{
	public interface IFileService
	{
		List<PhoneViewModel> Open(string aFileName);
		void Save(string aFileName, List<PhoneViewModel> phones);
	}
}