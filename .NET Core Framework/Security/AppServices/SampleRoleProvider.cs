using System;
using System.Linq;
using System.Web.Security;

namespace AppServices
{
   /// <summary>
   /// Поставщик ролей
   /// </summary>
   public class SampleRoleProvider : RoleProvider
   {
      internal static string ManagerRoleName = "Manager".ToLowerInvariant();
      internal static string EmployeeRoleName = "Employee".ToLowerInvariant();
      private string _applicationName = "Test";

      public override void AddUsersToRoles(string[] usernames, string[] roleNames)
      {
      }

      public override string ApplicationName
      {
         get { return _applicationName; }
         set { _applicationName = value; }
      }

      public override void CreateRole(string roleName)
      {
      }

      public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
      {
         return true;
      }

      public override string[] FindUsersInRole(string roleName, string usernameToMatch)
      {
         return new string[0];
      }

      public override string[] GetAllRoles()
      {
         return new string[0];
      }

      public override string[] GetRolesForUser(string username)
      {
         if (string.Compare(username, SampleMembershipProvider.ManagerUserName, StringComparison.OrdinalIgnoreCase) == 0)
         {
            return new[] { ManagerRoleName };
         }

         return
            string.Compare(username, SampleMembershipProvider.EmployeeUserName, StringComparison.OrdinalIgnoreCase) == 0
               ? new[] { EmployeeRoleName }
               : new string[0];
      }

      public override string[] GetUsersInRole(string roleName)
      {
         return new string[0];
      }

      public override bool IsUserInRole(string username, string roleName)
      {
         string[] roles = GetRolesForUser(username);
         return roles.Any(role => string.Compare(role, roleName, StringComparison.OrdinalIgnoreCase) == 0);
      }

      public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
      {
      }

      public override bool RoleExists(string roleName)
      {
         return false;
      }
   }
}
