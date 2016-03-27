namespace FatThinAspects.UnitTesting
{
   public interface IMyCrossCuttingConcern
   {
      void BeforeMethod(string logMessage);
      void AfterMethod(string logMessage);
   }
}