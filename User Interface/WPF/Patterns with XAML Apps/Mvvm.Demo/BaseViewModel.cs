using System.ComponentModel;

namespace MVVMDemo
{
   public abstract class BaseViewModel : INotifyPropertyChanged
   {
      private string _displayName = "Unknown";

      public string DisplayName
      {
         get
         {
            return _displayName;
         }
         set
         {
            _displayName = value;
            OnPropertyChanged("DisplayName");
         }
      }

      protected BaseViewModel(string displayName)
      {
         DisplayName = displayName;
      }

      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged(string propertyName)
      {
         if (PropertyChanged != null)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
      }
   }
}
