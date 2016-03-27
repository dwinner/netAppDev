/**
 * Преобразование данных XML в формат ADO.NET
 */

using System;
using System.Data;
using System.Text;

namespace XmlToAdoNet
{
   internal static class Program
   {
      private static void Main()
      {
         // Создание объекта DataSet
         var dataSet = new DataSet("XmlProducts");

         // Чтение документа XML
         dataSet.ReadXml("Products.xml");

         var builder = new StringBuilder();
         foreach (DataTable table in dataSet.Tables)
         {
            builder.Append(table.TableName).Append(Environment.NewLine);

            foreach (DataColumn column in table.Columns)
            {
               builder.AppendFormat("\t{0}-{1}{2}", column.ColumnName, column.DataType.FullName, Environment.NewLine);
            }
         }
      }
   }
}