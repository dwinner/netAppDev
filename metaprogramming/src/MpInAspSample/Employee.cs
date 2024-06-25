using System.ComponentModel.DataAnnotations;

namespace MpInAspSample;

public record Employee(
   [Required] string FirstName,
   [Required] string LastName);