using FuelEconomyUWP;

namespace FuelEconomyWPF
{
   public partial class FuelEconomyUserControl
   {
      public FuelEconomyUserControl()
      {
         InitializeComponent();
         DataContext = this;
      }

      public FuelEconomyViewModel ViewModel { get; } = new FuelEconomyViewModel();
   }
}