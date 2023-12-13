namespace Circular;

public class CheckOutService
{
   public void CheckOut(int orderId)
   {
      var service = new ReportGenerationService();
      service.GenerateReport(orderId, this);

      /* other work */
   }
}