using System.Reactive;
using System.Reactive.Linq;

var leftList = new List<string[]>
{
   new[] { "2013-01-01 02:00:00", "Batch1" },
   new[] { "2013-01-01 03:00:00", "Batch2" },
   new[] { "2013-01-01 04:00:00", "Batch3" }
};

var rightList = new List<string[]>
{
   new[] { "2013-01-01 01:00:00", "Production=2" },
   new[] { "2013-01-01 02:00:00", "Production=0" },
   new[] { "2013-01-01 03:00:00", "Production=3" }
};

var leftOb = leftList.ToObservable();
var rightOb = rightList.ToObservable();

var joinedOb = leftOb.GroupJoin(rightOb,
   _ => Observable.Never<Unit>(), // windows from each left event going on forever
   _ => Observable.Never<Unit>(), // windows from each right event going on forever
   Tuple.Create); // create tuple of left event with observable of right events

// e is a tuple with two items, left and obsOfRight
joinedOb.Subscribe(e =>
{
   var xs = e.Item2;
   xs.Where(x => x[0] == e.Item1[0]) // filter only when datetime matches
      .Subscribe(
         v =>
         {
            Console.WriteLine("{0},{1} and {2},{3} occur at the same time",
               e.Item1[0],
               e.Item1[1],
               v[0],
               v[1]);
         });
});