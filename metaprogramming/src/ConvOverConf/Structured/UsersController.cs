using Microsoft.AspNetCore.Mvc;

namespace ConvOverConf.Structured;

[Route("/api/users")]
public class UsersController(IUsersService usersService, IUserDetailsService userDetailsService)
   : Controller
{
   [HttpPost("register")]
   public async Task Register([FromBody] RegisterUser userRegistration)
   {
      var userId = await usersService.Register(userRegistration.UserName, userRegistration.Password);
      await userDetailsService.Register(
         userRegistration.FirstName,
         userRegistration.LastName,
         userRegistration.SocialSecurityNumber,
         userId);
   }
}