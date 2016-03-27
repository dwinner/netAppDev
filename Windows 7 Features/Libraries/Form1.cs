using Microsoft.WindowsAPICodePack.ApplicationServices;
using System;
using System.Windows.Forms;

namespace Libraries
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         this.lblPowerSource.Text = PowerManager.PowerSource.ToString();
         if (PowerManager.IsBatteryPresent)
         {
            this.prgBatteryLength.Value = PowerManager.BatteryLifePercent;
         }
         PowerManager.BatteryLifePercentChanged += new EventHandler(PowerManager_BatteryLifePercentChanged);

      }

      void PowerManager_BatteryLifePercentChanged(object sender, EventArgs e)
      {
         this.prgBatteryLength.Value = PowerManager.BatteryLifePercent;
      }
   }
}
