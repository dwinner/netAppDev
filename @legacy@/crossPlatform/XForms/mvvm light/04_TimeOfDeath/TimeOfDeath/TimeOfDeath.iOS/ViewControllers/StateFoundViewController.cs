﻿using System;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace TimeOfDeath.iOS
{
   public partial class StateFoundViewController : UIViewController
   {
      public StateFoundViewController(IntPtr handle) : base(handle)
      {
      }

      private StateFoundViewModel ViewModel => AppDelegate.Locator.StateFound;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();
         CreateUI();
         CreateBindings();
      }

      public override void DidReceiveMemoryWarning()
      {
         base.DidReceiveMemoryWarning();
         // Release any cached data, images, etc that aren't in use.
      }

      private void CreateUI()
      {
         txtBody.EditingDidBegin += delegate
         {
            txtBody.InputView =
               PickerUI.CreateDropList(vwView, new UIPickerView(), txtBody, ViewModel.GetBodyLayers);
         };

         txtFoundAir.EditingDidBegin += delegate
         {
            txtFoundAir.InputView =
               PickerUI.CreateDropList(vwView, new UIPickerView(), txtFoundAir, ViewModel.GetFoundAir);
         };

         txtFoundWater.EditingDidBegin += delegate
         {
            txtFoundWater.InputView =
               PickerUI.CreateDropList(vwView, new UIPickerView(), txtFoundWater, ViewModel.GetFoundWater);
         };

         txtPulledWater.EditingDidBegin += delegate
         {
            txtPulledWater.InputView =
               PickerUI.CreateDropList(vwView, new UIPickerView(), txtPulledWater, ViewModel.GetPulledWater);
         };
      }

      private void CreateBindings()
      {
         this.SetBinding(
            () => ViewModel.FoundWater,
            () => txtFoundWater.Enabled,
            BindingMode.TwoWay);

         /*txtBody.SetBinding(
             () => ViewModel.BodyCondition,
             BindingMode.TwoWay).WhenSourceChanges(() => { txtFoundWater.Enabled = ViewModel.FoundWater; txtFoundAir.Enabled = ViewModel.Air; });*/

         this.SetBinding(
            () => ViewModel.BodyCondition,
            () => txtBody.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.PulledFromWater,
            () => txtPulledWater.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.FoundInAir,
            () => txtFoundAir.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.FoundInWater,
            () => txtFoundWater.Text,
            BindingMode.TwoWay);

         btnCalc.SetCommand("TouchUpInside", ViewModel.BtnCalculateResults);
      }
   }
}