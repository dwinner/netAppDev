using System.ComponentModel.DataAnnotations;

namespace ModelTypes
{
   public class RegisterModel
   {
      [Required]
      [EmailAddress]
      public string Login { get; set; }

      [Required]
      public string Password { get; set; }

      [Required]
      [Compare(nameof(Password))]
      public string ConfirmPassword { get; set; }
   }
}