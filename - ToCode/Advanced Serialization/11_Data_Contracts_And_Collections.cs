[DataContract] public class Person
{
  [DataMember (Name="Addresses")]
  List<Address> _addresses;
  public IList<Address> Addresses { get { return _addresses; } }
}

[DataContract] public class Address
{
  [DataMember] public string Street, Postcode;
}

// Subclassing collection elements:
[DataContract, KnownType (typeof (USAddress))]
public class Address
{
  [DataMember] public string Street, Postcode;
}

public class USAddress : Address { }

// Customizing collection and element names:
[CollectionDataContract (ItemName="Residence")]
public class AddressList : Collection<Address> { }

[DataContract] public class Person
{
  ...
  [DataMember] public AddressList Addresses;
}

[CollectionDataContract (ItemName="Entry", KeyName="Kind", ValueName="Number")]
public class PhoneNumberList : Dictionary <string, string> { }

[DataContract] public class Person
{
  ...
  [DataMember] public PhoneNumberList PhoneNumbers;
}