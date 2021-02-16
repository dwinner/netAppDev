using System;
using System.Globalization;
using System.Windows.Forms;

namespace BatchEvents
{
   public partial class BatchEventsForm : Form
   {
      private BatchCollection<int> _items = new BatchCollection<int>();

      public BatchEventsForm()
      {
         InitializeComponent();
      }

      private void buttonOneAtATime_Click(object sender, EventArgs e)
      {
         _items = new BatchCollection<int>();
         _items.ItemsAdded += _items_ItemsAdded;

         GenerateItems();
      }

      private void buttonUpdateBatch_Click(object sender, EventArgs e)
      {
         _items = new BatchCollection<int>();
         _items.ItemsAdded += _items_ItemsAdded;

         _items.BeginUpdate();
         GenerateItems();
         _items.EndUpdate();
      }

      private void GenerateItems()
      {
         listViewOutput.Items.Clear();

         var start = DateTime.Now;
         for (var i = 0; i < 20000; i++)
         {
            _items.Add(i);
         }
         var end = DateTime.Now;
         var diff = end - start;
         labelElapsed.Text = diff.ToString();
      }

      private void _items_ItemsAdded(object sender, ItemAddedEventArgs<int> e)
      {
         listViewOutput.BeginUpdate();
         foreach (var item in e.Items)
         {
            listViewOutput.Items.Add(item.ToString(CultureInfo.InvariantCulture));
         }
         listViewOutput.EndUpdate();
      }
   }
}