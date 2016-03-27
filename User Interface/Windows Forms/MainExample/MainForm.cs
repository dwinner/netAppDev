using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MainExample
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
      }

      private void fileExit_Click(object sender, EventArgs e)
      {
         var cancel = new CancelEventArgs(false);
         Application.Exit(cancel);
      }

      private void buttonExample_Click(object sender, EventArgs e) // Дочерняя форма с кнопками
      {
         var buttonForm = new ButtonExample { MdiParent = this };
         buttonForm.Show();
      }

      private void listExample_Click(object sender, EventArgs e) // Форма со списочными элементами управления
      {
         var listForm = new ListExample { MdiParent = this };
         listForm.Show();
      }

      private void validationExample_Click(object sender, EventArgs e)  // Проверка корректности введенных данных
      {
         var validationForm = new ValidationExample { MdiParent = this };
         validationForm.Show();
      }

      private void textBoxExample_Click(object sender, EventArgs e)  // Текстовые поля
      {
         var form = new TexBoxExample { MdiParent = this };
         form.Show();
      }

      private void panelExample_Click(object sender, EventArgs e) // Компоновка
      {
         var form = new PanelExample { MdiParent = this };
         form.Show();
      }

      private void splitterExample_Click(object sender, EventArgs e) // Разделитель панелей
      {
         var form = new SplitterExample { MdiParent = this };
         form.Show();
      }

      private void userControlExample_Click(object sender, EventArgs e) // Специальный элемент управления
      {
         var form = new UserControlExample { MdiParent = this };
         form.Show();
      }

      private void dataSetExample_Click(object sender, EventArgs e) // Форма заполнения DataGridView для СУБД
      {
         var gridViewExample = new DataGridViewExample(grid =>
         {
            const string customers = "SELECT * FROM Customers";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["northwind"].ConnectionString))
            {
               var dataSource = new DataSet();
               using (var sqlDataAdapter = new SqlDataAdapter(customers, con))
               {
                  sqlDataAdapter.Fill(dataSource, "Customers");
               }

               grid.AutoGenerateColumns = true;
               grid.DataSource = dataSource;
               grid.DataMember = "Customers";
            }
         }) { MdiParent = this };

         gridViewExample.Show();
      }

      private void arrayDataSourceExample_Click(object sender, EventArgs e)   // Массив в качестве источника данных
      {
         var form = new DataGridViewExample(grid =>
         {
            Item[] items = { new Item("One"), new Item("Two"), new Item("Three") };
            grid.AutoGenerateColumns = true;
            grid.DataSource = items;
         }) { MdiParent = this };
         form.Show();
      }

      // Форма заполнения DataGridView для DataTable-источника данных
      private void dataTableSourceExample_Click(object sender, EventArgs e)
      {
         var form = new DataGridViewExample(grid =>
         {
            using (
               var northConnection =
                  new SqlConnection(ConfigurationManager.ConnectionStrings["northwind"].ConnectionString))
            {
               const string productSelect = "SELECT * FROM products";
               using (var dataAdapter = new SqlDataAdapter(productSelect, northConnection))
               {
                  var dataSet = new DataSet();
                  dataAdapter.Fill(dataSet, "Products");
                  grid.AutoGenerateColumns = true;
                  grid.DataSource = dataSet.Tables["Products"];
               }
            }
         }) { MdiParent = this };
         form.Show();
      }

      private void dataViewExample_Click(object sender, EventArgs e) // Отображение данных из DataView
      {
         var form = new DataViewExample { MdiParent = this };
         form.Show();
      }

      private void genericListExample_Click(object sender, EventArgs e) // Отображение обобщенных коллекций
      {
         var form = new DataGridViewExample(grid =>
         {
            var people = new List<Person>
            {
               new Person("Fred", Sex.Male, new DateTime(1970, 12, 14)),
               new Person("Bill", Sex.Male, new DateTime(1976, 10, 29)),
               new Person("Jack", Sex.Male, new DateTime(1945, 5, 17)),
               new Person("Jane", Sex.Female, new DateTime(1982, 1, 3))
            };

            grid.AutoGenerateColumns = true;
            grid.DataSource = people;
         }) { MdiParent = this };
         form.Show();
      }
   }
}