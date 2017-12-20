[Serializable] public sealed class Person
{
  public string Name;
  public int Age;
}

// Example
Person p = new Person() { Name = "George", Age = 25 };

IFormatter formatter = new BinaryFormatter();

using (FileStream s = File.Create ("serialized.bin"))
  formatter.Serialize (s, p);

using (FileStream s = File.OpenRead ("serialized.bin"))
{
  Person p2 = (Person) formatter.Deserialize (s);
  Console.WriteLine (p2.Name + " " + p.Age);     // George 25
}

// [NonSerialized]:
[Serializable] public sealed class Person
{
  public string Name;
  public DateTime DateOfBirth;

  // Age can be calculated, so there's no need to serialize it.
  [NonSerialized] public int Age;  
}

// [OnDeserializing] and [OnDeserialized]
public sealed class Person
{
  public string Name;
  public DateTime DateOfBirth;

  [NonSerialized] public int Age;
  [NonSerialized] public bool Valid = true;

  public Person() { Valid = true; }
}

// [OnSerializing] and [OnSerialized]
[Serializable] public sealed class Team
{
  public string Name;  
  Person[] _playersToSerialize;

  [NonSerialized] public List<Person> Players = new List<Person>();

  [OnSerializing]
  void OnSerializing (StreamingContext context)
  {
    _playersToSerialize = Players.ToArray();
  }

  [OnSerialized]
  void OnSerialized (StreamingContext context)
  {
    _playersToSerialize = null;   // Allow it to be freed from memory
  }

  [OnDeserialized]
  void OnDeserialized (StreamingContext context)
  {
    Players = new List<Person> (_playersToSerialize);
  }
}

// [OptionalField] and versioning:
[Serializable] public sealed class Person       // Version 2 Robust
{
  public string Name;
  [OptionalField (VersionAdded = 2)] public DateTime DateOfBirth;
}

// ISerializable
[Serializable] public class Team : ISerializable
{
  public string Name;
  public List<Person> Players;

  public virtual void GetObjectData (SerializationInfo si,
                                     StreamingContext sc)
  {
    si.AddValue ("Name", Name);
    si.AddValue ("PlayerData", Players.ToArray());
  }

  public Team() {}
  
  protected Team (SerializationInfo si, StreamingContext sc)
  {
    Name = si.GetString ("Name");

    // Deserialize Players to an array to match our serialization:
    Person[] a = (Person[]) si.GetValue ("PlayerData", typeof (Person[]));

    // Construct a new List using this array:
    Players = new List<Person> (a);
  }
}