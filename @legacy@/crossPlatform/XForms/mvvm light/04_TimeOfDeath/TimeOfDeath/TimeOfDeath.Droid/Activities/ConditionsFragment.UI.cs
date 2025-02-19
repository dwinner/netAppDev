﻿using Android.Widget;

namespace TimeOfDeath.Droid
{
   public partial class ConditionsFragment
   {
      private Spinner bodyCondSpinner, foundAirSpinner, foundWaterSpinner, fromWaterSpinner;
      private Button calculateButton;

      public Spinner SpinBodyCondition =>
         bodyCondSpinner ?? (bodyCondSpinner = view.FindViewById<Spinner>(Resource.Id.bodyCondSpinner));

      public Spinner SpinFoundAir =>
         foundAirSpinner ?? (foundAirSpinner = view.FindViewById<Spinner>(Resource.Id.foundAirSpinner));

      public Spinner SpinFoundWater => foundWaterSpinner ??
                                       (foundWaterSpinner = view.FindViewById<Spinner>(Resource.Id.foundWaterSpinner));

      public Spinner SpinFromWater => fromWaterSpinner ??
                                      (fromWaterSpinner = view.FindViewById<Spinner>(Resource.Id.fromWaterSpinner));

      public Button BtnCalculate =>
         calculateButton ?? (calculateButton = view.FindViewById<Button>(Resource.Id.calculateButton));

      private void KillViews()
      {
         bodyCondSpinner = foundAirSpinner = foundWaterSpinner = fromWaterSpinner = null;
         calculateButton = null;
      }
   }
}