using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Environment;

namespace VsCodeInstallExtensions
{
    internal sealed class VsCodeExtensionInstaller
    {
        private const string InstallShellCommand = "--install-extension";
        private const string UninstallShellCommand = "--uninstall-extension";
        private const string SshCaneasyVsix = "caneasy-1.0.0.vsix";
        private const string CaplVectorVsix = "kmasif.capl-vector";
        private const string VsCodeBin = "bin";
        private const string CodeCmd = "code.cmd";
        private readonly string _vscCmd;

        public VsCodeExtensionInstaller(string vsCodePath) =>
            _vscCmd = Path.Combine(Path.GetDirectoryName(vsCodePath) ?? ".", VsCodeBin, CodeCmd);

        public void Install()
        {
            const string dotVsCode = ".vscode";
            const string extensionFolder = "extensions";
            const string canEasyToFind = "caneasy";
            const string caplToFind = "capl-vector";

            var folderPath = GetFolderPath(SpecialFolder.UserProfile);
            var extFolder = Path.Combine(folderPath, dotVsCode, extensionFolder);
            if (!Directory.Exists(extFolder))
            {
                HandleError("Error", $"Directory '{extFolder}' isn't found");
                return;
            }

            var extensionList = Directory.EnumerateDirectories(extFolder, "*", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName).ToList();
            var caplFound = !string.IsNullOrEmpty(extensionList.Find(ext => ext.Contains(caplToFind)));
            var canEasyFound = !string.IsNullOrEmpty(extensionList.Find(ext => ext.Contains(canEasyToFind)));

            // Actualize recommended extensions
            if (caplFound)
            {
                Run(UninstallShellCommand, CaplVectorVsix);
            }

            if (!canEasyFound)
            {
                Run(InstallShellCommand, SshCaneasyVsix);
            }
        }

        private void Run(string vscCommand, string extensionId)
        {
            const string shellCmd = "cmd.exe";
            var args = $"/c \"{_vscCmd}\" {vscCommand} {extensionId}";

            try
            {
                var shellStartInfo = new ProcessStartInfo(shellCmd, args)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                var shellProc = new Process { StartInfo = shellStartInfo };
                shellProc.Start();
#if DEBUG
                var stdOut = shellProc.StandardOutput.ReadToEnd();
                var stdErr = shellProc.StandardError.ReadToEnd();
                Debug.WriteLine(!string.IsNullOrWhiteSpace(stdOut) ? $"{stdOut}{NewLine}" : string.Empty);
                Debug.WriteLine(!string.IsNullOrWhiteSpace(stdErr) ? $"{stdErr}{NewLine}" : string.Empty);
#endif
                shellProc.WaitForExit();
            }
            catch (Exception ex)
            {
                var content = $"Can't launch '{args}'";
                var title = ex.Message;
                HandleError(title, content);
            }
        }

        private static void HandleError(string errorTitle, string errorContent)
        {
#if DEBUG
            MessageBox.Show(errorContent, errorTitle);
#endif
        }
    }
}