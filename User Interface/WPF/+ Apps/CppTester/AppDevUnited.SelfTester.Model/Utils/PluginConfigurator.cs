using System.IO;
using System.Xml;

namespace AppDevUnited.SelfTester.Model.Utils
{
   /// <summary>
   ///    Конфигуратор работы plugin'ов
   /// </summary>
   public static class PluginConfigurator
   {
      private static readonly ApplicationConfigReader ApplicationConfigReader = ApplicationConfigReader.Instance;

      /// <summary>
      ///    Настройка PVS-Studio под конфигурацию тестера
      /// </summary>
      public static void ConfigurePvsStudio()
      {
         if (!InstallationInfo.IsInstalledPvsStudio)
            return;

         var currentPvsLicenseFile = ApplicationConfigReader.CurrentPvsLicenseFile;
         var backupPvsLicenseFile = ApplicationConfigReader.BackupPvsLicenseFile;
         var etalonPvsSettingsFile = ApplicationConfigReader.EtalonPvsSettingsFile;
         var etalonPvsLicenseFile = ApplicationConfigReader.EtalonPvsStudioLicenseFile;
         var currentPvsSettingsFile = ApplicationConfigReader.CurrentPvsSettingsFile;
         var backupPvsSettings = ApplicationConfigReader.BackupPvsSettings;

         var pvsXmlDocument = new XmlDocument();
         pvsXmlDocument.Load(etalonPvsSettingsFile);
         var sourceTreeRootNode = pvsXmlDocument.CreateNode(
            XmlNodeType.Element, "SourceTreeRoot", pvsXmlDocument.NamespaceURI);
         sourceTreeRootNode.InnerText = ApplicationConfigReader.SelfTesterRootPath + Path.DirectorySeparatorChar;
         if (pvsXmlDocument.DocumentElement != null)
            pvsXmlDocument.DocumentElement.AppendChild(sourceTreeRootNode);

         var etalonPvsSettingsFileStr = string.Format("{0}.STR", etalonPvsSettingsFile);
         pvsXmlDocument.Save(etalonPvsSettingsFileStr);

         File.Copy(File.Exists(currentPvsSettingsFile) ? currentPvsSettingsFile : etalonPvsSettingsFileStr,
            backupPvsSettings, true);

         File.Copy(etalonPvsSettingsFileStr, currentPvsSettingsFile, true);
         File.Delete(etalonPvsSettingsFileStr);

         File.Copy(File.Exists(currentPvsLicenseFile) ? currentPvsLicenseFile : etalonPvsLicenseFile,
            backupPvsLicenseFile, true);

         File.Copy(etalonPvsLicenseFile, currentPvsLicenseFile, true);
      }

      /// <summary>
      ///    Восстановление настроек PVS-Studio
      /// </summary>
      public static void RestorePvsStudio()
      {
         if (InstallationInfo.IsInstalledPvsStudio)
            File.Copy(ApplicationConfigReader.BackupPvsSettings, ApplicationConfigReader.CurrentPvsSettingsFile, true);
      }
   }
}