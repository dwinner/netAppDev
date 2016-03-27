using System;
using System.Net;
using System.Windows.Forms;

namespace _02_EventBasedAsync
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
      }

      #region Go button event handling

      private void goButton_Click(object sender, EventArgs e)
      {
         if (string.IsNullOrWhiteSpace(uriTextBox.Text))
            return;
         Uri acceptedUri = new Uri(uriTextBox.Text);  // TODO: Обработка ошибок ввода URI
         DumpWebPage(acceptedUri);
      }

      private void DumpWebPage(Uri uri)
      {
         using (WebClient webClient = new WebClient())
         {
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted; // NOTE: EAP
            webClient.DownloadStringAsync(uri);
         }
      }

      private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
      {
         downloadedTextBox.Text = e.Result;
      }

      #endregion

   }
}
