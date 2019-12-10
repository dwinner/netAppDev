using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace NotifyPropertyChangedDemo
{
   /// <summary>
   ///    Логика взаимодействия для MainWpfWindow.xaml
   /// </summary>
   public partial class MainWpfWindow
   {
      private readonly MyDataClass _data = new MyDataClass();
      private readonly Random _rand = new Random();

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

      private void data_PropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         // todo: Ддействия при изменении свойства
      }

      private void OnTimer(object sender, EventArgs e)
      {
         _data.Tag = _rand.Next(); // Периодически присваивать случайные значения
      }
   }
}