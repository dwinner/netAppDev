using Fundamentals;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CrossCutting.Commands;

public static class ModelErrorExtensions
{
   public static ValidationResult ToValidationResult(this ModelError error, string member)
   {
      member = string.Join('.', member.Split('.').Select(str => str.ToCamelCase()));
      return new ValidationResult(error.ErrorMessage, member);
   }
}