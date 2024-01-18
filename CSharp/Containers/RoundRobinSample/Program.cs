using RoundRobin;

var roundRobinList = new RoundRobinList<int>(
   new List<int>
   {
      1, 2, 3, 4, 5
   }
);

//the weight of the element 1 will be increase by 2 units
roundRobinList.IncreaseWeight(1, 2);

for (var i = 0; i < 10; i++)
{
   Console.WriteLine($"{roundRobinList.Next()},");
}