using System;
using System.ComponentModel;

namespace StoreDatabase
{
   public class Product : INotifyPropertyChanged
   {
      // For DataGridComboBoxColumn example.

      // This for testing date editing. The value isn't actually stored in the database.
      private string _description;
      private string _modelName;
      private string _modelNumber;
      private decimal _unitCost;

      public Product(string modelNumber, string modelName, decimal unitCost, string description)
      {
         ModelNumber = modelNumber;
         ModelName = modelName;
         UnitCost = unitCost;
         Description = description;
      }

      public Product(string modelNumber, string modelName, decimal unitCost, string description,
         string productImagePath)
         : this(modelNumber, modelName, unitCost, description)
      {
         ProductImagePath = productImagePath;
      }

      public Product(string modelNumber, string modelName, decimal unitCost, string description, int categoryId,
         string categoryName, string productImagePath)
         : this(modelNumber, modelName, unitCost, description)
      {
         CategoryName = categoryName;
         ProductImagePath = productImagePath;
         CategoryId = categoryId;
      }

      public string ModelNumber
      {
         get { return _modelNumber; }
         set
         {
            _modelNumber = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModelNumber)));
         }
      }

      public string ModelName
      {
         get { return _modelName; }
         set
         {
            _modelName = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModelName)));
         }
      }

      public decimal UnitCost
      {
         get { return _unitCost; }
         set
         {
            _unitCost = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(UnitCost)));
         }
      }

      public string Description
      {
         get { return _description; }
         set
         {
            _description = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Description)));
         }
      }

      public string CategoryName { get; set; }

      public int CategoryId { get; set; }

      public string ProductImagePath { get; set; }

      public DateTime DateAdded { get; set; } = DateTime.Today;

      public event PropertyChangedEventHandler PropertyChanged;

      public override string ToString() => $"{ModelName} ({ModelNumber})";

      private void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
   }
}