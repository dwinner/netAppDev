/**
 * Списки
 */

using System;
using System.Collections.Generic;

namespace ListSamples
{
   internal static class Program
   {
      static void Main()
      {
         #region Создание списков

         var graham = new Racer(7, "Graham", "Hill", "UK", 14);
         var emerson = new Racer(13, "Emerson", "Fittipaldi", "Brazil", 14);
         var mario = new Racer(16, "Mario", "Andretti", "USA", 12);

         var racers = new List<Racer>(20)
         {
            graham,
            emerson,
            mario,
            new Racer(24, "Michael", "Schumacher", "Germany", 91),
            new Racer(27, "Mika", "Hakkinen", "Finland", 20)
         };

         racers.AddRange(
            new[]
            {
               new Racer(14, "Niki", "Lauda", "Austria", 25),
               new Racer(21, "Alain", "Prost", "France", 51)
            });

         var racers2 = new List<Racer>(
            new[]
            {
               new Racer(12, "Jochen", "Rindt", "Austria", 6),
               new Racer(22, "Ayrton", "Senna", "Brazil", 41)
            });

         #endregion

         #region Обход списков

         racers2.ForEach(Console.WriteLine);
         racers.ForEach(racer => Console.WriteLine("{0:A}", racer));

         #endregion

         #region Поиск

         int finIndex = racers.FindIndex(new FindCountry("Finland").FindCountryPredicate);
         Console.WriteLine(racers[finIndex]);

         int findIndex = racers.FindIndex(racer => racer.Country == "Finland");
         Console.WriteLine(racers[findIndex]);

         Racer nikiRacer = racers.Find(racer => racer.FirstName == "Niki");
         Console.WriteLine(nikiRacer);

         List<Racer> bigWinners = racers.FindAll(racer => racer.Wins > 20);
         bigWinners.ForEach(racer => Console.WriteLine("{0:A}", racer));

         #endregion

         #region Сортировка

         racers.Sort();
         racers.ForEach(Console.WriteLine);

         racers.Sort(new RacerComparer(RacerComparer.CompareType.Country));
         racers.ForEach(Console.WriteLine);

         racers.Sort((first, second) => first.Wins.CompareTo(second.Wins));
         racers.ForEach(Console.WriteLine);

         #endregion

         #region Преобразование типов

         List<Person> persons = racers.ConvertAll(racer => new Person(string.Format("{0} {1}", racer.FirstName, racer.LastName)));
         persons.ForEach(Console.WriteLine);

         #endregion

         #region Коллекции, доступные только для чтения

         IList<Racer> readonlyRacers = racers.AsReadOnly();
         try
         {
            readonlyRacers.RemoveAt(0);
         }
         catch (NotSupportedException notSupportedEx)
         {
            Console.WriteLine(notSupportedEx);
         }

         #endregion

         Console.ReadKey();
      }
   }
}
