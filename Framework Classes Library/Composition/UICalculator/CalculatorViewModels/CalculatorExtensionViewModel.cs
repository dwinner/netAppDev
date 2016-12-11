using System;
using System.Collections.ObjectModel;
using System.Linq;
using CalculatorUtils;

namespace CalculatorViewModels
{
	public class CalculatorExtensionViewModel : BindableBase
	{
		private readonly CalculatorExtensionManager _calculatorExtensionManager;
		private ExtensionViewModel _selectedExtension;
		private string _status;

		public CalculatorExtensionViewModel()
		{
			_calculatorExtensionManager = new CalculatorExtensionManager();
			_calculatorExtensionManager.ImportsSatisfied +=
				(sender, args) => Status += $"{args.StatusMessage}{Environment.NewLine}";
		}

		public ObservableCollection<ExtensionViewModel> Extensions { get; }
			= new ObservableCollection<ExtensionViewModel>();

		public ObservableCollection<ExtensionViewModel> ActivatedExtensions { get; }
			= new ObservableCollection<ExtensionViewModel>();

		public string Status
		{
			get { return _status; }
			set { SetProperty(ref _status, value); }
		}

		public ExtensionViewModel SelectedExtension
		{
			get { return _selectedExtension; }
			set { SetProperty(ref _selectedExtension, value); }
		}

		public void Init(params Type[] parts)
		{
			_calculatorExtensionManager.InitializeContainer(parts);
			foreach (var extension in _calculatorExtensionManager.GetExtensionInformation())
			{
				var extensionVm = new ExtensionViewModel(extension);
				extensionVm.ActivatedExtensionChanged += OnActivatedExtensionChanged;
				Extensions.Add(extensionVm);
			}
		}

		private void OnActivatedExtensionChanged(ExtensionViewModel extension, ActivatedExtensionEventArgs e)
		{
			switch (e.ExtensionChange)
			{
				case ExtensionChange.Added:
					ActivatedExtensions.Add(extension);
					SelectedExtension = extension;
					break;

				case ExtensionChange.Removed:
					ActivatedExtensions.Remove(extension);
					SelectedExtension = ActivatedExtensions.FirstOrDefault();
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(e.ExtensionChange));
			}
		}

		private void OnActivateExtension(ExtensionViewModel sender, EventArgs e)
		{
			ActivatedExtensions.Add(sender);
			SelectedExtension = sender;
		}
	}
}