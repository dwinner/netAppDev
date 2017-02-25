using System.Windows;
using FirstsStepsRUI.ViewModels;
using ReactiveUI;

namespace FirstsStepsRUI.Views
{
   public partial class PlaceHolderView : IViewFor<PlaceHolderViewModel>
   {
      public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel",
         typeof(PlaceHolderViewModel), typeof(PlaceHolderView), new PropertyMetadata(null));

      public PlaceHolderView()
      {
         InitializeComponent();
         this.BindCommand(ViewModel, vm => vm.ChangeView, v => v.ClickMe);
      }

      object IViewFor.ViewModel
      {
         get { return ViewModel; }
         set { ViewModel = (PlaceHolderViewModel) value; }
      }

      public PlaceHolderViewModel ViewModel
      {
         get { return (PlaceHolderViewModel) GetValue(ViewModelProperty); }
         set { SetValue(ViewModelProperty, value); }
      }
   }
}