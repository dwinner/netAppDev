using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StoreDatabase
{
   public class Category : INotifyPropertyChanged
   {
      // For DataGridComboBoxColumn example.
      private int _categoryId;
      private string _categoryName;
      private ObservableCollection<Product> _products;

      public Category(string categoryName, ObservableCollection<Product> products)
      {
         CategoryName = categoryName;
         Products = products;
      }

      public Category(string categoryName, int categoryId)
      {
         CategoryName = categoryName;
         CategoryId = categoryId;
      }

      public string CategoryName
      {
         get { return _categoryName; }
         set
         {
            _categoryName = value;
            OnPropertyChanged(new PropertyChangedEventArgs("CategoryName"));
         }
      }

      public int CategoryId
      {
         get { return _categoryId; }
         set
         {
            _categoryId = value;
            OnPropertyChanged(new PropertyChangedEventArgs("CategoryID"));
         }
      }

      public ObservableCollection<Product> Products
      {
         get { return _products; }
         set
         {
            _products = value;
            OnPropertyChanged(new PropertyChangedEventArgs("Products"));
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      public void OnPropertyChanged(PropertyChangedEventArgs e)
      {
         if (PropertyChanged != null)
            PropertyChanged(this, e);
      }
   }
}