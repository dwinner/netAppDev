using System.ComponentModel.DataAnnotations;
using MessageResources;

namespace ModelTypes
{
   public class UserValidationAttribute : ValidationAttribute
   {
      public override bool IsValid(object value)
      {
         if (value is User user && user.Name == "Alice" && user.Age == 33)
         {
            ErrorMessage = Messages.User_Alice;
            return false;
         }

         return true;
      }
   }
}