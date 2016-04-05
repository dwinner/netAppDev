using System;
using System.Collections.ObjectModel;

namespace MVVMDemo
{
   public class AllWidgetsViewModel : BaseViewModel
   {
      private readonly WidgetRepository _widgets;
      
      // Коллекция моделей представления доступна представлению,
      // которое выводит ее, как считает нужным
      public ObservableCollection<WidgetViewModel> WidgetViewModels { get; private set; }

      public AllWidgetsViewModel(WidgetRepository widgets)
         : base("All Widgets")
      {
         _widgets = widgets;
         _widgets.WidgetAdded += OnWidgetAdded;

         CreateViewModels();
      }

      internal void OnWidgetAdded(object sender, EventArgs e)
      {
         CreateViewModels();
      }

      private void CreateViewModels()
      {
         WidgetViewModels = new ObservableCollection<WidgetViewModel>();
         foreach (Widget widget in _widgets.Widgets)
         {
            WidgetViewModels.Add(new WidgetViewModel(widget));
         }
         OnPropertyChanged("WidgetViewModels");
      }
   }
}
