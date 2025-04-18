﻿namespace V2;

public class User
{
   public User(int userId, string email, UserType type, bool isEmailConfirmed)
   {
      UserId = userId;
      Email = email;
      Type = type;
      IsEmailConfirmed = isEmailConfirmed;
      DomainEvents = new List<IDomainEvent>();
   }

   public int UserId { get; set; }
   public string Email { get; private set; }
   public UserType Type { get; private set; }
   public bool IsEmailConfirmed { get; }
   public List<IDomainEvent> DomainEvents { get; }

   public string CanChangeEmail()
   {
      if (IsEmailConfirmed)
      {
         return "Can't change email after it's confirmed";
      }

      return null;
   }

   public void ChangeEmail(string newEmail, Company company)
   {
      Precondition.Requires(CanChangeEmail() == null);

      if (Email == newEmail)
      {
         return;
      }

      var newType = company.IsEmailCorporate(newEmail)
         ? UserType.Employee
         : UserType.Customer;

      if (Type != newType)
      {
         var delta = newType == UserType.Employee ? 1 : -1;
         company.ChangeNumberOfEmployees(delta);
         AddDomainEvent(new UserTypeChangedEvent(UserId, Type, newType));
      }

      Email = newEmail;
      Type = newType;
      AddDomainEvent(new EmailChangedEvent(UserId, newEmail));
   }

   private void AddDomainEvent(IDomainEvent domainEvent)
   {
      DomainEvents.Add(domainEvent);
   }
}