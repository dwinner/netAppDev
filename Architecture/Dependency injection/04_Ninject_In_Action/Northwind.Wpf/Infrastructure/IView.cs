namespace Northwind.Wpf.Infrastructure
{
   public interface IView
   {
      bool? DialogResult { get; set; }

      bool? ShowDialog();

      void Show();

      void Close();
   }
}