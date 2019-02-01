using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Security;

namespace AppServices
{
   /// <summary>
   /// Поставщик членства
   /// </summary>
   public class SampleMembershipProvider : MembershipProvider
   {
      private readonly Dictionary<string, string> _users = new Dictionary<string, string>();
      internal static string ManagerUserName = "Manager".ToLowerInvariant();
      internal static string EmployeeUserName = "Employee".ToLowerInvariant();
      private string _applicationName = "test";

      public override void Initialize(string name, NameValueCollection config)
      {
         _users.Add(ManagerUserName, "secret@Pa$$w0rd");
         _users.Add(EmployeeUserName, "s0me@Secret");
         base.Initialize(name, config);
      }

      public override string ApplicationName
      {
         get { return _applicationName; }
         set { _applicationName = value; }
      }

      public override bool ChangePassword(string username, string oldPassword, string newPassword)
      {
         return true;
      }

      public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
      {
         return true;
      }

      public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
      {
         status = MembershipCreateStatus.ProviderError;
         return null;
      }

      public override bool DeleteUser(string username, bool deleteAllRelatedData)
      {
         return false;
      }

      public override bool EnablePasswordReset
      {
         get { return false; }
      }

      public override bool EnablePasswordRetrieval
      {
         get { return false; }
      }

      public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
      {
         totalRecords = default(int);
         return new MembershipUserCollection();
      }

      public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
      {
         totalRecords = default(int);
         return new MembershipUserCollection();
      }

      public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
      {
         totalRecords = default(int);
         return new MembershipUserCollection();
      }

      public override int GetNumberOfUsersOnline()
      {
         return -1;
      }

      public override string GetPassword(string username, string answer)
      {
         return string.Empty;
      }

      public override MembershipUser GetUser(string username, bool userIsOnline)
      {
         return null;
      }

      public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
      {
         return null;
      }

      public override string GetUserNameByEmail(string email)
      {
         return string.Empty;
      }

      public override int MaxInvalidPasswordAttempts
      {
         get { return -1; }
      }

      public override int MinRequiredNonAlphanumericCharacters
      {
         get { return -1; }
      }

      public override int MinRequiredPasswordLength
      {
         get { return -1; }
      }

      public override int PasswordAttemptWindow
      {
         get { return -1; }
      }

      public override MembershipPasswordFormat PasswordFormat
      {
         get { return default(MembershipPasswordFormat); }
      }

      public override string PasswordStrengthRegularExpression
      {
         get { return string.Empty; }
      }

      public override bool RequiresQuestionAndAnswer
      {
         get { return false; }
      }

      public override bool RequiresUniqueEmail
      {
         get { return false; }
      }

      public override string ResetPassword(string username, string answer)
      {
         return string.Empty;
      }

      public override bool UnlockUser(string userName)
      {
         return false;
      }

      public override void UpdateUser(MembershipUser user)
      {         
      }

      public override bool ValidateUser(string username, string password)
      {
         return _users.ContainsKey(username.ToLowerInvariant()) && password.Equals(_users[username.ToLowerInvariant()]);
      }
   }
}
