using System.ComponentModel.DataAnnotations;
using CustomValidationAttributes.Properties;

namespace CustomValidationAttributes
{
   public class UserNameAttribute : ValidationAttribute
   {
      public override bool IsValid(object value)
      {
         if (value != null)
         {
            var userName = value.ToString();
            if (!userName.StartsWith("T"))
            {
               return true;
            }

            ErrorMessage = CustomValidationMessages.UserName_StartWithTError;
         }

         return false;
      }
   }
}