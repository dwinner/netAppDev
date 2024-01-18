using ExtendingKeyedCollectionSample;

var zoo = new Zoo();
zoo.Animals.Add(new Animal("Kangaroo", 10));
zoo.Animals.Add(new Animal("Mr Sea Lion", 20));
Console.WriteLine(zoo.Animals[0].Popularity); // 10
Console.WriteLine(zoo.Animals["Mr Sea Lion"].Popularity); // 20
zoo.Animals["Kangaroo"].Name = "Mr Roo";
Console.WriteLine(zoo.Animals["Mr Roo"].Popularity); // 10