using System;
using System.Windows.Controls;

namespace Controls
{
   public partial class DateControls
   {
      public DateControls()
      {
         InitializeComponent();
      }

      private void DatePicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
      {
         ErrorLabel.Text = string.Format("'{0}' is not a valid value because {1}", e.Text, e.Exception.Message);
      }

      private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
      {         
         foreach (DateTime selectedDate in e.AddedItems)
         {
            if ((selectedDate.DayOfWeek == DayOfWeek.Saturday) || (selectedDate.DayOfWeek == DayOfWeek.Sunday))
            {
               ErrorLabel.Text = "Weekends are not allowed";

               ((Calendar) sender).SelectedDates.Remove(selectedDate);
            }
         }
      }
   }
}