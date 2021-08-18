// Прозрачное декорирование свойств на примере извлечения значений из системного реестра

using System;
using Microsoft.Win32;
using PostSharp.Aspects;

namespace TransparentRegistryValue
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   [Serializable]
   public class RegistryValueAttribute : LocationInterceptionAspect, IInstanceScopedAspect
   {
      [NonSerialized] private bool _fetchedFromRegistry;

      public RegistryValueAttribute(string keyName, string valueName)
      {
         KeyName = keyName;
         ValueName = valueName;
      }

      public string KeyName { get; set; }
      public string ValueName { get; set; }
      public object DefaultValue { get; set; }

      object IInstanceScopedAspect.CreateInstance(AdviceArgs adviceArgs) => MemberwiseClone();

      void IInstanceScopedAspect.RuntimeInitializeInstance()
      {
      }

      public override void OnGetValue(LocationInterceptionArgs args)
      {
         if (!_fetchedFromRegistry)
         {
            var value = Registry.GetValue(KeyName, ValueName, DefaultValue);
            args.SetNewValue(value);
            args.Value = value;
         }
         else
         {
            args.ProceedGetValue();
         }
      }

      public override void OnSetValue(LocationInterceptionArgs args)
      {
         if (Equals(args.Value, args.GetCurrentValue()))
         {
            Registry.SetValue(KeyName, ValueName, args.Value);
         }

         base.OnSetValue(args);
         _fetchedFromRegistry = true;
      }
   }
}