using System;

// Далее необходимо перечислить атрибуты уровня сборки или модуля.
// Следует позаботиться, чтобы все общедоступные типы в данной
// сборке обязательно отвечали требованиям CLS.
[assembly: CLSCompliant(true)]

namespace AttributedCarLibrary
{
   // Специальный атрибут.
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
   public sealed class VehicleDescriptionAttribute : Attribute
   {
      public string Description { get; set; }

      public VehicleDescriptionAttribute(string vehicalDescription)
      {
         Description = vehicalDescription;
      }

      public VehicleDescriptionAttribute() { }
   }
}
