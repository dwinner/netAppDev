﻿using System.ComponentModel.DataAnnotations;

namespace PartyInvites
{
   /// <summary>
   /// Сущность для приглашения на Party
   /// </summary>
   public class GuestResponse
   {
      [Required]
      public string Name { get; set; }

      [Required]
      public string Email { get; set; }

      [Required]
      public string Phone { get; set; }

      [Required(ErrorMessage = "Please tell us if you will attend")]
      public bool? WillAttend { get; set; }
   }
}