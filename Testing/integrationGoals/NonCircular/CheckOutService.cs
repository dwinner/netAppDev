namespace NonCircular;

public class CheckOutService
{
   public void CheckOut(int orderId)
   {
      var service = new ReportGenerationService();
      var report = service.GenerateReport(orderId);

      /* other work */
   }
}