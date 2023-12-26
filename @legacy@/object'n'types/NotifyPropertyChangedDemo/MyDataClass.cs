using System.ComponentModel;

namespace NotifyPropertyChangedDemo
{
   internal class MyDataClass : INotifyPropertyChanged
   {
      private int _tag;

      public int Tag
      {
         get => _tag;
         set
         {
            _tag = value;
            OnPropertyChanged("Tag");
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         if (PropertyChanged != null)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
      }
   }
}