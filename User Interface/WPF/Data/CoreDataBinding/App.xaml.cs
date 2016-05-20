using StoreDatabase;

namespace DataBinding
{
   public partial class App
   {
      static readonly StoreDb StoreDbInstance = new StoreDb();

      static readonly StoreDbDataSet StoreDbDataSetImpl = new StoreDbDataSet();

      public static StoreDb StoreDb
      {
         get { return StoreDbInstance; }
      }

      public static StoreDbDataSet StoreDbDataSet
      {
         get { return StoreDbDataSetImpl; }
      }
   }
}