namespace Facade
{
   public class DealerRepresentative
   {
      private readonly PizzaDelivery _delivery;
      private readonly PizzaFinance _finance;
      private readonly PizzaInsurance _insurance;
      private readonly PizzaOrder _order;
      private readonly PizzaRegistration _registration;
      private readonly PizzaCooking _service;

      public DealerRepresentative()
      {
         _delivery = new PizzaDelivery();
         _finance = new PizzaFinance();
         _insurance = new PizzaInsurance();
         _order = new PizzaOrder();
         _registration = new PizzaRegistration();
         _service = new PizzaCooking();
      }

      public void GetPizzaUpdate()
      {
         _delivery.GetDeliveryStaff();
         _finance.GetPizzaFinanceStuff();
         _insurance.GetPizzaInsuranceStuff();
         _order.GetPizzaOrderStuff();
         _registration.GetPizzaRegistrationStuff();
         _service.GetPizzaServiceStuff();
      }
   }
}