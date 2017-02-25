using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using FirstsStepsRUI.Models;
using FirstsStepsRUI.Repositories.Abstracts;
using FirstsStepsRUI.Repositories.Concretes;
using ReactiveUI;

namespace FirstsStepsRUI.ViewModels
{
   public class LoginViewModel : ReactiveObject, IRoutableViewModel
   {
      private PasswordBox _password;
      private User _user;
      private string _userName;

      public LoginViewModel(IScreen screen, IUserRepository userRepository)
      {
         HostScreen = screen;
         var canSubmit = this.WhenAny(m => m.UserName, m => m.Password, (user, password) => user.Value.IsValid());

         // We use "_" because we don't use the parameter
         Login = ReactiveCommand.CreateAsyncTask(canSubmit, _ => userRepository.LoginAsync(UserName, Password.Password));
         Login.ObserveOn(RxApp.MainThreadScheduler).Subscribe(user =>
         {
            User = user;
            HostScreen.Router.Navigate.Execute(new UserViewModel(HostScreen, user, userRepository));
         });

         // TODO use UserError.RegisterHandler
         Login.ThrownExceptions.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e => MessageBox.Show(e.Message));
      }

      public ReactiveCommand<User> Login { get; private set; }

      public User User
      {
         get { return _user; }
         set { this.RaiseAndSetIfChanged(ref _user, value); }
      }

      public string UserName
      {
         get { return _userName; }
         set { this.RaiseAndSetIfChanged(ref _userName, value); }
      }

      public PasswordBox Password
      {
         get { return _password; }
         set { this.RaiseAndSetIfChanged(ref _password, value); }
      }

      public IScreen HostScreen { get; private set; }

      public string UrlPathSegment
      {
         get { return "Login"; }
      }
   }
}