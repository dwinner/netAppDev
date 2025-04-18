﻿using System.Threading.Tasks;
using AudioPlayerApp.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace AudioPlayerApp.Core
{
   public class AppStart : MvxAppStart
   {
      public AppStart(IMvxApplication application, IMvxNavigationService navigationService)
         : base(application, navigationService)
      {
      }

      protected override Task NavigateToFirstViewModel(object hint = null) =>
         NavigationService.Navigate<MainPageViewModel>();
   }
}