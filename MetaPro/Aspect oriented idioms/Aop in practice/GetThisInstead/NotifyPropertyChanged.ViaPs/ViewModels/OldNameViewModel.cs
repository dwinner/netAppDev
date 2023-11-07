using System.ComponentModel;
using System.Runtime.CompilerServices;
using NotifyPropertyChanged.ViaPs.Annotations;
using NotifyPropertyChanged.ViaPs.Aspects;

namespace NotifyPropertyChanged.ViaPs.ViewModels
{
   public class OldNameViewModel : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      
      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      [OldStyleNotifyPropertyChangeAspect(nameof(FullName))]
      public string FirstName { get; set; }

      [OldStyleNotifyPropertyChangeAspect(nameof(FullName))]
      public string LastName { get; set; }

      public string FullName => $"{FirstName} {LastName}";
   }
}