using System;

namespace AttributedCarLibrary
{
   [Serializable]
   [VehicleDescription(Description = "My rocking Harley")]  // Мой классный Харли
   public class Motorcycle
   {
   }

   [Serializable]
   [Obsolete("Use another vehicle!")]
   // Используйте другое транспортное средство!
   [VehicleDescription("The old gray mare, she ain't what she used to be...")]
   // Старая серая кобыла, она уже не та, что была раньше...
   public class HorseAndBuggy
   {
   }

   [VehicleDescription("A very long, slow, but feature-rich auto")]
   // Очень длинное, медленное, но богатое возможностями авто
   public class Winnebago
   {
   }
}
