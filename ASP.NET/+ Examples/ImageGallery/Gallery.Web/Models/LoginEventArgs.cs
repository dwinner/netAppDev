using System;
using Gallery.Web.Annotations;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Аргументы события при успешном входе или регистрации пользователя
   /// </summary>
   public class LoginEventArgs : EventArgs
   {
      [NotNull]
      private readonly string _userName;

      /// <summary>
      /// Имя пользователя, использованное при авторизации
      /// </summary>
      public string UserName
      {
         get { return _userName; }
      }

      /// <summary>
      /// Конструктор события при входе пользователя
      /// </summary>
      /// <param name="userName">Имя пользователя, использованное при авторизации</param>
      public LoginEventArgs(string userName)
      {
         _userName = userName;
      }
   }
}