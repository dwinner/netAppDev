/**
 * Использование типа данных XML
 */


using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XmlDataTypeSample
{
   internal static class Program
   {
      private const string ConnectionString =
         @"Data Source=VINEVCEV-PC\BTRONIK;Initial Catalog=School;Integrated Security=True";

      private static void Main()
      {
         XmlReaderDemo();
         XmlDocumentDemo();
         EfDemo();
      }

      private static void XmlReaderDemo() // Чтение XML
      {
         using (var examConnection = new SqlConnection(ConnectionString))
         using (SqlCommand command = examConnection.CreateCommand())
         {
            command.CommandText = "SELECT Id, Number, Info FROM Exams";
            examConnection.Open();
            using (SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
               while (sqlDataReader.Read())
               {
                  SqlXml xml = sqlDataReader.GetSqlXml(2);
                  using (XmlReader xmlReader = xml.CreateReader())
                  {
                     var cources = new StringBuilder("Cource(s)", 40);
                     while (xmlReader.Read())
                     {
                        if (xmlReader.Name == "Exam" && xmlReader.IsStartElement())
                        {
                           Console.WriteLine("Exam: {0}", xmlReader.GetAttribute("Number"));
                        }
                        else if (xmlReader.Name == "Title" && xmlReader.IsStartElement())
                        {
                           Console.WriteLine("Title: {0}", xmlReader.ReadString());
                        }
                        else if (xmlReader.Name == "Course" && xmlReader.IsStartElement())
                        {
                           cources.AppendFormat("{0} ", xmlReader.ReadString());
                        }
                     }

                     Console.WriteLine(cources.ToString());
                  }
               }
            }
         }
      }

      private static void XmlDocumentDemo() // Запросы к узлам XML-документа
      {
         using (var examConnection = new SqlConnection(ConnectionString))
         using (SqlCommand command = examConnection.CreateCommand())
         {
            command.CommandText = "SELECT Id, Number, Info FROM Exams";
            examConnection.Open();
            using (SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
               while (sqlDataReader.Read())
               {
                  SqlXml xml = sqlDataReader.GetSqlXml(2);
                  var document = new XmlDocument();
                  document.LoadXml(xml.Value);
                  XmlNode examNode = document.SelectSingleNode("//Exam");
                  if (examNode != null && examNode.Attributes != null)
                  {
                     Console.WriteLine("Exam: {0}", examNode.Attributes["Number"].Value);
                     XmlNode titleNode = examNode.SelectSingleNode("./Title");
                     if (titleNode != null)
                     {
                        Console.WriteLine("Title: {0}", titleNode.InnerText);
                        Console.WriteLine("Course(s): ");
                        XmlNodeList courseNodeList = examNode.SelectNodes("./Course");
                        if (courseNodeList != null)
                        {
                           foreach (XmlNode courseNode in courseNodeList)
                           {
                              Console.WriteLine(courseNode.InnerText);
                           }
                        }
                        Console.WriteLine();
                     }
                  }
               }
            }
         }
      }

      private static void EfDemo() // Комбинирование LINQ To Entites с LINQ To XML
      {
         using (var schoolEntities = new SchoolEntities())
         {
            foreach (XElement exam in schoolEntities.Exams.Select(examItem => XElement.Parse(examItem.Info)))
            {
               Console.WriteLine("Exam: {0}", exam.Attribute("Number").Value);
               XElement titleEl = exam.Element("Title");
               if (titleEl != null)
               {
                  Console.WriteLine("Title: {0}", titleEl.Value);
                  Console.WriteLine("Course(s): ");
                  foreach (XElement course in exam.Elements("Course"))
                  {
                     Console.Write("{0} ", course.Value);
                  }
               }
            }
         }
      }
   }
}