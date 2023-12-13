namespace EF;

public class CompanyFactory
{
   public static Company Create(object[] data)
   {
      Precondition.Requires(data.Length >= 2);

      var domainName = (string)data[0];
      var numberOfEmployees = (int)data[1];

      return new Company(domainName, numberOfEmployees);
   }
}