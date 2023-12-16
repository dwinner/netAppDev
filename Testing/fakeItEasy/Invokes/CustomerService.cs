namespace Invokes;

public class CustomerService(IBuildCsv buildCsv)
{
   public string GetLastAndFirstNamesAsCsv(List<Customer> customers)
   {
      buildCsv.SetHeader(new[] { "Last Name", "First Name" });
      customers.ForEach(customer => buildCsv.AddRow(new[] { customer.LastName, customer.FirstName }));
      return buildCsv.Build();
   }
}