using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static System.Environment;

namespace MultiStudio.VsCodeInstallWizard.Utils
{
    internal sealed class VsCodeExtensionInstaller
    {
        private const string PathKey = "Path";
        private const string InstallShellCommand = "code --install-extension";
        private const string UninstallShellCommand = "code --uninstall-extension";
        private const string SshCaneasyVsix = "schleissheimer.caneasy.vsix";
        private static readonly (string extId, bool install)[] Recommendations;
        private readonly string _vsCodePath;

        static VsCodeExtensionInstaller()
        {
            var executableDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var ceVsixPath = Path.Combine(executableDir ?? ".", SshCaneasyVsix);
            Recommendations = new[]
            {
                (ceVsixPath, true),
                ("kmasif.capl-vector", false)
            };
        }

        public VsCodeExtensionInstaller(string vsCodePath) => _vsCodePath = vsCodePath;

        public async Task InstallAsync()
        {
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
                var vsShellCommand = $"{(install ? InstallShellCommand : UninstallShellCommand)} {extId}";
                await ExecuteShellAsync(vsShellCommand);
            }
        }

        private static async Task ExecuteShellAsync(string shellCommand)
        {
            try
            {
                var shellStartInfo = new ProcessStartInfo("cmd", $"/c {shellCommand}")
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
#if DEBUG
                Debug.WriteLine(!string.IsNullOrWhiteSpace(stdOut) ? $"{stdOut}{NewLine}" : string.Empty);
                Debug.WriteLine(!string.IsNullOrWhiteSpace(stdErr) ? $"{stdErr}{NewLine}" : string.Empty);
#endif
            }
            catch
            {
                // ignored
            }
        }
    }
}