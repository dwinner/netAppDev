namespace EF;

public class UserRepository
{
   private readonly CrmContext _context;

   public UserRepository(CrmContext context) => _context = context;

   public User GetUserById(int userId)
   {
      return _context.Users
         .SingleOrDefault(x => x.UserId == userId);
   }

   public void SaveUser(User user)
   {
      _context.Users.Update(user);
   }
}