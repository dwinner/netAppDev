using System.Collections.Generic;

namespace DataLib
{
   public static class Formula1
   {
      private static List<Racer> _racers;

      public static IEnumerable<Racer> Champions
      {
         get
         {
            return _racers ?? (_racers = new List<Racer>(40)
            {
               new Racer("Nino", "Farina", "Italy", 33, 5, new[] {1950}, new[] {"Alfa Romeo"}),
               new Racer("Alberto", "Ascari", "Italy", 32, 10, new[] {1952, 1953}, new[] {"Ferrari"}),
               new Racer("Juan Manuel", "Fangio", "Argentina", 51, 24, new[] {1951, 1954, 1955, 1956, 1957},
                  new[] {"Alfa Romeo", "Maserati", "Mercedes", "Ferrari"}),
               new Racer("Mike", "Hawthorn", "UK", 45, 3, new[] {1958}, new[] {"Ferrari"}),
               new Racer("Phil", "Hill", "USA", 48, 3, new[] {1961}, new[] {"Ferrari"}),
               new Racer("John", "Surtees", "UK", 111, 6, new[] {1964}, new[] {"Ferrari"}),
               new Racer("Jim", "Clark", "UK", 72, 25, new[] {1963, 1965}, new[] {"Lotus"}),
               new Racer("Jack", "Brabham", "Australia", 125, 14, new[] {1959, 1960, 1966},
                  new[] {"Cooper", "Brabham"}),
               new Racer("Denny", "Hulme", "New Zealand", 112, 8, new[] {1967}, new[] {"Brabham"}),
               new Racer("Graham", "Hill", "UK", 176, 14, new[] {1962, 1968}, new[] {"BRM", "Lotus"}),
               new Racer("Jochen", "Rindt", "Austria", 60, 6, new[] {1970}, new[] {"Lotus"}),
               new Racer("Jackie", "Stewart", "UK", 99, 27, new[] {1969, 1971, 1973},
                  new[] {"Matra", "Tyrrell"}),
               new Racer("Emerson", "Fittipaldi", "Brazil", 143, 14, new[] {1972, 1974},
                  new[] {"Lotus", "McLaren"}),
               new Racer("James", "Hunt", "UK", 91, 10, new[] {1976}, new[] {"McLaren"}),
               new Racer("Mario", "Andretti", "USA", 128, 12, new[] {1978}, new[] {"Lotus"}),
               new Racer("Jody", "Scheckter", "South Africa", 112, 10, new[] {1979}, new[] {"Ferrari"}),
               new Racer("Alan", "Jones", "Australia", 115, 12, new[] {1980}, new[] {"Williams"}),
               new Racer("Keke", "Rosberg", "Finland", 114, 5, new[] {1982}, new[] {"Williams"}),
               new Racer("Niki", "Lauda", "Austria", 173, 25, new[] {1975, 1977, 1984},
                  new[] {"Ferrari", "McLaren"}),
               new Racer("Nelson", "Piquet", "Brazil", 204, 23, new[] {1981, 1983, 1987},
                  new[] {"Brabham", "Williams"}),
               new Racer("Ayrton", "Senna", "Brazil", 161, 41, new[] {1988, 1990, 1991}, new[] {"McLaren"}),
               new Racer("Nigel", "Mansell", "UK", 187, 31, new[] {1992}, new[] {"Williams"}),
               new Racer("Alain", "Prost", "France", 197, 51, new[] {1985, 1986, 1989, 1993},
                  new[] {"McLaren", "Williams"}),
               new Racer("Damon", "Hill", "UK", 114, 22, new[] {1996}, new[] {"Williams"}),
               new Racer("Jacques", "Villeneuve", "Canada", 165, 11, new[] {1997}, new[] {"Williams"}),
               new Racer("Mika", "Hakkinen", "Finland", 160, 20, new[] {1998, 1999}, new[] {"McLaren"}),
               new Racer("Michael", "Schumacher", "Germany", 287, 91,
                  new[] {1994, 1995, 2000, 2001, 2002, 2003, 2004}, new[] {"Benetton", "Ferrari"}),
               new Racer("Fernando", "Alonso", "Spain", 177, 27, new[] {2005, 2006}, new[] {"Renault"}),
               new Racer("Kimi", "Räikkönen", "Finland", 148, 17, new[] {2007}, new[] {"Ferrari"}),
               new Racer("Lewis", "Hamilton", "UK", 90, 17, new[] {2008}, new[] {"McLaren"}),
               new Racer("Jenson", "Button", "UK", 208, 12, new[] {2009}, new[] {"Brawn GP"}),
               new Racer("Sebastian", "Vettel", "Germany", 81, 21, new[] {2010, 2011},
                  new[] {"Red Bull Racing"})
            });
         }
      }

      private static List<Team> _teams;

      public static IEnumerable<Team> ConstructorChampions
      {
         get
         {
            return _teams ?? (_teams = new List<Team>
            {
               new Team("Vanwall", 1958),
               new Team("Cooper", 1959, 1960),
               new Team("Ferrari", 1961, 1964, 1975, 1976, 1977, 1979, 1982, 1983, 1999, 2000, 2001, 2002, 2003, 2004, 2007,
                  2008),
               new Team("BRM", 1962),
               new Team("Lotus", 1963, 1965, 1968, 1970, 1972, 1973, 1978),
               new Team("Brabham", 1966, 1967),
               new Team("Matra", 1969),
               new Team("Tyrrell", 1971),
               new Team("McLaren", 1974, 1984, 1985, 1988, 1989, 1990, 1991, 1998),
               new Team("Williams", 1980, 1981, 1986, 1987, 1992, 1993, 1994, 1996, 1997),
               new Team("Benetton", 1995),
               new Team("Renault", 2005, 2006),
               new Team("Brawn GP", 2009),
               new Team("Red Bull Racing", 2010, 2011)
            });
         }
      }

      private static List<Championship> _championships;

      public static IEnumerable<Championship> Championships
      {
         get
         {
            return _championships ?? (_championships = new List<Championship>
            {
               new Championship
               {
                  Year = 1950,
                  First = "Nino Farina",
                  Second = "Juan Manuel Fangio",
                  Third = "Luigi Fagioli"
               },
               new Championship
               {
                  Year = 1951,
                  First = "Juan Manuel Fangio",
                  Second = "Alberto Ascari",
                  Third = "Froilan Gonzalez"
               },
               new Championship
               {
                  Year = 1952,
                  First = "Alberto Ascari",
                  Second = "Nino Farina",
                  Third = "Piero Taruffi"
               },
               new Championship
               {
                  Year = 1953,
                  First = "Alberto Ascari",
                  Second = "Juan Manuel Fangio",
                  Third = "Nino Farina"
               },
               new Championship
               {
                  Year = 1954,
                  First = "Juan Manuel Fangio",
                  Second = "Froilan Gonzalez",
                  Third = "Mike Hawthorn"
               },
               new Championship
               {
                  Year = 1955,
                  First = "Juan Manuel Fangio",
                  Second = "Stirling Moss",
                  Third = "Eugenio Castellotti"
               },
               new Championship
               {
                  Year = 1956,
                  First = "Juan Manuel Fangio",
                  Second = "Stirling Moss",
                  Third = "Peter Collins"
               },
               new Championship
               {
                  Year = 1957,
                  First = "Juan Manuel Fangio",
                  Second = "Stirling Moss",
                  Third = "Luigi Musso"
               },
               new Championship
               {
                  Year = 1958,
                  First = "Mike Hawthorn",
                  Second = "Stirling Moss",
                  Third = "Tony Brooks"
               },
               new Championship
               {
                  Year = 1959,
                  First = "Jack Brabham",
                  Second = "Tony Brooks",
                  Third = "Stirling Moss"
               },
               new Championship
               {
                  Year = 1960,
                  First = "Jack Brabham",
                  Second = "Bruce McLaren",
                  Third = "Stirling Moss"
               },
               new Championship
               {
                  Year = 1961,
                  First = "Phil Hill",
                  Second = "Wolfgang von Trips",
                  Third = "Stirling Moss"
               },
               new Championship
               {
                  Year = 1962,
                  First = "Graham Hill",
                  Second = "Jim Clark",
                  Third = "Bruce McLaren"
               },
               new Championship
               {
                  Year = 1963,
                  First = "Jim Clark",
                  Second = "Graham Hill",
                  Third = "Richie Ginther"
               },
               new Championship
               {
                  Year = 1964,
                  First = "John Surtees",
                  Second = "Graham Hill",
                  Third = "Jim Clark"
               },
               new Championship
               {
                  Year = 1965,
                  First = "Jim Clark",
                  Second = "Graham Hill",
                  Third = "Jackie Stewart"
               },
               new Championship
               {
                  Year = 1966,
                  First = "Jack Brabham",
                  Second = "John Surtees",
                  Third = "Jochen Rindt"
               },
               new Championship
               {
                  Year = 1967,
                  First = "Dennis Hulme",
                  Second = "Jack Brabham",
                  Third = "Jim Clark"
               },
               new Championship
               {
                  Year = 1968,
                  First = "Graham Hill",
                  Second = "Jackie Stewart",
                  Third = "Dennis Hulme"
               },
               new Championship
               {
                  Year = 1969,
                  First = "Jackie Stewart",
                  Second = "Jackie Ickx",
                  Third = "Bruce McLaren"
               },
               new Championship
               {
                  Year = 1970,
                  First = "Jochen Rindt",
                  Second = "Jackie Ickx",
                  Third = "Clay Regazzoni"
               },
               new Championship
               {
                  Year = 1971,
                  First = "Jackie Stewart",
                  Second = "Ronnie Peterson",
                  Third = "Francois Cevert"
               },
               new Championship
               {
                  Year = 1972,
                  First = "Emerson Fittipaldi",
                  Second = "Jackie Stewart",
                  Third = "Dennis Hulme"
               },
               new Championship
               {
                  Year = 1973,
                  First = "Jackie Stewart",
                  Second = "Emerson Fittipaldi",
                  Third = "Ronnie Peterson"
               },
               new Championship
               {
                  Year = 1974,
                  First = "Emerson Fittipaldi",
                  Second = "Clay Regazzoni",
                  Third = "Jody Scheckter"
               },
               new Championship
               {
                  Year = 1975,
                  First = "Niki Lauda",
                  Second = "Emerson Fittipaldi",
                  Third = "Carlos Reutemann"
               },
               new Championship
               {
                  Year = 1976,
                  First = "James Hunt",
                  Second = "Niki Lauda",
                  Third = "Jody Scheckter"
               },
               new Championship
               {
                  Year = 1977,
                  First = "Niki Lauda",
                  Second = "Jody Scheckter",
                  Third = "Mario Andretti"
               },
               new Championship
               {
                  Year = 1978,
                  First = "Mario Andretti",
                  Second = "Ronnie Peterson",
                  Third = "Carlos Reutemann"
               },
               new Championship
               {
                  Year = 1979,
                  First = "Jody Scheckter",
                  Second = "Gilles Villeneuve",
                  Third = "Alan Jones"
               },
               new Championship
               {
                  Year = 1980,
                  First = "Alan Jones",
                  Second = "Nelson Piquet",
                  Third = "Carlos Reutemann"
               },
               new Championship
               {
                  Year = 1981,
                  First = "Nelson Piquet",
                  Second = "Carlos Reutemann",
                  Third = "Alan Jones"
               },
               new Championship
               {
                  Year = 1982,
                  First = "Keke Rosberg",
                  Second = "Didier Pironi",
                  Third = "John Watson"
               },
               new Championship
               {
                  Year = 1983,
                  First = "Nelson Piquet",
                  Second = "Alain Prost",
                  Third = "Rene Arnoux"
               },
               new Championship
               {
                  Year = 1984,
                  First = "Niki Lauda",
                  Second = "Alain Prost",
                  Third = "Elio de Angelis"
               },
               new Championship
               {
                  Year = 1985,
                  First = "Alain Prost",
                  Second = "Michele Alboreto",
                  Third = "Keke Rosberg"
               },
               new Championship
               {
                  Year = 1986,
                  First = "Alain Prost",
                  Second = "Nigel Mansell",
                  Third = "Nelson Piquet"
               },
               new Championship
               {
                  Year = 1987,
                  First = "Nelson Piquet",
                  Second = "Nigel Mansell",
                  Third = "Ayrton Senna"
               },
               new Championship
               {
                  Year = 1988,
                  First = "Ayrton Senna",
                  Second = "Alain Prost",
                  Third = "Gerhard Berger"
               },
               new Championship
               {
                  Year = 1989,
                  First = "Alain Prost",
                  Second = "Ayrton Senna",
                  Third = "Riccardo Patrese"
               },
               new Championship
               {
                  Year = 1990,
                  First = "Ayrton Senna",
                  Second = "Alain Prost",
                  Third = "Nelson Piquet"
               },
               new Championship
               {
                  Year = 1991,
                  First = "Ayrton Senna",
                  Second = "Nigel Mansell",
                  Third = "Riccardo Patrese"
               },
               new Championship
               {
                  Year = 1992,
                  First = "Nigel Mansell",
                  Second = "Riccardo Patrese",
                  Third = "Michael Schumacher"
               },
               new Championship
               {
                  Year = 1993,
                  First = "Alain Prost",
                  Second = "Ayrton Senna",
                  Third = "Damon Hill"
               },
               new Championship
               {
                  Year = 1994,
                  First = "Michael Schumacher",
                  Second = "Damon Hill",
                  Third = "Gerhard Berger"
               },
               new Championship
               {
                  Year = 1995,
                  First = "Michael Schumacher",
                  Second = "Damon Hill",
                  Third = "David Coulthard"
               },
               new Championship
               {
                  Year = 1996,
                  First = "Damon Hill",
                  Second = "Jacques Villeneuve",
                  Third = "Michael Schumacher"
               },
               new Championship
               {
                  Year = 1997,
                  First = "Jacques Villeneuve",
                  Second = "Heinz-Harald Frentzen",
                  Third = "David Coulthard"
               },
               new Championship
               {
                  Year = 1998,
                  First = "Mika Hakkinen",
                  Second = "Michael Schumacher",
                  Third = "David Coulthard"
               },
               new Championship
               {
                  Year = 1999,
                  First = "Mika Hakkinen",
                  Second = "Eddie Irvine",
                  Third = "Heinz-Harald Frentzen"
               },
               new Championship
               {
                  Year = 2000,
                  First = "Michael Schumacher",
                  Second = "Mika Hakkinen",
                  Third = "David Coulthard"
               },
               new Championship
               {
                  Year = 2001,
                  First = "Michael Schumacher",
                  Second = "David Coulthard",
                  Third = "Rubens Barrichello"
               },
               new Championship
               {
                  Year = 2002,
                  First = "Michael Schumacher",
                  Second = "Rubens Barrichello",
                  Third = "Juan Pablo Montoya"
               },
               new Championship
               {
                  Year = 2003,
                  First = "Michael Schumacher",
                  Second = "Kimi Räikkönen",
                  Third = "Juan Pablo Montoya"
               },
               new Championship
               {
                  Year = 2004,
                  First = "Michael Schumacher",
                  Second = "Rubens Barrichello",
                  Third = "Jenson Button"
               },
               new Championship
               {
                  Year = 2005,
                  First = "Fernando Alonso",
                  Second = "Kimi Räikkönen",
                  Third = "Michael Schumacher"
               },
               new Championship
               {
                  Year = 2006,
                  First = "Fernando Alonso",
                  Second = "Michael Schumacher",
                  Third = "Felipe Massa"
               },
               new Championship
               {
                  Year = 2007,
                  First = "Kimi Räikkönen",
                  Second = "Lewis Hamilton",
                  Third = "Fernando Alonso"
               },
               new Championship
               {
                  Year = 2008,
                  First = "Lewis Hamilton",
                  Second = "Felipe Massa",
                  Third = "Kimi Raikkonen"
               },
               new Championship
               {
                  Year = 2009,
                  First = "Jenson Button",
                  Second = "Sebastian Vettel",
                  Third = "Rubens Barrichello"
               },
               new Championship
               {
                  Year = 2010,
                  First = "Sebastian Vettel",
                  Second = "Fernando Alonso",
                  Third = "Mark Webber"
               },
               new Championship
               {
                  Year = 2011,
                  First = "Sebastian Vettel",
                  Second = "Jenson Button",
                  Third = "Mark Webber"
               }
            });
         }
      }

      private static IList<Racer> _moreRacers;

      public static IList<Racer> MoreRacers
      {
         get
         {
            return _moreRacers ?? (_moreRacers = new List<Racer>
            {
               new Racer("Luigi", "Fagioli", "Italy", 7, 1),
               new Racer("Jose Froilan", "Gonzalez", "Argentina", 26, 2),
               new Racer("Piero", "Taruffi", "Italy", 18, 1),
               new Racer("Stirling", "Moss", "UK", 66, 16),
               new Racer("Eugenio", "Castellotti", "Italy", 14, 0),
               new Racer("Peter", "Collins", "UK", 32, 3),
               new Racer("Luigi", "Musso", "Italy", 24, 1),
               new Racer("Tony", "Brooks", "UK", 38, 6),
               new Racer("Bruce", "McLaren", "New Zealand", 100, 4),
               new Racer("Wolfgang von", "Trips", "Germany", 27, 2),
               new Racer("Richie", "Ginther", "USA", 52, 1),
               new Racer("Jackie", "Ickx", "Belgium", 116, 8),
               new Racer("Clay", "Regazzoni", "Switzerland", 132, 5),
               new Racer("Ronnie", "Peterson", "Sweden", 123, 10),
               new Racer("Francois", "Cevert", "France", 46, 1),
               new Racer("Carlos", "Reutemann", "Argentina", 146, 12),
               new Racer("Gilles", "Villeneuve", "Canada", 67, 6),
               new Racer("Didier", "Pironi", "France", 70, 3),
               new Racer("John", "Watson", "UK", 152, 5),
               new Racer("Rene", "Arnoux", "France", 149, 7),
               new Racer("Elio", "de Angelis", "Italy", 108, 2),
               new Racer("Michele", "Alboreto", "Italy", 194, 5),
               new Racer("Gerhard", "Berger", "Austria", 210, 10),
               new Racer("Riccardo", "Patrese", "Italy", 256, 6),
               new Racer("David", "Coulthard", "UK", 246, 13),
               new Racer("Heinz-Harald", "Frentzen", "Germany", 156, 3),
               new Racer("Eddie", "Irvine", "UK", 147, 4),
               new Racer("Rubens", "Barrichello", "Brazil", 322, 11),
               new Racer("Juan Pablo", "Montoya", "Columbia", 94, 7),
               new Racer("Felipe", "Massa", "Brazil", 152, 11),
               new Racer("Mark", "Webber", "Australia", 176, 7)
            });
         }
      }
   }
}
