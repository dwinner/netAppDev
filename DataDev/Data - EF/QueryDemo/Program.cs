/**
 * Запросы к базе данных через сущностные объекты
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using Formula1Demo;

namespace QueryDemo
{
   internal class Program
   {
      private static void Main()
      {
         //LinqToEntities();
         //LinqToEntities2();

         //ObjectQuery();
         //ObjectQueryFiltering();

         //EntitySqlWithParameters();
         //EntitySqlDemo();
         //EntitySqlDemo2();

         Console.ReadLine();
      }

      #region Запросы через Linq To Entities

      private static void LinqToEntities()
      {
         using (var data = new Formula1v2Entities())
         {
            IOrderedQueryable<Racer> racers = from racer in data.Racers
                                              where racer.Wins > 40
                                              orderby racer.Wins descending
                                              select racer;
            foreach (Racer racer in racers)
            {
               Console.WriteLine("{0} {1}", racer.FirstName, racer.LastName);
            }
         }
      }

      private static void LinqToEntities2()
      {
         using (var entities = new Formula1v2Entities())
         {
            var query = from racer in entities.Racers
                        from raceResult in racer.RaceResults
                        where raceResult.Position <= 3 && raceResult.Position >= 1 && racer.Nationality == "Switzerland"
                        group racer by racer.Id
                           into g
                           let podium = g.Count()
                           orderby podium descending
                           select new
                           {
                              Racer = g.FirstOrDefault(),
                              Podiums = podium
                           };
            foreach (var q in query)
            {
               Console.WriteLine("{0} {1} {2}", q.Racer.FirstName, q.Racer.LastName, q.Podiums);
            }
         }
      }

      #endregion

      #region Объектные запросы

      private static void ObjectQuery()
      {
         const string country = "Brazil";
         using (var data = new Formula1v2Entities())
         {
            IQueryable<Racer> racers = data.Racers.Where(racer => racer.Nationality == country);
            foreach (Racer racer in racers)
            {
               Console.WriteLine(racer.FirstName);
            }
         }
      }

      private static void ObjectQueryFiltering()
      {
         const string country = "USA";

         using (var formula1 = new Formula1v2Entities())
         {
            IQueryable<Racer> racers = formula1.Racers.Where(racer => racer.Nationality == country)
               .OrderByDescending(racer => racer.Wins)
               .ThenByDescending(racer => racer.Starts)
               .Take(3);
            foreach (Racer racer in racers)
            {
               Console.WriteLine("{0} {1}, wins: {2}, starts: {3}", racer.FirstName, racer.LastName, racer.Wins,
                  racer.Starts);
            }
         }
      }

      #endregion

      #region Запросы низкого уровня через Entity SQL

      private static async void EntitySqlWithParameters()
      {
         string connectionString = ConfigurationManager.ConnectionStrings["Formula1v2Entities"].ConnectionString;
         using (var connection = new EntityConnection(connectionString))
         {
            await connection.OpenAsync();
            EntityCommand command = connection.CreateCommand();
            command.CommandText =
               "SELECT VALUE it FROM [Formula1v2Entities].[Racers] AS it WHERE it.Nationality = @Country";
            command.Parameters.AddWithValue("Country", "Austria");

            DbDataReader reader =
               await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection);
            while (await reader.ReadAsync())
            {
               Console.WriteLine("{0} {1}", reader["FirstName"], reader["LastName"]);
            }
            reader.Close();
         }
      }

      private static async void EntitySqlDemo()
      {
         string connectionString = ConfigurationManager.ConnectionStrings["Formula1v2Entities"].ConnectionString;
         using (var connection = new EntityConnection(connectionString))
         {
            await connection.OpenAsync();
            EntityCommand command = connection.CreateCommand();
            command.CommandText = "[Formula1v2Entities].[Racers]";
            DbDataReader reader =
               await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection);
            while (await reader.ReadAsync())
            {
               Console.WriteLine("{0} {1}", reader["FirstName"], reader["LastName"]);
            }
            reader.Close();
         }
      }

      private static async void EntitySqlDemo2()
      {
         string connectionString = ConfigurationManager.ConnectionStrings["Formula1v2Entities"].ConnectionString;
         var connection = new EntityConnection(connectionString);
         await connection.OpenAsync();
         EntityCommand command = connection.CreateCommand();
         command.CommandText = "SELECT Racers.FirstName, Racers.LastName FROM Formula1v2Entities.Racers";
         DbDataReader reader =
            await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection);
         while (await reader.ReadAsync())
         {
            Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
         }
         reader.Close();
      }

      #endregion
   }
}