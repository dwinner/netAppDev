using System.ComponentModel.DataAnnotations;

namespace CrossCutting;

public record Employee(
   [Required] string FirstName,
   [Required] string LastName);