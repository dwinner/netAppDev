using System.Threading.Tasks;
using LiveShaping.Data;

namespace LiveShaping
{
   public partial class MainWindow
   {
      private readonly LapChart _lapChart = new LapChart();

      public MainWindow()
      {
         InitializeComponent();
         DataContext = _lapChart.GetLapInfo();

         // ReSharper disable ReturnedTaskNotObserved
         Task.Run(async () =>
         {
            var raceContinues = true;
            while (raceContinues)
            {
               await Task.Delay(3000).ConfigureAwait(true);
               raceContinues = _lapChart.NextLap();
            }
         });
         // ReSharper restore ReturnedTaskNotObserved
      }
   }
}