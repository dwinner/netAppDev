using PrototypeFactory;
using static System.Console;

var main = new Person(null, new Address("123 East Dr", "London", 0));
var aux = new Person(null, new Address("123B East Dr", "London", 0));

var john = new Person("John", new Address("123 London Road", "London", 123));

//var chris = john;
var jane = new Person(john)
{
   Name = "Jane"
};

WriteLine(john); // oops, john is called chris
WriteLine(jane);