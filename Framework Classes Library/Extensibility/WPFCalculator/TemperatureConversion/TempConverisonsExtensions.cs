using System;

namespace TemperatureConversion
{
   /// <summary>
   /// Методы расширения для преобразования температур
   /// </summary>
   public static class TempConverisonsExtensions
   {
      /// <summary>
      /// Преобразование температуры в градусы по Цельсию
      /// </summary>
      /// <param name="temperature">Температура в указанном формате второго аргумента</param>
      /// <param name="fromConversionType">Тип, в котором находится текущее значение температуры</param>
      /// <returns>Температура в градусах по Цельсию</returns>
      public static double ToCelsius(this double temperature, TempConversionType fromConversionType)
      {
         switch (fromConversionType)
         {
            case TempConversionType.Celsius:
               return temperature;
            case TempConversionType.Fahrenheit:
               return (temperature - 32) / 1.8;
            case TempConversionType.Kelvin:
               return temperature - 273.15;
            default:
               throw new ArgumentException("invalid enumeration value");
         }
      }

      /// <summary>
      /// Преобразование температуры из градусов по Цельсию в указанный тип
      /// </summary>
      /// <param name="celsiusTemp">Температура по Цельсию</param>
      /// <param name="toConversionType">Тип назначения</param>
      /// <returns>Температура в указанном типе назначения</returns>
      public static double FromCelsius(this double celsiusTemp, TempConversionType toConversionType)
      {
         switch (toConversionType)
         {
            case TempConversionType.Celsius:
               return celsiusTemp;
            case TempConversionType.Fahrenheit:
               return (celsiusTemp * 1.8) + 32;
            case TempConversionType.Kelvin:
               return celsiusTemp + 273.15;
            default:
               throw new ArgumentException("invalid enumeration value");
         }
      }
   }
}