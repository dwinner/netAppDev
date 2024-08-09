using System.Reactive.Linq;
using GroupByOp;
using RxHelpers;
using Ex = RxHelpers.ObservableEx;

var people = Ex.FromValues(
   new Person { Gender = Gender.Male, Age = 21 },
   new Person { Gender = Gender.Female, Age = 31 },
   new Person { Gender = Gender.Male, Age = 23 },
   new Person { Gender = Gender.Female, Age = 33 });

var genderAge =
   from gender in people.GroupBy(p => p.Gender)
   from avg in gender.Average(p => p.Age)
   select new
   {
      Gender = gender.Key,
      AvgAge = avg
   };

genderAge.SubscribeConsole("Gender Age");

//
// Grouping with Query Syntax GroupBy clause
//
//var genderAge2 =
//    from p in people
//    group p by p.Gender
//    into gender
//    from avg in gender.Average(p => p.Age)
//    select new { Gender = gender.Key, AvgAge = avg };