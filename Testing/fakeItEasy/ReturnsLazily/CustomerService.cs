using System.Text;

namespace ReturnsLazily;

public class CustomerService(ICustomerRepository customerRepository)
{
   public string GetCustomerNamesAsCsv(IEnumerable<int> customerIds)
   {
      var customers = new StringBuilder();
      foreach (var customerId in customerIds)
      {
         var customer = customerRepository.GetCustomerById(customerId);
         customers.Append($"{customer.FirstName} {customer.LastName},");
      }

      RemoveTrailingComma(customers);
      return customers.ToString();
   }

   private static void RemoveTrailingComma(StringBuilder stringBuilder)
   {
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
   }
}