using System.ComponentModel;
using MultiStudio.VsCodeInstallWizard.ViewModel;

namespace MultiStudio.VsCodeInstallWizard
{
    public partial class VsCodeInstallExtensionsWindow
    {
        private readonly VsCodeInstallExtensionsViewModel _viewModel;

        public VsCodeInstallExtensionsWindow()
        {
            InitializeComponent();
        }

        public VsCodeInstallExtensionsWindow(string vsCodePath)
            : this() =>
            DataContext = _viewModel = new VsCodeInstallExtensionsViewModel(vsCodePath);

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (_viewModel.IsInstalling)
            {
                e.Cancel = true;
            }
            else
            {
                _viewModel.CloseCommand?.Execute(null);
            }
        }
    }
}