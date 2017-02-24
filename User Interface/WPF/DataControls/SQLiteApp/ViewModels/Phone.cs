using PostSharp.Patterns.Contracts;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SQLiteApp.ViewModels
{
	public sealed class Phone : ReactiveObject
	{
		public int Id { get; set; }

		[Required]
		[Reactive]
		public string Title { get; set; }

		[Required]
		[Reactive]
		public string Company { get; set; }

		[StrictlyPositive]
		[Reactive]
		public int Price { get; set; }
	}
}