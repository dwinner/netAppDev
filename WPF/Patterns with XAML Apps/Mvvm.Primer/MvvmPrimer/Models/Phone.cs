namespace MvvmPrimer.Models
{
	public sealed class Phone
	{
		public string Title { get; set; }

		public string Company { get; set; }

		public int Price { get; set; }

		public override string ToString()
			=> $"{nameof(Title)}: {Title}, {nameof(Company)}: {Company}, {nameof(Price)}: {Price}";
	}
}