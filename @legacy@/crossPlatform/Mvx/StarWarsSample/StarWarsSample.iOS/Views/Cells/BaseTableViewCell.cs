﻿using System;
using MvvmCross.Platforms.Ios.Binding.Views;

namespace StarWarsSample.iOS.Views.Cells
{
   public class BaseTableViewCell : MvxTableViewCell
   {
      private bool _didSetupConstraints;

      protected BaseTableViewCell(IntPtr handle)
         : base(handle)
      {
         RunLifecycle();
      }

      private void RunLifecycle()
      {
         CreateView();
         SetNeedsUpdateConstraints();
      }

      public sealed override void UpdateConstraints()
      {
         if (!_didSetupConstraints)
         {
            CreateConstraints();
            _didSetupConstraints = true;
         }

         base.UpdateConstraints();
      }

      protected virtual void CreateView()
      {
      }

      protected virtual void CreateConstraints()
      {
      }
   }
}