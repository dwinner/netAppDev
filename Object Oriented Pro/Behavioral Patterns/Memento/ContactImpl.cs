using System;

namespace Memento
{
   public class ContactImpl : IContact, IEquatable<ContactImpl>
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Title { get; set; }

      public string Organization { get; set; }

      public IAddress Address { get; set; }

      public ContactImpl(string firstName, string lastName, string title, string organization, IAddress address)
      {
         FirstName = firstName;
         LastName = lastName;
         Title = title;
         Organization = organization;
         Address = address;
      }

      public ContactImpl()
      {
      }

      public override string ToString()
      {
         return string.Format("FirstName: {0}, LastName: {1}, Title: {2}, Organization: {3}, Address: {4}", FirstName,
            LastName, Title, Organization, Address);
      }

      public bool Equals(ContactImpl other)
      {
         if (ReferenceEquals(null, other))
            return false;
         if (ReferenceEquals(this, other))
            return true;
         return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) &&
                string.Equals(Title, other.Title) && string.Equals(Organization, other.Organization) &&
                Equals(Address, other.Address);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj))
            return false;
         if (ReferenceEquals(this, obj))
            return true;
         if (obj.GetType() != GetType())
            return false;
         return Equals((ContactImpl)obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            int hashCode = (FirstName != null ? FirstName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Organization != null ? Organization.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Address != null ? Address.GetHashCode() : 0);
            return hashCode;
         }
      }

      public static bool operator ==(ContactImpl left, ContactImpl right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(ContactImpl left, ContactImpl right)
      {
         return !Equals(left, right);
      }
   }
}