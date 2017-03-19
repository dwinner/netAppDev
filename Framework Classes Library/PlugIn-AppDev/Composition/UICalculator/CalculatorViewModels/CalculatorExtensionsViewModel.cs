using System;
using System.Collections.ObjectModel;
using System.Linq;
using CalculatorUtils;

namespace CalculatorViewModels
{
	public class CalculatorExtensionsViewModel : BindableBase
	{
		private readonly CalculatorExtensionsManager _calculatorExtensionsManager;
		private ExtensionViewModel _selectedExtension;
		private string _status;

		public CalculatorExtensionsViewModel()
		{
			_calculatorExtensionsManager = new CalculatorExtensionsManager();
			_calculatorExtensionsManager.ImportsSatisfied += (sender, e) => { Status += $"{e.StatusMessage}\n"; };
		}

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

		public ObservableCollection<ExtensionViewModel> Extensions { get; } = new ObservableCollection<ExtensionViewModel>();

		public ObservableCollection<ExtensionViewModel> ActivatedExtensions { get; } =
			new ObservableCollection<ExtensionViewModel>();

		public void Init(params Type[] parts)
		{
			_calculatorExtensionsManager.InitializeContainer(parts);

			foreach (var extension in _calculatorExtensionsManager.GetExtensionInformation())
			{
				var vm = new ExtensionViewModel(extension);
				vm.ActivatedExtensionChanged += OnActivatedExtensionChanged;
				Extensions.Add(vm);
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
			}
		}

		private void OnActivateExtension(ExtensionViewModel sender, EventArgs e)
		{
			ActivatedExtensions.Add(sender);
			SelectedExtension = sender;
		}
	}
}