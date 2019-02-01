namespace AopXaml
{
   using PostSharp.Patterns.Model;

   [NotifyPropertyChanged]
   public class CustomerViewModel
   {
      public CustomerModel Customer { get; set; }

      public string FullName
         =>
            Customer == null
               ? "(No Data)"
               : $"{Customer.FirstName} {Customer.LastName} from {(Customer.PrincipalAddress != null ? Customer.PrincipalAddress.FullAddress : "?")}"
         ;
   }
}