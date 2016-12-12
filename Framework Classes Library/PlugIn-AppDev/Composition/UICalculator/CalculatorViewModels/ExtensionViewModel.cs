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
			ActivateCommand=new DelegateCommand(OnActivate);
			CloseCommand=new DelegateCommand(OnClose);
			Extension = extension;			
		}

		public object UI
		{
			get { return _ui; }
			set { SetProperty(ref _ui, value); }
		}

		public event ExtensionHandler ActivatedExtensionChanged;
		public DelegateCommand ActivateCommand { get; }
		public DelegateCommand CloseCommand { get; }
		public Lazy<ICalculatorExtension, CalculatorExtensionMetadataAttribute> Extension { get; }

		public void OnActivate()
		{
			UI = Extension.Value.UI;
			OnActivatedExtensionChanged(
				this, new ActivatedExtensionEventArgs {ExtensionChange = ExtensionChange.Removed});
		}

		private void OnActivatedExtensionChanged(ExtensionViewModel extension, ActivatedExtensionEventArgs e)
			=> ActivatedExtensionChanged?.Invoke(extension, e);

		public void OnClose()
			=> OnActivatedExtensionChanged(this, new ActivatedExtensionEventArgs {ExtensionChange = ExtensionChange.Removed});
	}
}