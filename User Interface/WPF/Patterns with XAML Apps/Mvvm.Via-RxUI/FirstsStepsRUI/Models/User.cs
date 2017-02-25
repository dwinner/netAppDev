namespace FirstsStepsRUI.Models
{
   public class User
   {
      public User(int id, string code, bool blocked, UserGroup userGroup)
      {
         Id = id;
         Code = code;
         Blocked = blocked;
         Group = userGroup;
      }

      public int Id { get; private set; }
      public string Code { get; set; }
      public bool Blocked { get; private set; }
      public UserGroup Group { get; set; }

      public override string ToString()
      {
         return string.Format("{0}: {1}", Id, Code);
      }

      public static string GetGroupName(UserGroup group)
      {
         switch (group)
         {
            case UserGroup.Admin:
               return "Administrator";
            default:
               return group.ToString();
         }
      }
   }
}