using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace CultureDemo.Converters
{
   public class CalendarTypeToCalendarInformationConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var cal = value as Calendar;
         if (cal == null)
            return null;

         var calendarText = new StringBuilder(50);
         calendarText.Append(cal);
         calendarText.Remove(0, 21);   // Удалить пространство имен
         calendarText.Replace("Calendar", string.Empty);

         var gregorianCalendar = cal as GregorianCalendar;
         if (gregorianCalendar != null)
            calendarText.AppendFormat(" {0}", gregorianCalendar.CalendarType);

         return calendarText.ToString();
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}