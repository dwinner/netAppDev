namespace DeliveryServiceTests;

public class DeliveryService
{
   public bool IsDeliveryValid(Delivery delivery) => delivery.Date >= DateTime.Now.AddDays(1.999);
}