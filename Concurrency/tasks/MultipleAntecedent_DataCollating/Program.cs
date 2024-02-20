// task1 and task2 would call complex functions in real life:

var task1 = Task.Factory.StartNew(() => 123);
var task2 = Task.Factory.StartNew(() => 456);

var task3 = Task<int>.Factory.ContinueWhenAll(
   new[] { task1, task2 }, tasks => tasks.Sum(t => t.Result));

Console.WriteLine(task3.Result); // 579