// Copyright (C) 2011 Oliver Sturm <oliver@oliversturm.com>
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, see <http://www.gnu.org/licenses/>.
  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FCSlib;
using ManglerDemo;
using System.Threading.Tasks;

namespace LINQ {
  class Program {
    static void Main(string[] args) {
      VerySimpleLinqQuery( );
      VerySimpleLinqQueryWithoutSyntacticSugar( );
      VerySimpleQueryWithFCSlib( );
      QueryingPersonObjects( );
      ComplexLinqQueryWithGrouping( );
      ComplexLinqQueryWithJoining( );

      ManglerTesterCompiled.ShowIt( );
      ManglerTesterExpressions.ShowIt( );

      ParallelLINQ( );
    }

    static void VerySimpleLinqQuery( ) {
      var values = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
      var valuesGreater5 =
        from v in values
        where v > 5
        select v;
    }

    static void VerySimpleLinqQueryWithoutSyntacticSugar( ) {
      var values = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
      var valuesGreater5 =
        values.Where(v => v > 5).Select(v => v);
    }

    static void VerySimpleQueryWithFCSlib( ) {
      var values = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
      var valuesGreater5 =
        Functional.Filter(v => v > 5, values);
    }

    static void QueryingPersonObjects( ) {
      var people = new[] {
        new Person() { Name = "Harry", Age = 22 },
        new Person() { Name = "Jodie", Age = 35 },
        new Person() { Name = "William", Age = 56 },
        new Person() { Name = "Susan", Age = 41 }
      };

      var agesGreater40 =
        from p in people
        where p.Age > 40
        select p.Age;

      // or with Extension Methods
      var agesGreater40_2 =
        people.Where(p => p.Age > 40).Select(p => p.Age);

      // or with Functional
      var agesGreater40_3 =
        Functional.Map(p => p.Age, Functional.Filter(p => p.Age > 40, people));
    }

    static void ComplexLinqQueryWithGrouping( ) {
      var list = new CountryInfoList( );

      var groupList =
        from info in list
        orderby info.Population
        group info by info.Population / 1000000
          into g
          select new {
            Group = g,
            PopulationGroup = g.Key,
            MaxPopulation = g.Max(info => info.Population)
          };

      var groupList_2 =
        list.
        OrderBy(i => i.Population).
        GroupBy(i => i.Population / 1000000).
        Select(g => new {
          Group = g,
          PopulationGroup = g.Key,
          MaxPopulation = g.Max(info => info.Population)
        });
    }

    static void ComplexLinqQueryWithJoining( ) {
      var countries = new CountryInfoList( );
      var people = new CountryPerson[] {
								new CountryPerson {Name = "Bert Bott", Country="Brazil"},
								new CountryPerson {Name = "Jill Jones", Country = "Cameroon"}
							};

      var joinedList =
        from person in people
        join country in countries
        on person.Country equals country.Name
        select new {
          Name = person.Name,
          Country = person.Country,
          Population = country.Population
        };

      var joinedList_2 =
        people.Join(countries, p => p.Country, c => c.Name,
          (p, c) => new {
            Name = p.Name,
            Country = p.Country,
            Population = c.Population
          });
    }

    static void ParallelLINQ( ) {
      Console.WriteLine(GetValues( ).AsParallel( ).Sum( ));
    }

    static IEnumerable<int> GetValues( ) {
      int i = 0;
      while (i < 20) {
        Console.WriteLine("({0}) Returning {1}", Task.CurrentId, i);
        yield return i++;
      }
    }
  }
}
