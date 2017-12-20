public class Person
{
  public string Name;
  public int Age;
}

// Example
p.Name = "Stacey"; p.Age = 30;

XmlSerializer xs = new XmlSerializer (typeof (Person));

using (Stream s = File.Create ("person.xml"))
  xs.Serialize (s, p);

Person p2;
using (Stream s = File.OpenRead ("person.xml"))
  p2 = (Person) xs.Deserialize (s);

Console.WriteLine (p2.Name + " " + p2.Age);   // Stacey 30

// Attributes, names and namespaces:
public class Person
{
  [XmlElement ("FirstName")] public string Name;
  [XmlAttribute ("RoughAge")] public int Age;
}

// XML element order:
public class Person
{
  [XmlElement (Order = 2)] public string Name;
  [XmlElement (Order = 1)] public int Age;
}

// XmlSerializer and subclassing:
[XmlInclude (typeof (Student))]
[XmlInclude (typeof (Teacher))]
public class Person { public string Name; }

public class Student : Person { }

public class Teacher : Person { }

public void SerializePerson (Person p, string path)
{
  XmlSerializer xs = new XmlSerializer (typeof (Person));
  using (Stream s = File.Create (path))
    xs.Serialize (s, p);
}

// Alternative
XmlSerializer xs = new XmlSerializer (typeof (Person),
                     new Type[] { typeof (Student), typeof (Teacher) } );

// Serializing child objects:
public class Person
{
  public string Name;
  public Address HomeAddress = new Address();
}

public class Address { public string Street, PostCode; }

Person p = new Person(); p.Name = "Stacey";
p.HomeAddress.Street = "Odo St";
p.HomeAddress.PostCode = "6020";

// Subclassed child objects, solution 1:
[XmlInclude (typeof (AUAddress))]
[XmlInclude (typeof (USAddress))]
public class Address { public string Street, PostCode; }

public class USAddress : Address {  }

public class AUAddress : Address {  }

public class Person
{
  public string Name;
  public Address HomeAddress = new USAddress();
}

// Subclassed child objects, solution 2:
public class Address { public string Street, PostCode; }

public class USAddress : Address {  }
public class AUAddress : Address {  }

public class Person
{
  public string Name;

  [XmlElement ("Address", typeof (Address))]
  [XmlElement ("AUAddress", typeof (AUAddress))]
  [XmlElement ("USAddress", typeof (USAddress))]
  public Address HomeAddress = new USAddress();
}

// Serializing collections with the outer element:
public class Person
{
  public string Name;

  [XmlArray ("PreviousAddresses")]
  [XmlArrayItem ("Location")]
  public List<Address> Addresses = new List<Address>();
}

// Serializing collections without the outer element:
public class Person
{
  public string Name;

  [XmlElement ("Address")]
  public List<Address> Addresses = new List<Address>();
}

// Subclassing collection elements with the outer element:
[XmlArrayItem ("Address",   typeof (Address))]
[XmlArrayItem ("AUAddress", typeof (AUAddress))]
[XmlArrayItem ("USAddress", typeof (USAddress))]
public List<Address> Addresses = new List<Address>();

// Subclassing collection elements without the outer element:
[XmlElement ("Address",   typeof (Address))]
[XmlElement ("AUAddress", typeof (AUAddress))]
[XmlElement ("USAddress", typeof (USAddress))]
public List<Address> Addresses = new List<Address>();

// IXmlSerializable:
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class Address : IXmlSerializable
{
  public string Street, PostCode;

  public XmlSchema GetSchema() { return null; }

  public void ReadXml(XmlReader reader)
  {
    reader.ReadStartElement();
    Street   = reader.ReadElementContentAsString ("Street", "");
    PostCode = reader.ReadElementContentAsString ("PostCode", "");
    reader.ReadEndElement();
  }

  public void WriteXml (XmlWriter writer)
  {
    writer.WriteElementString ("Street", Street);
    writer.WriteElementString ("PostCode", PostCode);
  }
}