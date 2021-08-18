using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StripHtml
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
      }

      private void goButton_Click(object sender, EventArgs e)
      {
         strippedTextBox.Text = StripHtml(htmlTextBox.Text);
      }

      private static string StripHtml(string source)
      {
         string[] patterns =
         {
            @"<(.|\n)*?>",          // HTML-тэги общего назначения
            @"<script.*?</script>"  // Теги сценариев
         };

         return patterns.Aggregate(source, (current, pattern) => Regex.Replace(current, pattern, string.Empty));
      }
   }
}
