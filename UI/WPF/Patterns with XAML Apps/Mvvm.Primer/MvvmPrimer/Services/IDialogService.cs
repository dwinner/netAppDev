namespace MvvmPrimer.Services
{
	public interface IDialogService
	{
		void Show(string aMessage);
		string FilePath { get; }
		bool OpenFile();
		bool SaveFile();
	}
}