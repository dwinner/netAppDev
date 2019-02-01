using System;
using System.Windows.Threading;

namespace NotifyPropertyChangedDemo
{
   /// <summary>
   /// Логика взаимодействия для MainWpfWindow.xaml
   /// </summary>
   public partial class MainWpfWindow
   {
      private readonly Random _rand = new Random();
      private readonly MyDataClass _data = new MyDataClass();

      public MainWpfWindow()
      {
         InitializeComponent();

         new DispatcherTimer(
            new TimeSpan(0, 0, 1), DispatcherPriority.Normal, OnTimer, Dispatcher);
                  
         // Указать WPF наблюдать за привязкой данных в этом объекте
         // WPF будет использовать INotifyPropertyChanged для обновления UI
         DataContext = _data;
         _data.PropertyChanged += data_PropertyChanged;
      }

      void data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
      {
         // todo: Ддействия при изменении свойства
      }

      private void OnTimer(object sender, EventArgs e)
      {         
         _data.Tag = _rand.Next();  // Периодически присваивать случайные значения
      }
   }
}
