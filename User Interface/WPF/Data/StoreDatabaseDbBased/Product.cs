using System;
using System.ComponentModel;

namespace StoreDatabase
{
   public class Product : INotifyPropertyChanged
   {
      // For DataGridComboBoxColumn example.

      // This for testing date editing. The value isn't actually stored in the database.
      string description;
      string modelName;
      string modelNumber;
      decimal unitCost;

      public Product(string modelNumber, string modelName, decimal unitCost, string description)
      {
         ModelNumber = modelNumber;
         ModelName = modelName;
         UnitCost = unitCost;
         Description = description;
      }

      public Product(string modelNumber, string modelName, decimal unitCost, string description, string productImagePath)
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
         get { return modelNumber; }
         set
         {
            modelNumber = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModelNumber)));
         }
      }

      public string ModelName
      {
         get { return modelName; }
         set
         {
            modelName = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModelName)));
         }
      }

      public decimal UnitCost
      {
         get { return unitCost; }
         set
         {
            unitCost = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(UnitCost)));
         }
      }

      public string Description
      {
         get { return description; }
         set
         {
            description = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Description)));
         }
      }

      public string CategoryName { get; set; }

      public int CategoryId { get; set; }

      public string ProductImagePath { get; set; }

      public DateTime DateAdded { get; set; } = DateTime.Today;

      public event PropertyChangedEventHandler PropertyChanged;

      public override string ToString()
      {
         return $"{ModelName} ({ModelNumber})";
      }

      void OnPropertyChanged(PropertyChangedEventArgs e)
      {
         PropertyChanged?.Invoke(this, e);
      }
   }
}