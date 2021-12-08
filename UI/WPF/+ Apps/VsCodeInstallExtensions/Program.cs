namespace VsCodeInstallExtensions
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args?.Length != 1)
            {
                return 1;
            }

            var vsCodePath = args[0];
            var installer = new VsCodeExtensionInstaller(vsCodePath);
            installer.Install();

            return 0;
        }
    }
}