using System.Collections.Generic;
using System.Threading.Tasks;
using FirstsStepsRUI.Models;

namespace FirstsStepsRUI.Repositories.Abstracts
{
   public interface IUserRepository
   {
      Task<User> LoginAsync(string userName, string unsecurePassword);
      Task<IList<Menu>> GetMenuByUserAsync(User user);
      Task<bool> SubmitAsync(User user);
   }
}