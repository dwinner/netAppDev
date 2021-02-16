/**
 * Преобразование информации из базы данных в XML-документ
 */

using System;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DBToXML
{
   class Program
   {
      static void Main()
      {
         //var doc = new XmlDocument();
         var dataSet = new DataSet("Books");
         using (var sqlConnection = new SqlConnection(GetConnectionString()))
         using (var selectCommand = new SqlCommand("SELECT * FROM Books", sqlConnection))
         using (var sqlDataAdapter = new SqlDataAdapter(selectCommand))
         {
            sqlDataAdapter.Fill(dataSet);
         }

         var stringBuilder = new StringBuilder();
         using (var sw = new StringWriter(stringBuilder))
         {
            dataSet.WriteXml(sw);
         }

         Console.WriteLine(stringBuilder.ToString());
         Console.ReadKey();
      }

      static string GetConnectionString()
      {
         return ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;
      }
   }
}
