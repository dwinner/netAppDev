using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace MultitabledDataSetApp
{
   public partial class MainForm : Form
   {
      private readonly DataSet _autoLotDataSet = new DataSet();   // Формирование широкого DataSet

      // Использование построителей команд для упрощения настройки адаптера данных.
      private SqlCommandBuilder _inventoryCommandBuilder;
      private SqlCommandBuilder _customersCommandBuilder;
      private SqlCommandBuilder _ordersCommandBuilder;

      // Адаптеры данных (для каждой таблицы)
      private readonly SqlDataAdapter _inventoryDataAdapter;
      private readonly SqlDataAdapter _customersDataAdapter;
      private readonly SqlDataAdapter _ordersDataAdapter;

      private readonly string _connectionString;   // Формирование строки подключения.

      public MainForm()
      {         
         InitializeComponent();

         _connectionString = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString; // Получение строки подключения из файла *.config

         // Создание адаптеров.
         _inventoryDataAdapter = new SqlDataAdapter("select * from Inventory", _connectionString);
         _customersDataAdapter = new SqlDataAdapter("select * from Customers", _connectionString);
         _ordersDataAdapter = new SqlDataAdapter("select * from Orders", _connectionString);

         // Генерация команд.
         _inventoryCommandBuilder = new SqlCommandBuilder(_inventoryDataAdapter);
         _customersCommandBuilder = new SqlCommandBuilder(_customersDataAdapter);
         _customersCommandBuilder = new SqlCommandBuilder(_customersDataAdapter);

         // Добавление таблиц в DataSet
         _inventoryDataAdapter.Fill(_autoLotDataSet, "Inventory");
         _customersDataAdapter.Fill(_autoLotDataSet, "Customers");
         _ordersDataAdapter.Fill(_autoLotDataSet, "Orders");

         BuildTableRelationship();  // Создание отношений между таблицами

         // Привязка к графическим элементам
         inventoryDataGridView.DataSource = _autoLotDataSet.Tables["Inventory"];
         customersDataGridView.DataSource = _autoLotDataSet.Tables["Customers"];
         ordersDataGridView.DataSource = _autoLotDataSet.Tables["Orders"];
      }

      private void BuildTableRelationship()  // Создание объектов отношений.
      {
         // Создание объекта отношения между данными CustomerOrder
         DataRelation dataRelation = new DataRelation("CustomerOrder",
            _autoLotDataSet.Tables["Customers"].Columns["CustId"],   /* Родительский */
            _autoLotDataSet.Tables["Orders"].Columns["CustId"] /* Дочерний */ );
         _autoLotDataSet.Relations.Add(dataRelation);

         // Создание объекта отношения между данными InventoryOrder
         dataRelation = new DataRelation("InventoryOrder",
            _autoLotDataSet.Tables["Inventory"].Columns["CarId"],
            _autoLotDataSet.Tables["Orders"].Columns["CarId"]);
         _autoLotDataSet.Relations.Add(dataRelation);
      }

      private void updateDatabaseButton_Click(object sender, EventArgs e)  // Отправление изменений в базу данных
      {
         try
         {
            _inventoryDataAdapter.Update(_autoLotDataSet, "Inventory");
            _customersDataAdapter.Update(_autoLotDataSet, "Customers");
            _ordersDataAdapter.Update(_autoLotDataSet, "Orders");
         }
         catch (Exception exception)
         {            
            MessageBox.Show(exception.Message);
         }
      }

      private void retrieveOrderDetailsButton_Click(object sender, EventArgs e)  // Информация о заказе клиента
      {
         string orderInfo = string.Empty;
         DataRow[] customerDataRows = null;
         DataRow[] orderDataRows = null;
         int custId = int.Parse(custIdTextBox.Text);  // Получение идентификатора клиента из текстового поля.
         customerDataRows = _autoLotDataSet.Tables["Customers"].Select(string.Format("CustId = {0}", custId)); // Поиск строки в таблице Customers
         orderInfo += string.Format("Customer {0}: {1} {2}\n",
                                    customerDataRows[0]["CustId"],
                                    customerDataRows[0]["FirstName"],
                                    customerDataRows[0]["LastName"]);
         orderDataRows = customerDataRows[0].GetChildRows(_autoLotDataSet.Relations["CustomerOrder"]);   // Переход из таблицы Customers к таблице Orders
         orderInfo = orderDataRows.Aggregate(orderInfo, (current, orderDataRow) => current + string.Format("Order Number: {0}\n", orderDataRow["OrderId"]));   // Получение номера заказа
         DataRow[] inventoryDataRows = orderDataRows[0].GetParentRows(_autoLotDataSet.Relations["InventoryOrder"]);  // Переход из таблицы Orders к таблице Inventory
         foreach (DataRow inventoryDataRow in inventoryDataRows)  // Перебор всех заказов данного клиента
         {
            orderInfo += string.Format("----\nOrder Number: {0}\n", inventoryDataRow["OrderId"]);
            DataRow[] lInventoryDataRows = inventoryDataRow.GetParentRows(_autoLotDataSet.Relations["InventoryOrder"]); // Выборка автомобилей, упоминаемых в данном заказе.
            DataRow car = inventoryDataRows[0]; // Получение информации для этого автомобиля
            orderInfo += string.Format("Make: {0}\n", car["Make"]); // Марка
            orderInfo += string.Format("Color: {0}\n", car["Color"]);   // Цвет
            orderInfo += string.Format("Pet Name: {0}\n", car["PetName"]); // Дружественное им
         }
         MessageBox.Show(orderInfo, "Order details");
      }
   }
}
