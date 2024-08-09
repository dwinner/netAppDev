using System.Reactive.Linq;
using System.Reactive.Subjects;
using GroupJoinOp;
using RxHelpers;

Demo.DisplayHeader(
   "The GroupJoin operator - correlates elements from two observables based on overlapping duration windows and put them in a correlation group");

var doorOpenedSubject = new Subject<DoorOpened>();
var doorOpened = doorOpenedSubject.AsObservable();

var enterences = doorOpened.Where(o => o.Direction == OpenDirection.Entering);
var maleEntering = enterences.Where(x => x.Gender == Gender.Male);
var femaleEntering = enterences.Where(x => x.Gender == Gender.Female);

var exits = doorOpened.Where(o => o.Direction == OpenDirection.Leaving);
var maleExiting = exits.Where(x => x.Gender == Gender.Male);
var femaleExiting = exits.Where(x => x.Gender == Gender.Female);

var malesAcquaintances =
   maleEntering
      .GroupJoin(femaleEntering,
         male => maleExiting.Where(exit => exit.Name == male.Name),
         female => femaleExiting.Where(exit => female.Name == exit.Name),
         (m, females) => new { Male = m.Name, Females = females });

var amountPerUser =
   from acquinteces in malesAcquaintances
   from cnt in acquinteces.Females.Scan(0, (acc, _) => acc + 1)
   select new { acquinteces.Male, cnt };

amountPerUser.SubscribeConsole("Amount of meetings per User");

//
// Using Query Syntax GroupJoin clause
//
var malesAcquaintances2 =
   from male in maleEntering
   join female in femaleEntering
      on maleExiting.Where(exit => exit.Name == male.Name)
      equals femaleExiting.Where(exit => female.Name == exit.Name)
      into females
   select new
   {
      Male = male.Name,
      Females = females
   };
var gJoin =
   from acquinteces in malesAcquaintances2
   from cnt in acquinteces.Females.Scan(0, (acc, _) => acc + 1)
   select new
   {
      acquinteces.Male,
      cnt
   };
gJoin.SubscribeConsole(string.Empty);

//amountPerUser2.SubscribeConsole("Amount of meetings per User (query syntax)");

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