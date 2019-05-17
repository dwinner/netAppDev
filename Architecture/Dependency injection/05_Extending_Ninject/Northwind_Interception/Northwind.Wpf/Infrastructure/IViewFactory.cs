namespace Northwind.Wpf.Infrastructure
{
   public interface IViewFactory
   {
      T CreateView<T>() where T : IView;

      T CreateView<T>(ViewModel viewModel) where T : IView;
   }
}