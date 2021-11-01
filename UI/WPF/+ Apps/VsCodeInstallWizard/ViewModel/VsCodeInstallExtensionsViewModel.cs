using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MultiStudio.VsCodeInstallWizard.Model;
using MultiStudio.VsCodeInstallWizard.Mvvm;
using static System.Environment;

namespace MultiStudio.VsCodeInstallWizard.ViewModel
{
    public class VsCodeInstallExtensionsViewModel : ViewModelBase
    {
        private const string PathKey = "Path";
        private const string InstallShellCommand = "code --install-extension";
        private const string UninstallShellCommand = "code --uninstall-extension";

        private static readonly (string extId, bool install)[] Recommendations =
        {
            ("ms-vscode.cpptools", true),
            ("ms-dotnettools.csharp", true),
            ("ms-python.python", true),
            ("schleissheimer.caneasy", true), // wrong path - schleissheimer.caneasy.vsix
            ("kmasif.capl-vector", false) // unistall it for capl
        };

        private readonly string _vsCodePath;
        private ICommand _closeCommand;
        private ICommand _installCommand;
        private bool _installed;
        private string _installLog;
        private bool _isInstalling;
        private string _title;

        public VsCodeInstallExtensionsViewModel(string vsCodePath)
        {
            _vsCodePath = vsCodePath;
            Title = "Install recommended extensions";
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsInstalling
        {
            get => _isInstalling;
            set => SetProperty(ref _isInstalling, value);
        }

        public string InstallLog
        {
            get => _installLog;
            set => SetProperty(ref _installLog, value);
        }

        public bool Installed
        {
            get => _installed;
            set => SetProperty(ref _installed, value);
        }

        public ICommand InstallCommand =>
            _installCommand ?? (_installCommand = new RelayCommand(async _ => await OnInstall()
                .ConfigureAwait(true)));

        public ICommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new RelayCommand(_ => OnClose()));

        private void OnClose()
        {
            var exitCode = IsInstalling
                ? (short)ExitCodeKind.NotFinished
                : Installed
                    ? (short)ExitCodeKind.AutoInstalled
                    : (short)ExitCodeKind.NotFinished;

            Exit(exitCode);
        }

        private async Task OnInstall()
        {
            try
            {
                InstallLog = string.Empty;
                IsInstalling = true;

                // Find VS Code in Path environment variable
                var envVars = GetEnvironmentVariables();
                if (!envVars.Contains(PathKey))
                {
                    return;
                }

                if (!(envVars[PathKey] is string path))
                {
                    return;
                }

                var shellSoftwares = path.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                const string binSubFolder = "bin";
                var vsCodeRootDir = Path.GetDirectoryName(_vsCodePath)
                                    ?? throw new ArgumentException(nameof(_vsCodePath));
                var vsCodeBin = Path.Combine(vsCodeRootDir, binSubFolder);
                var vscShellFound = shellSoftwares.Any(software =>
                    string.Equals(vsCodeBin, software, StringComparison.CurrentCultureIgnoreCase));
                if (!vscShellFound)
                {
                    return;
                }

                // Ok, now we know we are able to run 'bin/code.cmd' from any location.
                // Actualize recommended extensions
                foreach (var (extId, install) in Recommendations)
                {
                    var cmd = $"{(install ? InstallShellCommand : UninstallShellCommand)} {extId}";
                    await ExecuteShellAsync(cmd);
                }

                Installed = true;
                Title = "Done! You can close the window.";
            }
            finally
            {
                IsInstalling = false;
            }
        }

        private async Task ExecuteShellAsync(string cmd)
        {
            try
            {
                var shellStartInfo = new ProcessStartInfo("cmd", $"/c {cmd}")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                var shellProc = new Process { StartInfo = shellStartInfo };
                shellProc.Start();
                var stdOut = await shellProc.StandardOutput.ReadToEndAsync();
                var stdErr = await shellProc.StandardError.ReadToEndAsync();
                if (!string.IsNullOrWhiteSpace(stdOut))
                {
                    InstallLog += $"{stdOut}{NewLine}";
                }

                if (!string.IsNullOrWhiteSpace(stdErr))
                {
                    InstallLog += $"{stdErr}{NewLine}";
                }
            }
            catch (Exception ex)
            {
                InstallLog = $"{ex.Message}{NewLine}";
            }
        }
    }
}