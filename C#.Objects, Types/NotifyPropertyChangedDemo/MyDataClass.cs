namespace NotifyPropertyChangedDemo
{
   class MyDataClass : System.ComponentModel.INotifyPropertyChanged
   {
      public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         if (PropertyChanged != null)
         {
            PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
         }
      }

      private int _tag = 0;
      public int Tag
      {
         get { return _tag; }
         set
         {
            _tag = value;
            OnPropertyChanged("Tag");
         }
      }
   }
}
