using CopyConstructor;
using static System.Console;

var john = new Person("John", new Address("123 London Road", "London", "UK"));

//var chris = john;
var jane = new Person(john)
{
   Name = "Jane"
};

WriteLine(john); // oops, john is called chris
WriteLine(jane);