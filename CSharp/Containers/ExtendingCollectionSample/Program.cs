using ExtendingCollectionSample;

var zoo = new Zoo();
zoo.Animals.Add(new Animal("Kangaroo", 10));
zoo.Animals.Add(new Animal("Mr Sea Lion", 20));
foreach (var animal in zoo.Animals)
{
   Console.WriteLine(animal.Name);
}