using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LinqToXmlWinApp.Model;

namespace LinqToXmlWinApp
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
      }

      private void MainForm_Load(object sender, EventArgs e)
      {
         inventoryTextBox.Text = LinqToXmlObjectModel.GetXmlInventory().ToString();
      }

      private void addButton_Click(object sender, EventArgs e)
      {
         LinqToXmlObjectModel.InsertNewElement(new CarEntity() // Добавить новый элемент к документу
            {
               Make = makeTextBox.Text,
               Color = colorTextBox.Text,
               PetName = petNameTextBox.Text
            });
         inventoryTextBox.Text = LinqToXmlObjectModel.GetXmlInventory().ToString(); // Отобразить текущий
      }

      private void lookUpColorsButton_Click(object sender, EventArgs e)
      {
         List<string> foundColors = LinqToXmlObjectModel.LookUpColorsForMake(makeToLookUpTextBox.Text);
         StringBuilder founded = new StringBuilder();
         foreach (string foundColor in foundColors)
         {
            founded.Append(foundColor).Append(Environment.NewLine);
         }
         MessageBox.Show(founded.ToString());
      }
   }
}
