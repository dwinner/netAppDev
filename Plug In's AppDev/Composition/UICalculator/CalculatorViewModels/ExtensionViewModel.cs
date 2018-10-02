using System;
using CalculatorContract;
using CalculatorUtils;

namespace CalculatorViewModels
{
	public class ExtensionViewModel : BindableBase
	{
		private object _ui;

		public ExtensionViewModel(Lazy<ICalculatorExtension, CalculatorExtensionMetadataAttribute> extension)
		{
			ActivateCommand = new DelegateCommand(OnActivate);
			CloseCommand = new DelegateCommand(OnClose);
			Extension = extension;
		}

		public DelegateCommand ActivateCommand { get; }
		public DelegateCommand CloseCommand { get; }
		public Lazy<ICalculatorExtension, CalculatorExtensionMetadataAttribute> Extension { get; }

		public object Ui
		{
			get { return _ui; }
			set { SetProperty(ref _ui, value); }
		}

		public event ExtensionHandler ActivatedExtensionChanged;

		public void OnActivate()
		{
			Ui = Extension.Value.Ui;
			ActivatedExtensionChanged?.Invoke(
				this, new ActivatedExtensionEventArgs {ExtensionChange = ExtensionChange.Added});
		}

		public void OnClose()
		{
			ActivatedExtensionChanged?.Invoke(
				this, new ActivatedExtensionEventArgs {ExtensionChange = ExtensionChange.Removed});
		}
	}
}