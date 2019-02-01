using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using MvvmPrimer.ViewModels;

namespace MvvmPrimer.Services
{
	public sealed class JsonFileService : IFileService
	{
		public List<PhoneViewModel> Open(string aFileName)
		{
			List<PhoneViewModel> phones;
			var jsonFormatter = new DataContractJsonSerializer(typeof(List<PhoneViewModel>));
			using (var fileStream = new FileStream(aFileName, FileMode.OpenOrCreate))
			{
				phones = jsonFormatter.ReadObject(fileStream) as List<PhoneViewModel>;
			}

			return phones;
		}

		public void Save(string aFileName, List<PhoneViewModel> phones)
		{
			var jsonFormatter = new DataContractJsonSerializer(typeof(List<PhoneViewModel>));
			using (var fileStream = new FileStream(aFileName, FileMode.Create))
			{
				jsonFormatter.WriteObject(fileStream, phones);
			}
		}
	}
}