using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SelfPlacingWindow
{
   public partial class Form1 : Form
   {
      private readonly ColorDialog _chooseColorDialog = new ColorDialog();

      public Form1()
      {
         InitializeComponent();

         buttonChooseColor.Click += OnClickChooseColor;

         try
         {
            listBoxMessages.Items.Add(ReadSettings() == false
               ? "No information in registry"
               : "Information read in from registry");
            StartPosition = FormStartPosition.Manual;
         }
         catch (Exception e)
         {
            listBoxMessages.Items.Add("A problem occurred reading in data from registry:");
            listBoxMessages.Items.Add(e.Message);
         }
      }

      private void OnClickChooseColor(object sender, EventArgs e)
      {
         if (_chooseColorDialog.ShowDialog() == DialogResult.OK)
         {
            BackColor = _chooseColorDialog.Color;
         }
      }

      private void SaveSettings()
      {
         RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("Software", true);
         if (softwareKey == null)
         {
            return;
         }

         RegistryKey wroxKey = softwareKey.CreateSubKey("WroxPress");
         if (wroxKey == null)
         {
            return;
         }

         RegistryKey selfPlacingWindowKey = wroxKey.CreateSubKey("SelfPlacingWindow");
         if (selfPlacingWindowKey == null)
         {
            return;
         }

         selfPlacingWindowKey.SetValue("BackColor", BackColor.ToKnownColor());
         selfPlacingWindowKey.SetValue("Red", (int)BackColor.R);
         selfPlacingWindowKey.SetValue("Green", (int)BackColor.G);
         selfPlacingWindowKey.SetValue("Blue", (int)BackColor.B);
         selfPlacingWindowKey.SetValue("Width", Width);
         selfPlacingWindowKey.SetValue("Height", Height);
         selfPlacingWindowKey.SetValue("X", DesktopLocation.X);
         selfPlacingWindowKey.SetValue("Y", DesktopLocation.Y);
         selfPlacingWindowKey.SetValue("WindowState", WindowState.ToString());
      }

      private bool ReadSettings()
      {
         RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("Software");
         if (softwareKey == null)
         {
            return false;
         }

         RegistryKey wroxKey = softwareKey.OpenSubKey("WroxPress");
         if (wroxKey == null)
         {
            return false;
         }

         RegistryKey selfPlacingWindowKey = wroxKey.OpenSubKey("SelfPlacingWindow");
         if (selfPlacingWindowKey == null)
         {
            return false;
         }

         listBoxMessages.Items.Add(string.Format("Successfully opened key {0}", selfPlacingWindowKey));

         var redComponent = (int)selfPlacingWindowKey.GetValue("Red");
         var greenComponent = (int)selfPlacingWindowKey.GetValue("Green");
         var blueComponent = (int)selfPlacingWindowKey.GetValue("Blue");
         BackColor = Color.FromArgb(redComponent, greenComponent, blueComponent);
         listBoxMessages.Items.Add(string.Format("Background color: {0}", BackColor.Name));
         var x = (int)selfPlacingWindowKey.GetValue("X");
         var y = (int)selfPlacingWindowKey.GetValue("Y");
         DesktopLocation = new Point(x, y);
         listBoxMessages.Items.Add(string.Format("Desktop location: {0}", DesktopLocation));
         Height = (int)selfPlacingWindowKey.GetValue("Height");
         Width = (int)selfPlacingWindowKey.GetValue("Width");
         listBoxMessages.Items.Add(string.Format("Size: {0}", new Size(Width, Height)));
         var initialWindowState = (string)selfPlacingWindowKey.GetValue("WindowState");
         listBoxMessages.Items.Add(string.Format("Window State: {0}", initialWindowState));
         WindowState = (FormWindowState)Enum.Parse(WindowState.GetType(), initialWindowState);

         return true;
      }
   }
}