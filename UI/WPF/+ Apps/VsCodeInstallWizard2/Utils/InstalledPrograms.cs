using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Win32;

namespace MultiStudio.VsCodeInstallWizard.Utils
{
    internal static class InstalledPrograms
    {
        private const string UninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        private static readonly RegistryKey LocalMachine = Registry.LocalMachine;
        private static readonly RegistryKey CurrentUser = Registry.CurrentUser;

        private static IEnumerable<dynamic> GetSoftwareByRegKey(RegistryKey topRegKey)
        {
            var softwareList = new List<dynamic>();
            using (var registryKey = topRegKey.OpenSubKey(UninstallKey))
            {
                foreach (var skName in registryKey.GetSubKeyNames())
                {
                    using (var sk = registryKey.OpenSubKey(skName))
                    {
                        var valueNames = sk.GetValueNames();
                        dynamic swEntity = new ExpandoObject();
                        foreach (var keyName in valueNames)
                        {
                            var keyValue = sk.GetValue(keyName);
                            if (keyValue != null)
                            {
                                ((IDictionary<string, object>)swEntity).Add(keyName, keyValue);
                            }
                        }

                        softwareList.Add(swEntity);
                    }
                }
            }

            return softwareList;
        }

        private static IEnumerable<dynamic> GetLocalMachineSw() => GetSoftwareByRegKey(LocalMachine);

        private static IEnumerable<dynamic> GetCurrentUserSw() => GetSoftwareByRegKey(CurrentUser);

        private static IEnumerable<dynamic> GetAllInstalledSw() => GetLocalMachineSw().Union(GetCurrentUserSw());

        internal static dynamic FindSoftwareByName(string swName)
        {
            return
                (from software in GetAllInstalledSw()
                    let displayName = GetDisplayName(software)
                    where displayName != null && displayName.Contains(swName)
                    select software).FirstOrDefault();

            string GetDisplayName(dynamic software)
            {
                string displayName;
                try
                {
                    displayName = software.DisplayName as string;
                }
                catch
                {
                    displayName = null;
                }

                return displayName;
            }
        }
    }
}