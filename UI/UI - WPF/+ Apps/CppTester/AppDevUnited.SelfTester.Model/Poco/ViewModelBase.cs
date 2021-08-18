using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppDevUnited.SelfTester.Model.Poco
{
   public abstract class ViewModelBase : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         var handler = PropertyChanged;
         if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}