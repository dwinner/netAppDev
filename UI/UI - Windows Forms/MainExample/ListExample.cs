using System.Windows.Forms;

namespace MainExample
{
   public partial class ListExample : Form
   {
      public ListExample()
      {
         InitializeComponent();
         checkedListBox1.DisplayMember = "Name";
         checkedListBox1.ValueMember = "Id";
         LoadData();
      }

      private void LoadData()
      {
         // Get all vendors and add into the controls on screen
         try
         {
            comboBox1.BeginUpdate();
            listBox1.BeginUpdate();
            checkedListBox1.BeginUpdate();
            listView1.BeginUpdate();

            foreach (var v in Vendor.GetVendors())
            {
               comboBox1.Items.Add(v);
               listBox1.Items.Add(v);
               checkedListBox1.Items.Add(v);
               var lvi = new ListViewItem(new[] {v.Id.ToString(), v.Name}) {Tag = v};
               listView1.Items.Add(lvi);
            }
         }
         finally
         {
            comboBox1.EndUpdate();
            listBox1.EndUpdate();
            checkedListBox1.EndUpdate();
            listView1.EndUpdate();
         }
      }

      private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
      {
         var list = sender as CheckedListBox;

         if (null != list)
         {
         }
      }
   }
}