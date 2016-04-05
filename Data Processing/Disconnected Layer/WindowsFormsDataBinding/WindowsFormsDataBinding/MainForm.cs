using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WindowsFormsDataBinding.Properties;

namespace WindowsFormsDataBinding
{
   public partial class MainForm : Form
   {
      private readonly List<Car> _listCars; // Коллекция объектов Car
      private readonly DataTable _inventoryDataTable = new DataTable();   // Информация о таблице
      private DataView _coltsDataView;

      public MainForm()
      {
         InitializeComponent();

         // Добавление в список нескольких автомобилей.
         _listCars = new List<Car> 
            {
                new Car { Id = 100, PetName = "Chucky", Make = "BMW", Color = "Green" },
                new Car { Id = 101, PetName = "Tiny", Make = "Yugo", Color = "White" },
                new Car { Id = 102, PetName = "Ami", Make = "Jeep", Color = "Tan" },
                new Car { Id = 103, PetName = "Pain Inducer", Make = "Caravan", Color = "Pink" },
                new Car { Id = 104, PetName = "Fred", Make = "BMW", Color = "Green" },
                new Car { Id = 105, PetName = "Sidd", Make = "BMW", Color = "Black" },
                new Car { Id = 106, PetName = "Mel", Make = "Firebird", Color = "Red" },
                new Car { Id = 107, PetName = "Sarah", Make = "Colt", Color = "Black" },
            };
         CreateDataTable();
         CreateDataView();
      }

      private void CreateDataView()
      {
         _coltsDataView = new DataView(_inventoryDataTable);   // Указание таблицы для создания данного представления
         _coltsDataView.RowFilter = "Make='Yugo'"; // Настройка представлений с помощью фильтра
         coltsOnlyDataGridView.DataSource = _coltsDataView;
      }

      private void CreateDataTable()
      {
         DataColumn carIdColumn = new DataColumn("Id", typeof(int));
         DataColumn carMakeColumn = new DataColumn("Make", typeof(string));
         DataColumn carColorColumn = new DataColumn("Color", typeof(string));
         DataColumn carPetNameColumn = new DataColumn("PetName", typeof(string));
         carPetNameColumn.Caption = "Pet Name";
         _inventoryDataTable.Columns.AddRange(new[]
            {
               carIdColumn,
               carMakeColumn,
               carColorColumn,
               carPetNameColumn
            });

         // Последовательное создание строк из элементов списка.
         foreach (Car car in _listCars)
         {
            DataRow dataRow = _inventoryDataTable.NewRow();
            dataRow["Id"] = car.Id;
            dataRow["Make"] = car.Make;
            dataRow["Color"] = car.Color;
            dataRow["PetName"] = car.PetName;
            _inventoryDataTable.Rows.Add(dataRow);
         }

         // Привязка DataTable к carInventoryGridView
         carInventoryGridView.DataSource = _inventoryDataTable;         
      }

      #region Обработчики

      // Удаление данной строки из DataRowCollection     
      private void deleteButton_Click(object sender, EventArgs e)
      {
         try
         {
            // Поиск строки, которую нужно удалить
            DataRow[] rowToDelete = _inventoryDataTable.Select(string.Format("Id={0}", int.Parse(deleteIdTextBox.Text)));
            if (rowToDelete.Length > 0)   // Удаление
            {
               rowToDelete[0].Delete();
               _inventoryDataTable.AcceptChanges();
            }
         }
         catch (Exception exception)
         {
            MessageBox.Show(exception.Message);
         }
      }

      // Обработчик фильтрации
      private void viewButton_Click(object sender, EventArgs e)
      {
         // Создание фильтра на основе введенных пользователем данных
         string filterStr = string.Format("Make='{0}'", makeTextBox.Text);

         // Поиск всех строк, удовлетворяющих фильтру.
         DataRow[] makes = _inventoryDataTable.Select(filterStr);
         if (makes.Length == 0)
         {
            MessageBox.Show(Resources.MainForm_viewButton_Click_Sorry__no_cars___,
               Resources.MainForm_viewButton_Click_Selection_error_);   // ничего не найдено
         }
         else
         {
            var strMake = new StringBuilder();
            foreach (DataRow dataRow in makes)
            {
               // Получение значения PetName из текущей строки
               strMake.Append(dataRow["PetName"]).Append("\n");
            }
            // Вывод названий всех найденных автомобилей указанной марки.
            MessageBox.Show(strMake.ToString(), string.Format("We have {0}s named:", makeTextBox.Text));
         }
      }

      // Поиск с помощью фильтра всех строк, которые нужно изменить.
      private void changeMakeButton_Click(object sender, EventArgs e)
      {
         if (DialogResult.Yes == // Проверка выбора пользователя
            MessageBox.Show(Resources.MainForm_changeMakeButton_Click_Are_you_sure_,
               Resources.MainForm_changeMakeButton_Click_Please_confirm,
               MessageBoxButtons.YesNo))
         {
            string filterStr = "Make='BMW'"; // Создание фильтра.                        
            DataRow[] makes = _inventoryDataTable.Select(filterStr); // Поиск всех строк, удовлетворяющих фильтру.                        
            foreach (DataRow dataRow in makes)  // Замена всех бумеров на Yugo
            {
               dataRow["Make"] = "Yugo";
            }
         }
      }

      #endregion                      
   }
}
