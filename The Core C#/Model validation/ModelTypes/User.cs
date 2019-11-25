using System.ComponentModel.DataAnnotations;
using MessageResources;

namespace ModelTypes
{
   [UserValidation]
   public class User
   {
      [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "UserId_Error")]
      public string Id { get; set; }

      [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "UserName_NotSpecified")]
      [StringLength(50, MinimumLength = 3)]
      public string Name { get; set; }

      [Required]
      [Range(1, 100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "UserAge_OutOfRange")]
      public int Age { get; set; }

      [Required]
      [RegularExpression(@"^\+[1-9]\d{3}-\d{3}-\d{4}$",
         ErrorMessageResourceType = typeof(Messages),
         ErrorMessageResourceName = "UserPhone_InvalidPhoneNumber")]
      //[Phone]
      public string Phone { get; set; }

      [CreditCard]
      public string BankAccount { get; set; }

      [Url]
      public string SocialUrl { get; set; }
   }
}