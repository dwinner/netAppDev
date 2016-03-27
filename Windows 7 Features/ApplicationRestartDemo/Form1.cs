using Microsoft.WindowsAPICodePack.ApplicationServices;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ApplicationRestartDemo
{
   public partial class Form1 : Form
   {
      private static string recoveryFile = string.Empty;
      private static readonly Data dataContent = new Data();

      public Form1()
      {
         InitializeComponent();

         recoveryFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "RecoveryData.xml");
         ApplicationRestartRecoveryManager.RegisterForApplicationRestart(
             new RestartSettings("/restart", RestartRestrictions.NotOnReboot | RestartRestrictions.NotOnPatch));

         var data = new RecoveryData(RecoveryProcedure, null);
         var settings = new RecoverySettings(data, 0);

         ApplicationRestartRecoveryManager.RegisterForApplicationRecovery(settings);


         if (Environment.GetCommandLineArgs().Length > 1 && Environment.GetCommandLineArgs()[1] == "/restart")
         {
            RecoverLastSession(Environment.GetCommandLineArgs()[1]);
         }
      }

      private void RecoverLastSession(string command)
      {
         if (!File.Exists(recoveryFile))
         {
            MessageBox.Show(this, string.Format("Recovery file {0} does not exist", recoveryFile));
            return;
         }

         var doc = XDocument.Load(recoveryFile);
         var eleName = doc.Descendants("Name").FirstOrDefault();
         var eleAbout = doc.Descendants("about").FirstOrDefault();
         if (eleName != null)
            dataContent.Name = eleName.Value;
         if (eleAbout != null)
            dataContent.About = eleAbout.Value;
      }

      private int RecoveryProcedure(object state)
      {
         PingSystem();
         var doc = new XDocument(
             new XDeclaration("1.0", "UTF-8", "yes"),
             new XElement("root",
                 new XElement("Name", dataContent.Name),
                 new XElement("about", dataContent.About)));
         doc.Save(recoveryFile, SaveOptions.DisableFormatting);
         ApplicationRestartRecoveryManager.ApplicationRecoveryFinished(true);
         return 0;
      }

      private void PingSystem()
      {
         // Find out if the user canceled recovery.
         var isCanceled = ApplicationRestartRecoveryManager.ApplicationRecoveryInProgress();

         if (isCanceled)
         {
            Console.WriteLine("Recovery has been canceled by user.");
            Environment.Exit(2);
         }
      }

      private void btnError_Click(object sender, EventArgs e)
      {
         Environment.FailFast("System Generated Error");
      }

      private void txtName_TextChanged(object sender, EventArgs e)
      {
         dataContent.Name = txtName.Text;
      }

      private void rchAboutYou_TextChanged(object sender, EventArgs e)
      {
         dataContent.About = rchAboutYou.Rtf;
      }
   }

   internal class Data
   {
      public string Name { get; set; }
      public string About { get; set; }
   }
}