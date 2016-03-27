namespace AbstractFactory
{
   /// <summary>
   /// Общий базовый класс для представления адресов.
   /// </summary>
   public abstract class Address
   {
      public const string EolString = "\r\n";
      public const string Space = " ";

      public virtual string Street { get; set; }
      
      public virtual string City { get; set; }

      public virtual string Region { get; set; }

      public virtual string PostalCode { get; set; }

      public abstract string GetCountry();

      public abstract string GetFullAddress();
   }

   /// <summary>
   /// Класс адресов Франции.
   /// </summary>
   class FrenchAddress : Address
   {
      private const string COUNTRY = "FRANCE";

      public override string GetCountry()
      {
         return COUNTRY;
      }

      public override string GetFullAddress()
      {
         return Street + EolString + PostalCode + Space + City + EolString + COUNTRY + EolString;
      }
   }

   /// <summary>
   /// Класс адресов США
   /// </summary>
   class UsAddress : Address
   {
      private const string COUNTRY = "UNITED STATES";
      private const string COMMA = ",";

      public override string GetCountry()
      {
         return COUNTRY;
      }

      public override string GetFullAddress()
      {
         return Street + EolString + City + COMMA + Space + Region + Space + PostalCode + EolString + COUNTRY +
                EolString;
      }
   }
   
}
