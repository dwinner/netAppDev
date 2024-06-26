﻿using MoreLinq;

string[] fullNames = { "Anne Williams", "John Fred Smith", "Sue Green" };

IEnumerable<string> query1 =
   from fullName in fullNames
   from x in fullName.Split().Select(name => new
   {
      name, 
      fullName
   })
   orderby x.fullName, x.name
   select $"{x.name} came from {x.fullName}";

query1.ForEach(name => Console.WriteLine(name));

var query2 = fullNames
   .SelectMany(fName => fName.Split().Select(name => new 
      { name, 
         fName
      }))
   .OrderBy(x => x.fName)
   .ThenBy(x => x.name)
   .Select(x => $"{x.name} came from {x.fName}");

query2.ForEach(name => Console.WriteLine(name));