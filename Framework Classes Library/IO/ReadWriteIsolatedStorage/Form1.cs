using System;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SelfPlacingWindow
{
   public partial class Form1 : Form
   {
      private readonly ColorDialog _chooseColorDialog = new ColorDialog();
      private const string SelfplacingwindowXml = "SelfPlacingWindow.xml";

      public Form1()
      {
         InitializeComponent();

         buttonChooseColor.Click += OnClickChooseColor;

         try
         {
            listBoxMessages.Items.Add(ReadSettings() == false
               ? "No information in isolated storage"
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
         var storageFile = IsolatedStorageFile.GetUserStoreForDomain();
         var storageStream = new IsolatedStorageFileStream(SelfplacingwindowXml, FileMode.Create, FileAccess.Write);
         var writer = new XmlTextWriter(storageStream, Encoding.UTF8) { Formatting = Formatting.Indented };
         writer.WriteStartDocument();
         writer.WriteStartElement("Settings");

         {
            writer.WriteStartElement("BackColor");
            writer.WriteValue(BackColor.ToKnownColor().ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Red");
            writer.WriteValue(BackColor.R);
            writer.WriteEndElement();

            writer.WriteStartElement("Green");
            writer.WriteValue(BackColor.G);
            writer.WriteEndElement();

            writer.WriteStartElement("Blue");
            writer.WriteValue(BackColor.B);
            writer.WriteEndElement();

            writer.WriteStartElement("Width");
            writer.WriteValue(Width);
            writer.WriteEndElement();

            writer.WriteStartElement("Height");
            writer.WriteValue(Height);
            writer.WriteEndElement();

            writer.WriteStartElement("X");
            writer.WriteValue(DesktopLocation.X);
            writer.WriteEndElement();

            writer.WriteStartElement("Y");
            writer.WriteValue(DesktopLocation.Y);
            writer.WriteEndElement();

            writer.WriteStartElement("WindowState");
            writer.WriteValue(WindowState.ToString());
            writer.WriteEndElement();
         }

         writer.WriteEndElement();
         writer.Flush();
         writer.Close();
         storageStream.Close();
         storageFile.Close();
      }

      private bool ReadSettings()
      {
         var storageFile = IsolatedStorageFile.GetUserStoreForDomain();
         var userFiles = storageFile.GetFileNames(SelfplacingwindowXml);
         foreach (var userFile in userFiles.Where(file => file == SelfplacingwindowXml))
         {
            listBoxMessages.Items.Add(string.Format("Successfully opened file {0}", userFile));
            var storageStream = new StreamReader(
               new IsolatedStorageFileStream(SelfplacingwindowXml, FileMode.Open, storageFile));
            var reader = new XmlTextReader(storageStream);
            int redComponent = 0;
            int greenComponent = 0;
            int blueComponent = 0;
            int x = 0;
            int y = 0;
            while (reader.Read())
            {
               switch (reader.Name)
               {
                  case "Red":
                     redComponent = int.Parse(reader.ReadString());
                     break;

                  case "Green":
                     greenComponent = int.Parse(reader.ReadString());
                     break;

                  case "Blue":
                     blueComponent = int.Parse(reader.ReadString());
                     break;

                  case "X":
                     x = int.Parse(reader.ReadString());
                     break;

                  case "Y":
                     y = int.Parse(reader.ReadString());
                     break;

                  case "Width":
                     Width = int.Parse(reader.ReadString());
                     break;

                  case "Height":
                     Height = int.Parse(reader.ReadString());
                     break;

                  case "WindowState":
                     WindowState = (FormWindowState)Enum.Parse(WindowState.GetType(), reader.ReadString());
                     break;
               }
            }

            BackColor = Color.FromArgb(redComponent, greenComponent, blueComponent);
            DesktopLocation = new Point(x, y);
            listBoxMessages.Items.Add(string.Format("Background color: {0}", BackColor.Name));
            listBoxMessages.Items.Add(string.Format("Desktop location: {0}", DesktopLocation));
            listBoxMessages.Items.Add(string.Format("Size: {0}", new Size(Width, Height).ToString()));
            listBoxMessages.Items.Add(string.Format("Window State: {0}", WindowState));

            storageStream.Close();
            storageFile.Close();
         }

         return true;
      }
   }
}