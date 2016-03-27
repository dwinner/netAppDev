using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

namespace TaskDialogDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void SimpleTaskDialogButton_OnClick(object sender, RoutedEventArgs e)
      {
         TaskDialog.Show("Simple task dialog", "Additional information", "Title");
      }

      private void AdvancedTaskDialogButton_OnClick(object sender, RoutedEventArgs e)
      {
         var advancedTaskDialog = new TaskDialog
         {
            Caption = "Title",
            Text = "Some information",
            StandardButtons = TaskDialogStandardButtons.Ok | TaskDialogStandardButtons.Cancel,
            Icon = TaskDialogStandardIcon.Information
         };

         advancedTaskDialog.Show();
      }

      private void MoreTaskDialogButton_OnClick(object sender, RoutedEventArgs e)
      {
         var moreDialog = new TaskDialog
         {
            Caption = "Title",
            Text = "Some information",
            StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No,
            Icon = TaskDialogStandardIcon.Shield,
            DetailsExpandedText = "Additional text",
            DetailsExpandedLabel = "Less information",
            DetailsCollapsedLabel = "More information",
            ExpansionMode = TaskDialogExpandedDetailsLocation.ExpandContent,
            FooterText = "Footer information",
            FooterIcon = TaskDialogStandardIcon.Information
         };

         moreDialog.Show();
      }

      private void OnComplexTaskDialog(object sender, RoutedEventArgs e)
      {
         var firstRadioButton = new TaskDialogRadioButton("Radio1", "One");
         var secondRadioButton = new TaskDialogRadioButton("Radio1", "Two");
         var commandLink = new TaskDialogCommandLink("Lonk1", "Information", "Sample command link");
         var progressBar = new TaskDialogProgressBar("progress") { State = TaskDialogProgressBarState.Marquee };

         var taskDialog = new TaskDialog
         {
            Caption = "Title",
            InstructionText = "Sample task dialog",
            StandardButtons = TaskDialogStandardButtons.Ok
         };
         taskDialog.Controls.Add(firstRadioButton);
         taskDialog.Controls.Add(secondRadioButton);
         taskDialog.Controls.Add(commandLink);
         taskDialog.Controls.Add(progressBar);

         taskDialog.Show();
      }
   }
}