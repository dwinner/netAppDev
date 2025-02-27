﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _10_TPHWithConventions
{
   public class Payment
   {
      public int PaymentId { get; set; }

      [Required]
      public string Name { get; set; }

      [Column(TypeName = "Money")]
      public decimal Amount { get; set; }
   }
}