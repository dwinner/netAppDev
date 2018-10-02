namespace WordEditTimerAddIn
{
   partial class TimerRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      public TimerRibbon()
         : base(Globals.Factory.GetRibbonFactory())
      {
         InitializeComponent();
      }

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
         this.HomeTab = this.Factory.CreateRibbonTab();
         this.PrimaryGroup = this.Factory.CreateRibbonGroup();
         this.TimerToggleButton = this.Factory.CreateRibbonToggleButton();
         this.SecondaryGroup = this.Factory.CreateRibbonGroup();
         this.TimerPassedCheckBox = this.Factory.CreateRibbonCheckBox();
         this.HomeTab.SuspendLayout();
         this.PrimaryGroup.SuspendLayout();
         this.SecondaryGroup.SuspendLayout();
         // 
         // HomeTab
         // 
         this.HomeTab.Groups.Add(this.PrimaryGroup);
         this.HomeTab.Groups.Add(this.SecondaryGroup);
         this.HomeTab.Label = "DOC TIMER";
         this.HomeTab.Name = "HomeTab";
         // 
         // PrimaryGroup
         // 
         this.PrimaryGroup.DialogLauncher = ribbonDialogLauncherImpl1;
         this.PrimaryGroup.Items.Add(this.TimerToggleButton);
         this.PrimaryGroup.Name = "PrimaryGroup";
         this.PrimaryGroup.DialogLauncherClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.PrimaryGroup_DialogLauncherClick);
         // 
         // TimerToggleButton
         // 
         this.TimerToggleButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
         this.TimerToggleButton.Label = "Toggle Timer Display";
         this.TimerToggleButton.Name = "TimerToggleButton";
         this.TimerToggleButton.OfficeImageId = "StartAfterPrevious";
         this.TimerToggleButton.ShowImage = true;
         this.TimerToggleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.TimerToggleButton_Click);
         // 
         // SecondaryGroup
         // 
         this.SecondaryGroup.Items.Add(this.TimerPassedCheckBox);
         this.SecondaryGroup.Name = "SecondaryGroup";
         // 
         // TimerPassedCheckBox
         // 
         this.TimerPassedCheckBox.Label = "Timer passed";
         this.TimerPassedCheckBox.Name = "TimerPassedCheckBox";
         this.TimerPassedCheckBox.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.TimerPassedCheckBox_Click);
         // 
         // TimerRibbon
         // 
         this.Name = "TimerRibbon";
         this.RibbonType = "Microsoft.Word.Document";
         this.Tabs.Add(this.HomeTab);
         this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.TimerRibbon_Load);
         this.HomeTab.ResumeLayout(false);
         this.HomeTab.PerformLayout();
         this.PrimaryGroup.ResumeLayout(false);
         this.PrimaryGroup.PerformLayout();
         this.SecondaryGroup.ResumeLayout(false);
         this.SecondaryGroup.PerformLayout();

      }

      #endregion

      internal Microsoft.Office.Tools.Ribbon.RibbonTab HomeTab;
      internal Microsoft.Office.Tools.Ribbon.RibbonGroup PrimaryGroup;
      internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox TimerPassedCheckBox;
      internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton TimerToggleButton;
      internal Microsoft.Office.Tools.Ribbon.RibbonGroup SecondaryGroup;      
   }

   partial class ThisRibbonCollection
   {
      internal TimerRibbon TimerRibbon
      {
         get { return this.GetRibbon<TimerRibbon>(); }
      }
   }
}
