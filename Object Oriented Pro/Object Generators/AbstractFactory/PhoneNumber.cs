using System;

namespace AbstractFactory
{
   /// <summary>
   /// Общий базовый класс для представления телефонных номеров.
   /// </summary>
   public abstract class PhoneNumber
   {
      public virtual string TelephoneNumber { get; set; }

      public abstract string GetCountryCode();
   }

   /// <summary>
   /// Номер телефона во Франции
   /// </summary>
   class FrenchPhoneNumber : PhoneNumber
   {
      private const string COUNTRY_CODE = "33";
      private const int NUMBER_LENGTH = 9;

      public override string GetCountryCode()
      {
         return COUNTRY_CODE;
      }

      public override string TelephoneNumber
      {
         get { return base.TelephoneNumber; }
         set
         {
            if (value.Length == NUMBER_LENGTH)
            {
               base.TelephoneNumber = value;
            }
            else
            {
               throw new ArgumentException(string.Format("Length must be {0}", NUMBER_LENGTH));
            }
         }
      }
   }

   /// <summary>
   /// Номер телефона в США.
   /// </summary>
   class UsPhoneNumber : PhoneNumber
   {
      private const string COUNTRY_CODE = "01";
      private const int NUMBER_LENGTH = 10;

      public override string GetCountryCode()
      {
         return COUNTRY_CODE;
      }

      public override string TelephoneNumber
      {
         get { return base.TelephoneNumber; }
         set
         {
            if (value.Length == NUMBER_LENGTH)
            {
               base.TelephoneNumber = value;
            }
            else
            {
               throw new ArgumentException(string.Format("Length must be {0}", NUMBER_LENGTH));
            }
         }
      }
   }
}
