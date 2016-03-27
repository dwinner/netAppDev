namespace Proxy.Canonical
{
   public class ProxyChiefDesignEngineer : IEmployee
   {
      private readonly IEmployee _employee;
      private readonly object _syncRoot = new object();

      public ProxyChiefDesignEngineer(IEmployee employee)
      {
         _employee = employee;
      }

      public string Design()
      {
         lock (_syncRoot)
         {
            return _employee.Design();
         }
      }

      public string StressTest()
      {
         lock (_syncRoot)
         {
            return _employee.StressTest();
         }
      }

      public string Mechanical()
      {
         lock (_syncRoot)
         {
            return _employee.Mechanical();
         }
      }

      public string Performance()
      {
         lock (_syncRoot)
         {
            return _employee.Performance();
         }
      }
   }
}