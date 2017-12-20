[DataContract, KnownType (typeof (Student)), KnownType (typeof (Teacher))]
public class Person
{
  [DataMember] public string Name;
  [DataMember] public int Age;
}

[DataContract] public class Student : Person { }
[DataContract] public class Teacher : Person { }

static Person DeepClone (Person p)
{
  DataContractSerializer ds = new DataContractSerializer (typeof (Person));
  MemoryStream stream = new MemoryStream();
  ds.WriteObject (stream, p);
  stream.Position = 0;
  return (Person) ds.ReadObject (stream);
}

Person  person  = new Person  { Name = "Stacey", Age = 30 };
Student student = new Student { Name = "Stacey", Age = 30 };
Teacher teacher = new Teacher { Name = "Stacey", Age = 30 };

Person  p2 =           DeepClone (person);
Student s2 = (Student) DeepClone (student);
Teacher t2 = (Teacher) DeepClone (teacher);

// Alternative to KnownType on [DataContract]:
DataContractSerializer ds = new DataContractSerializer (typeof (Person),
  new Type[]
  {
  	typeof (Student), typeof (Teacher)
  } );