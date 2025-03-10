﻿using System.Runtime.CompilerServices;

namespace ValidationDemo
{
   public class SomeDataWithNotifications : NotifyDataErrorInfoBase
   {
      private int val1;

      public int Val1
      {
         get { return val1; }
         set
         {
            SetProperty(ref val1, value);
            CheckVal1(val1, value);
         }
      }

      private async void CheckVal1(int oldValue, int newValue,
         [CallerMemberName] string propertyName = null)
      {
         ClearErrors(propertyName);
         var result = await ValidationSimulator.Validate(newValue, propertyName);
         if (result != null)
            SetError(result, propertyName);
      }
   }
}