using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClipboardDemo.Properties;

namespace ClipboardDemo
{
   public partial class Form1 : Form
   {
      private readonly string[][] _people =
      {
         new[] {"Bob", "M", "45"},
         new[] {"Mary", "F", "24"},
         new[] {"Bill", "M", "25"},
         new[] {"Mark", "M", "37"},
         new[] {"Ashley", "F", "39"},
         new[] {"Anna", "F", "56"},
         new[] {"Joe", "M", "65"}
      };

      private readonly Random _rand = new Random();

      public Form1()
      {
         InitializeComponent();

         listView1.FullRowSelect = true;
         listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;

         AddRandomPerson();
         AddRandomPerson();
         AddRandomPerson();

         listView1.Items[0].Selected = true;
      }

      private void listView1_SelectedIndexChanged(object sender, EventArgs e)
      {
         buttonCopy.Enabled = buttonCut.Enabled = listView1.SelectedIndices.Count > 0;
      }

      private void AddRandomPerson()
      {
         var person = _people[_rand.Next(0, _people.Length - 1)];
         AddPerson(person[0], person[1], person[2]);
      }

      private void AddPerson(string name, string sex, string age)
      {
         var item = new ListViewItem(name);
         item.SubItems.Add(sex);
         item.SubItems.Add(age);

         listView1.Items.Add(item);
      }

      private void buttonAddRandomPerson_Click(object sender, EventArgs e)
      {
         AddRandomPerson();
      }

      private void buttonCopy_Click(object sender, EventArgs e)
      {
         CopyAllFormats();
      }

      private void CopyAllFormats()
      {
         var obj = new DataObject();
         obj.SetText(GetText());
         obj.SetImage(GetBitmap());
         obj.SetData(MyClipboardItem.Format.Name, RowsToClipboardItems());
         Clipboard.SetDataObject(obj);

         // var image = Clipboard.GetImage();
         // var text = Clipboard.GetText();
      }

      private string GetText()
      {
         var sb = new StringBuilder(256);
         foreach (ListViewItem item in listView1.SelectedItems)
         {
            sb.AppendFormat("{0},{1},{2}", item.Text, item.SubItems[1].Text, item.SubItems[2].Text);
            sb.AppendLine();
         }
         return sb.ToString();
      }

      private Bitmap GetBitmap()
      {
         var bitmap = new Bitmap(listView1.Width, listView1.Height);
         listView1.DrawToBitmap(bitmap, listView1.ClientRectangle);
         return bitmap;
      }

      private List<MyClipboardItem> RowsToClipboardItems()
      {
         return (from ListViewItem item in listView1.SelectedItems
                 select new MyClipboardItem(item.Text, item.SubItems[1].Text, item.SubItems[2].Text)).ToList();
      }

      private void buttonPasteToList_Click(object sender, EventArgs e)
      {
         if (Clipboard.ContainsData(MyClipboardItem.Format.Name))
         {
            var items = GetItemsFromClipboard();
            foreach (var item in items)
            {
               AddPerson(item.Name, item.Sex, item.Age);
            }
         }
         else
         {
            MessageBox.Show(Resources.Nothing);
         }
      }

      private static IEnumerable<MyClipboardItem> GetItemsFromClipboard()
      {
         var obj = Clipboard.GetData(MyClipboardItem.Format.Name);
         return obj as IList<MyClipboardItem>;
      }

      private void buttonCut_Click(object sender, EventArgs e)
      {
         CopyAllFormats();
         var itemsToDelete = listView1.SelectedItems.Cast<ListViewItem>().ToList();
         foreach (var item in itemsToDelete)
         {
            item.Remove();
         }
      }
   }
}