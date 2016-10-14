using System.Text;

namespace Customers
{
   public sealed class CustomerHardCoded : Customer
   {
      public override string ToString()
         => new StringBuilder()
            .Append("Age: ").Append(Age)
            .Append(Constants.Separator)
            .Append("Id: ").Append(Id)
            .Append(Constants.Separator)
            .Append("FirstName: ").Append(FirstName)
            .Append(Constants.Separator)
            .Append("LastName: ").Append(LastName).ToString();
   }
}