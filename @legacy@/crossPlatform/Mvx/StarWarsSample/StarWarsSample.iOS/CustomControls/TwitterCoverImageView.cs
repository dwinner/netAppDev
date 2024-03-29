﻿using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace StarWarsSample.iOS.CustomControls
{
   public class TwitterCoverImageView : UIImageView
   {
      public static readonly nfloat DefaultCoverViewHeight = 160;

      private UIScrollView _scrollView;
      private UIView _topView;

      public TwitterCoverImageView()
      {
         Initialize(null);
      }

      public TwitterCoverImageView(UIView topView)
      {
         Initialize(topView);
      }

      public TwitterCoverImageView(CGRect frame)
         : base(frame)
      {
         Initialize(null);
      }

      public TwitterCoverImageView(CGRect frame, UIView topView)
         : base(frame)
      {
         Initialize(topView);
      }

      public nfloat CoverViewHeight { get; set; }

      public UIScrollView ScrollView
      {
         get => _scrollView;
         set
         {
            if (ScrollView != null)
            {
               ScrollView.RemoveObserver(this, "contentOffset");
            }

            _scrollView = value;
            ScrollView.AddObserver(this, "contentOffset", NSKeyValueObservingOptions.New, IntPtr.Zero);
         }
      }

      public override UIImage Image
      {
         get => base.Image;
         set => base.Image = value;
      }

      private void Initialize(UIView top)
      {
         _topView = top;
         CoverViewHeight = DefaultCoverViewHeight;
         ContentMode = UIViewContentMode.ScaleAspectFill;
         ClipsToBounds = true;
      }

      public override void LayoutSubviews()
      {
         base.LayoutSubviews();

         var windowWidth = ((AppDelegate) UIApplication.SharedApplication.Delegate).Window.Bounds.Width;

         if (ScrollView.ContentOffset.Y < 0)
         {
            var offset = -ScrollView.ContentOffset.Y;

            nfloat topViewHeight = 0;
            if (_topView != null)
            {
               _topView.Frame = new CGRect(0, -offset, windowWidth, _topView.Bounds.Size.Height);
               topViewHeight = _topView.Bounds.Size.Height;
            }

            Frame = new CGRect(-offset, -offset + topViewHeight, windowWidth + offset * 2, CoverViewHeight + offset);
         }
         else
         {
            nfloat topViewHeight = 0;
            if (_topView != null)
            {
               _topView.Frame = new CGRect(0, 0, windowWidth, _topView.Bounds.Size.Height);
               topViewHeight = _topView.Bounds.Size.Height;
            }

            Frame = new CGRect(0, topViewHeight, windowWidth, CoverViewHeight);
         }
      }

      public override void RemoveFromSuperview()
      {
         ScrollView.RemoveObserver(this, "contentOffset");
         if (_topView != null)
         {
            _topView.RemoveFromSuperview();
         }

         base.RemoveFromSuperview();
      }

      public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
      {
         SetNeedsLayout();
      }
   }
}