using LifecycleSample.Common;

namespace LifecycleSample.DataModel
{
   public class Page2Data : BindableBase
   {
      private string _data;

      public string Data
      {
         get { return _data; }
         set { SetProperty(ref _data, value); }
      }
   }
}