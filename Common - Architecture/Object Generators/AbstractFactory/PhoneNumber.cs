namespace AbstractFactory
{   
   public abstract class PhoneNumber
   {
      public virtual string TelephoneNumber { get; set; }

      public abstract string GetCountryCode();
   }
}
