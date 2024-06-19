namespace PrototypeFactory;

public class Person : IDeepCopyable<Person>
{
   public Person(string name, Address address)
   {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      Address = address ?? throw new ArgumentNullException(nameof(address));
   }

   public Person(Person other)
   {
      Name = other.Name;
      Address = new Address(other.Address);
   }

   public string Name { get; set; }

   public Address Address { get; }

   public Person DeepCopy() => (Person)MemberwiseClone();

   public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
}