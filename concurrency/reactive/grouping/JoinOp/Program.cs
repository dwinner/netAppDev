using System.Reactive.Linq;
using System.Reactive.Subjects;
using JoinOp;
using RxHelpers;

Demo.DisplayHeader("The Join operator - ...");

var doorOpenedSubject = new Subject<DoorOpened>();
var doorOpened = doorOpenedSubject.AsObservable();

var enterences = doorOpened.Where(o => o.Direction == OpenDirection.Entering);
var maleEntering = enterences.Where(x => x.Gender == Gender.Male);
var femaleEntering = enterences.Where(x => x.Gender == Gender.Female);

var exits = doorOpened.Where(o => o.Direction == OpenDirection.Leaving);
var maleExiting = exits.Where(x => x.Gender == Gender.Male);
var femaleExiting = exits.Where(x => x.Gender == Gender.Female);


//Using Method Chaining approach
maleEntering
   .Join(femaleEntering,
      male => maleExiting.Where(exit => exit.Name == male.Name),
      female => femaleExiting.Where(exit => female.Name == exit.Name),
      (m, f) => new { Male = m.Name, Female = f.Name })
   .SubscribeConsole("Together At Room");

//
//Using Query Syntax Join clause
//
var test =
   from male in maleEntering
   join female in femaleEntering
      on maleExiting.Where(exit => exit.Name == male.Name)
      equals femaleExiting.Where(exit => female.Name == exit.Name)
   select new
   {
      MName = male.Name,
      FName = female.Name
   };
test.SubscribeConsole("Together At Room (query syntax)");

//This is the sequence you see in Figure 9.8
doorOpenedSubject.OnNext(new DoorOpened("Bob", Gender.Male, OpenDirection.Entering));
doorOpenedSubject.OnNext(new DoorOpened("Sara", Gender.Female, OpenDirection.Entering));
doorOpenedSubject.OnNext(new DoorOpened("John", Gender.Male, OpenDirection.Entering));
doorOpenedSubject.OnNext(new DoorOpened("Sara", Gender.Female, OpenDirection.Leaving));
doorOpenedSubject.OnNext(new DoorOpened("Fibi", Gender.Female, OpenDirection.Entering));
doorOpenedSubject.OnNext(new DoorOpened("Bob", Gender.Male, OpenDirection.Leaving));
doorOpenedSubject.OnNext(new DoorOpened("Dan", Gender.Male, OpenDirection.Entering));
doorOpenedSubject.OnNext(new DoorOpened("Fibi", Gender.Female, OpenDirection.Leaving));
doorOpenedSubject.OnNext(new DoorOpened("John", Gender.Male, OpenDirection.Leaving));
doorOpenedSubject.OnNext(new DoorOpened("Dan", Gender.Male, OpenDirection.Leaving));