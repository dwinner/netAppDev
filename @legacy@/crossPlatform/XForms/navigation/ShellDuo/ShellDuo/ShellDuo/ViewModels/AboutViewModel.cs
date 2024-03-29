﻿using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShellDuo.ViewModels
{
   public class AboutViewModel : BaseViewModel
   {
      public AboutViewModel()
      {
         Title = "About";
         OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));
      }

      public ICommand OpenWebCommand { get; }

      protected override void UpdateLayouts()
      {
         UpdateLayouts(false, null);
      }
   }
}