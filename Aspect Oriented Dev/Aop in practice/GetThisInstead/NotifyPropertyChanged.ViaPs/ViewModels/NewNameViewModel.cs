using PostSharp.Patterns.Model;

namespace NotifyPropertyChanged.ViaPs.ViewModels
{
   [NotifyPropertyChanged]
   public class NewNameViewModel
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string FullName => $"{FirstName} {LastName}";
   }
}