﻿namespace ContentViewSample.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new ContentViewSample.App());
      }
   }
}