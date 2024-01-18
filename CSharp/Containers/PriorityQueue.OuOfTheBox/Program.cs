var prioQ = new PriorityQueue<string, int>();

prioQ.Enqueue("three", 3);
prioQ.Enqueue("four", 4);
prioQ.Enqueue("two", 2);

Console.WriteLine(prioQ.Peek()); // 2 as with a minimum priority

prioQ.Enqueue("five", 5);
prioQ.Enqueue("one", 1);

Console.WriteLine(prioQ.Dequeue()); // 1 as with with a minimum priority

// 2,3,4,5 are left
foreach (var (element, priority) in prioQ.UnorderedItems)
{
   Console.WriteLine($"Element {element} with priority {priority}");
}