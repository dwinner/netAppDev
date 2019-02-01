using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NotifyPropertyChanged.ViaPs.Annotations;

namespace NotifyPropertyChanged.ViaPs.ViewModels
{
   /// <summary>
   ///    Ручная реализация
   /// </summary>
   public class NameViewModel : INotifyPropertyChanged
   {
      private string _firstName;
      private string _lastName;

      public string FirstName
      {
         get { return _firstName; }
         set
         {
            if (_firstName != null && value != _firstName)
            {
               _firstName = value;
               OnPropertyChanged(nameof(FirstName));
               OnPropertyChanged(nameof(FullName));
            }
         }
      }

      public string LastName
      {
         get { return _lastName; }
         set
         {
            if (_lastName != null && value != _lastName)
            {
               _lastName = value;
               OnPropertyChanged(nameof(LastName));
               OnPropertyChanged(nameof(FullName));
            }
         }
      }

      public string FullName => $"{_firstName} {_lastName}";
      public event PropertyChangedEventHandler PropertyChanged;

      /// <exception cref="Exception">A delegate callback throws an exception.</exception>
      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}