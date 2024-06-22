/*
 * Note: Ручная реализация глубокого копирования
 */

using System;

namespace ObjectsCloning
{
   public sealed class Title : ICloneable
   {
      public enum TitleNameEnum
      {
         GreenHorn,
         HotspotGuru
      }

      private readonly TitleNameEnum _title;

      // ReSharper disable once NotAccessedField.Local
      private double _maxPay;

      // ReSharper disable once NotAccessedField.Local
      private double _minPay;

      public Title(TitleNameEnum titleName)
      {
         _title = titleName;
         LookupPayScale();
      }

      private Title(Title otherTitle)
      {
         _title = otherTitle._title;
         LookupPayScale();
      }

      #region Реализация IClonable

      public object Clone() => new Title(this);

      #endregion

      private void LookupPayScale()
      {
         // Находит тарифную сетку в базе данных.
         // Тарифная сетка определяется званием
         _minPay = 0;
         _maxPay = 0;
      }
   }

   public sealed class Employee : ICloneable
   {
      private readonly string _name;
      private readonly string _ssn;
      private readonly Title _title;

      public Employee(string name, Title title, string ssn)
      {
         _name = name;
         _title = title;
         _ssn = ssn;
      }

      private Employee(Employee otherEmployee)
      {
         _name = string.Copy(otherEmployee._name);
         _title = (Title)otherEmployee._title.Clone();
         _ssn = string.Copy(otherEmployee._ssn);
      }

      #region Реализация IClonable

      public object Clone() => new Employee(this);

      #endregion
   }
}