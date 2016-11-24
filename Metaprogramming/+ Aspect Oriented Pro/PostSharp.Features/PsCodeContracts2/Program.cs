// Контракты

using System;
using PostSharp.Aspects;
using PostSharp.Patterns.Contracts;
using PostSharp.Reflection;

namespace PsCodeContracts2
{
   internal static class Program
   {
      private static void Main()
      {
         try
         {
            var customerModel = new CustomerModel();
            customerModel.SetFullName(string.Empty, null);
            Console.WriteLine("{0} {1}", customerModel.FirstName, customerModel.SecondName);
         }
         catch (ArgumentNullException argumentNullEx)
         {
            Console.WriteLine(argumentNullEx.ParamName);
         }         

         try
         {
            var mod = Divider.Mod(0, 0);
         }
         catch (ArgumentOutOfRangeException argumentOutOfRangeEx)
         {
            Console.WriteLine(argumentOutOfRangeEx.Message);
         }
      }
   }

   public class CustomerModel : ICustomerModel
   {
      [Required]
      public string FirstName { get; set; }

      [Required]
      public string SecondName { get; set; }

      public void SetFullName(string aFirstName, string aLastName)
      {
         FirstName = aFirstName;
         SecondName = aLastName;
      }
   }

   public interface ICustomerModel
   {
      void SetFullName([Required] string aFirstName, [Required] string aLastName);
   }

   public class NonZeroAttribute : LocationContractAttribute, ILocationValidationAspect<int>,
      ILocationValidationAspect<uint>
   {
      private new const string ErrorMessage = "NonZeroErrorMessage";

      public Exception ValidateValue(int value, string locationName, LocationKind locationKind)
      {
         return value == 0 ? CreateArgumentOutOfRangeException(value, locationName, locationKind) : null;
      }

      public Exception ValidateValue(uint value, string locationName, LocationKind locationKind)
      {
         return value == 0 ? CreateArgumentOutOfRangeException(value, locationName, locationKind) : null;
      }

      protected override string GetErrorMessage()
      {
         return "Value {2} must have a non-zero value.";
      }
   }

   internal static class Divider
   {
      internal static bool Mod([NonZero] int number, [NonZero] int dividend) => number % dividend == 0;
      internal static bool Mod([NonZero] uint number, [NonZero] uint dividend) => number % dividend == 0;
   }
}