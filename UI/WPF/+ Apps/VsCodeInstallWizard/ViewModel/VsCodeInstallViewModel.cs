using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Input;
using MultiStudio.VsCodeInstallWizard.Model;
using MultiStudio.VsCodeInstallWizard.Mvvm;

namespace MultiStudio.VsCodeInstallWizard.ViewModel
{
    public class VsCodeInstallViewModel : ViewModelBase
    {
        private const string DefaultVsCodeDirectLink = "https://aka.ms/win32-x64-user-stable";
        private const string DefaultVsCodeUserLink = "https://code.visualstudio.com/download";
        private const string DefaultVsCodeFileName = "VSCode.exe";
        private static readonly string AppFolder;
        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private readonly DownloadModel _downloadModel;
        private readonly string _targetFileName;
        private ICommand _cancelCommand;
        private CancellationTokenSource _cancellationTokenSource;
        private ICommand _closeCommand;
        private int _currentProgress;
        private ICommand _downloadCommand;
        private string _downloadInfo;
        private string _downloadPercentage;
        private ICommand _goVsCodeUserLinkCommand;
        private bool _isDownloading;
        private BackgroundWorker _worker;

        static VsCodeInstallViewModel()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData,
                Environment.SpecialFolderOption.Create);
            var appFolder = Path.Combine(appDataFolder, assemblyName);
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            AppFolder = appFolder;
        }

        public VsCodeInstallViewModel()
        {
            _targetFileName = Path.Combine(AppFolder, DefaultVsCodeFileName);
            _downloadModel = new DownloadModel(_targetFileName);
            _downloadModel.PercentageChanged += OnPercentageChanged;
            CurrentProgress = 0;
        }

        public string VsCodeUserLink => DefaultVsCodeUserLink;

        public bool IsDownloading
        {
            get => _isDownloading;
            private set => SetProperty(ref _isDownloading, value);
        }

        public string DownloadInfo
        {
            get => _downloadInfo ?? "OR";
            private set => SetProperty(ref _downloadInfo, value);
        }

        public int CurrentProgress
        {
            get => _currentProgress;
            private set => SetProperty(ref _currentProgress, value);
        }

        public string DownloadPercentage
        {
            get => _downloadPercentage;
            private set => SetProperty(ref _downloadPercentage, value);
        }

        public ICommand DownloadCommand =>
            _downloadCommand ?? (_downloadCommand = new RelayCommand(_ => OnDownloadContent()));

        public ICommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new RelayCommand(_ => OnCancelDownload()));

        public ICommand GoVsCodeUserLinkCommand =>
            _goVsCodeUserLinkCommand ?? (_goVsCodeUserLinkCommand = new RelayCommand(OnGoToUrl));

        public ICommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new RelayCommand(_ => OnClose()));

        private void OnGoToUrl(object urlObj)
        {
            var url = urlObj as string;
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            Process.Start(url);
            const short exitCode = (short)ExitCodeKind.ManuallyDownload;
            if (IsDownloading)
            {
                CleanUp(exitCode);
                return;
            }

            Environment.Exit(exitCode);
        }

        private void OnClose()
        {
            const short closed = (short)ExitCodeKind.Closed;
            if (_worker == null)
            {
                Environment.Exit(closed);
                return;
            }

            if (IsDownloading)
            {
                CleanUp(closed);
                return;
            }

            Environment.Exit(closed);
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e) =>
            CurrentProgress = e.ProgressPercentage;

        private async void OnDownload(object sender, DoWorkEventArgs e)
        {
            if (CurrentProgress >= 100)
            {
                CurrentProgress = 0;
            }

            DownloadInfo = $"Downloading: {DefaultVsCodeDirectLink}";
            try
            {
                IsDownloading = true;
                await _downloadModel.GetHttpFileAsync(DefaultVsCodeDirectLink, _cancellationTokenSource.Token)
                    .ConfigureAwait(true);

                var vsCodeInstaller = _downloadModel.TargetFileName;
                var vsCodeInfo = new ProcessStartInfo
                {
                    FileName = vsCodeInstaller
                };
                var vsCodeInslallerProc = Process.Start(vsCodeInfo);
                if (vsCodeInslallerProc != null)
                {
                    vsCodeInslallerProc.WaitForExit();
                    var exitCode = vsCodeInslallerProc.HasExited && vsCodeInslallerProc.ExitCode == 0
                        ? (short)ExitCodeKind.AutoInstalled
                        : (short)ExitCodeKind.NotFinished;

                    Environment.Exit(exitCode);
                }
                else
                {
                    Environment.Exit((short)ExitCodeKind.NotFinished);
                }
            }
            catch (OperationCanceledException)
            {
                DownloadInfo = $"Cancelled: {DefaultVsCodeDirectLink}";
                CurrentProgress = 0;
                DownloadPercentage = "0% of file complete";
                IsDownloading = false;
                if (File.Exists(_targetFileName))
                {
                    File.Delete(_targetFileName);
                }

                _autoResetEvent.Set();
            }
            catch (IOException ioEx)
            {
                DownloadInfo = $"Error: {ioEx.Message}. Please try again";
                IsDownloading = false;
            }
            finally
            {
                IsDownloading = false;
            }
        }

        private void OnPercentageChanged(object sender, EventArgs<int> e)
        {
            DownloadPercentage = $"{e.Value} % of file complete";
            CurrentProgress = e.Value;
        }

        private BackgroundWorker GetNewWorker()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += OnDownload;
            worker.ProgressChanged += OnProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            _cancellationTokenSource = new CancellationTokenSource();

            return worker;
        }

        private void OnDownloadContent()
        {
            _worker = GetNewWorker();
            _worker.RunWorkerAsync();
        }

        private void OnCancelDownload() => _cancellationTokenSource.Cancel();

        private void CleanUp(short exitCode)
        {
            _worker.CancelAsync();
            OnCancelDownload();
            _autoResetEvent.WaitOne();
            Environment.Exit(exitCode);
        }
    }
}