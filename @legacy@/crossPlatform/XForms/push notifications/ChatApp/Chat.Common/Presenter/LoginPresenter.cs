﻿using System;
using System.Threading;
using Chat.ServiceAccess.Web;

namespace Chat.Common.Presenter
{
   /// <summary>
   ///    Login presenter.
   /// </summary>
   public class LoginPresenter : BasePresenter
   {
      /// <summary>
      ///    The view.
      /// </summary>
      private ILoginView _view;

      /// <summary>
      ///    Initializes a new instance of the <see cref="LoginPresenter" /> class.
      /// </summary>
      /// <param name="state">State.</param>
      /// <param name="navigationService">Navigation service.</param>
      public LoginPresenter(ApplicationState state, INavigationService navigationService)
      {
         _navigationService = navigationService;
         _state = state;
         _webApiAccess = new WebApiAccess();
      }

      /// <summary>
      ///    Sets the view.
      /// </summary>
      /// <returns>The view.</returns>
      /// <param name="view">View.</param>
      public void SetView(ILoginView view)
      {
         _view = view;

         _view.Login -= HandleLogin;
         _view.Login += HandleLogin;
         _view.Register -= HandleRegister;
         _view.Register += HandleRegister;
      }

      /// <summary>
      ///    Handles the login.
      /// </summary>
      /// <returns>The login.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="user">User.</param>
      private async void HandleLogin(object sender, Tuple<string, string> user)
      {
         if (_view.IsInProgress)
         {
            return;
         }

         _state.Username = user.Item1;
         _view.IsInProgress = true;

         if (user.Item2.Length < 6)
         {
            _view.SetErrorMessage("Password must be at least 6 characters.");
         }
         else
         {
            var loggedIn = await _webApiAccess.LoginAsync(user.Item1, user.Item2, CancellationToken.None)
               .ConfigureAwait(true);
            if (loggedIn)
            {
               var tokenContract = await _webApiAccess.GetTokenAsync(user.Item1, user.Item2, CancellationToken.None)
                  .ConfigureAwait(true);

               if (!string.IsNullOrEmpty(tokenContract.AccessToken))
               {
                  var presenter = new ClientsListPresenter(_state, _navigationService,
                     tokenContract.AccessToken);
                  _navigationService.PushPresenter(presenter);
               }
               else
               {
                  _view.SetErrorMessage("Failed to register user.");
               }
            }
            else
            {
               _view.SetErrorMessage("Invalid username or password.");
            }
         }

         _view.IsInProgress = false;
      }

      /// <summary>
      ///    Handles the register.
      /// </summary>
      /// <returns>The register.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="user">User.</param>
      private async void HandleRegister(object sender, Tuple<string, string> user)
      {
         // make sure only once can we be registering at any one time
         if (!_view.IsInProgress)
         {
            _state.Username = user.Item1;
            _view.IsInProgress = true;

            if (user.Item2.Length >= 6)
            {
               var registerSuccess = await _webApiAccess.RegisterAsync(user.Item1, user.Item2, CancellationToken.None)
                  .ConfigureAwait(true);
               if (registerSuccess)
               {
                  _view.SetErrorMessage("User successfully registered.");
               }
            }
            else
            {
               _view.SetErrorMessage("Password must be at least 6 characters.");
            }

            _view.IsInProgress = false;
         }
      }

      /// <summary>
      ///    Login view.
      /// </summary>
      public interface ILoginView : IView
      {
         /// <summary>
         ///    Occurs when login.
         /// </summary>
         event EventHandler<Tuple<string, string>> Login;

         /// <summary>
         ///    Occurs when register.
         /// </summary>
         event EventHandler<Tuple<string, string>> Register;
      }
   }
}