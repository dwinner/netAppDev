using System;
using System.ComponentModel;
using System.Reflection;
using PostSharp.Aspects;

namespace NotifyPropertyChanged.ViaPs.Aspects
{
   [Serializable]
   public class OldStyleNotifyPropertyChangeAspectAttribute : LocationInterceptionAspect
   {
      private readonly string[] _derivedProperties;

      public OldStyleNotifyPropertyChangeAspectAttribute(params string[] derivedProperties)
      {
         _derivedProperties = derivedProperties;
      }

      public override void OnSetValue(LocationInterceptionArgs args)
      {
         var oldValue = args.GetCurrentValue();
         var newValue = args.Value;
         if (oldValue != newValue)
         {
            args.ProceedSetValue();
            RaisePropertyChanged(args.Instance, args.LocationName);
            if (_derivedProperties == null)
               return;

            foreach (var derivedProperty in _derivedProperties)
            {
               RaisePropertyChanged(args.Instance, derivedProperty);
            }
         }
      }

      private void RaisePropertyChanged(object anInstance, string aPropertyName)
      {
         var type = anInstance.GetType();
         var propertyChanged = type.GetField(nameof(INotifyPropertyChanged.PropertyChanged),
            BindingFlags.Instance | BindingFlags.NonPublic);
         if (propertyChanged != null)
         {
            var handler = propertyChanged.GetValue(anInstance) as PropertyChangedEventHandler;
            handler?.Invoke(anInstance, new PropertyChangedEventArgs(aPropertyName));
         }
      }
   }

   /*public class NameViewModelNotifyPropertyWeaver : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string FullName => $"{FirstName} {LastName}";
   }*/
}