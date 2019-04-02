using GalaSoft.MvvmLight.Threading;

namespace MvvmLightSample
{
   public partial class App
   {
      static App()
      {
         DispatcherHelper.Initialize();
      }
   }
}