namespace UsingMocks4;

public class CustomerController
{
   private readonly CustomerRepository _customerRepository;
   private readonly IEmailGateway _emailGateway;
   private readonly Store _mainStore;
   private readonly ProductRepository _productRepository;

   public CustomerController(IEmailGateway emailGateway) => _emailGateway = emailGateway;

   public bool Purchase(int customerId, int productId, int quantity)
   {
      var customer = _customerRepository.GetById(customerId);
      var product = _productRepository.GetById(productId);

      var isSuccess = customer.Purchase(_mainStore, product, quantity);

      if (isSuccess)
      {
         _emailGateway.SendReceipt(customer.Email, product.Name, quantity);
      }

      return isSuccess;
   }
}