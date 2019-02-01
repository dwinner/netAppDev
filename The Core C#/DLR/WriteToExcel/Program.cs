/**
 * Запись текста в Excel
 */

using System;
using System.Reflection;

namespace WriteToExcel
{
   static class Program
   {
      static void Main()
      {
         //WriteToExcelViaReflection();
         WriteToExcelViaDlr();
         Console.ReadKey();
      }

      public static void WriteToExcelViaReflection()  // Запись в Excel через отражение
      {
         // Создать экземпляр Excel
         Type xlAppType = Type.GetTypeFromProgID("Excel.Application");
         if (xlAppType == null)
            return;
         object xlInstance = Activator.CreateInstance(xlAppType);
         // Сделать Excel видимым
         xlInstance.GetType()
            .InvokeMember("Visible", BindingFlags.GetProperty, null, xlInstance, new object[] { true });
         // Создать новую рабочую книгу
         object workbooks = xlInstance.GetType()
            .InvokeMember("Workbooks", BindingFlags.GetProperty, null, xlInstance, null);
         workbooks.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, workbooks, new object[] { -4167 });
         // Установить значение первой ячейки
         object cell = xlInstance.GetType()
            .InvokeMember("Cells", BindingFlags.GetProperty, null, xlInstance, new object[] { 1, 1 });
         cell.GetType().InvokeMember("Value2", BindingFlags.SetProperty, null, cell, new object[] { "C# Rocks" });
      }

      public static void WriteToExcelViaDlr()   // Запись в Excel через DLR
      {
         // Создать экземпляр Excel
         Type xlAppType = Type.GetTypeFromProgID("Excel.Application");
         if (xlAppType == null)
            return;
         dynamic xl = Activator.CreateInstance(xlAppType);

         // Сделать Excel видимым
         xl.Visible = true;

         // Создать новую рабочую книгу
         dynamic workbooks = xl.Workbooks;
         workbooks.Add(-4167);

         // Установить значение новой ячейки
         xl.Cells[1, 1].Value2 = "C# Rocks!";
      }
   }
}
