using JsonMetadata;

var person = new Person
{
   FirstName = "Jane",
   LastName = "Doe",
   SocialSecurityNumber = "12345abcd"
};

Console.WriteLine(Serializer.SerializeToJson(person));