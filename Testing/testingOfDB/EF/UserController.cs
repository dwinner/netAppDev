namespace EF;

public class UserController
{
   private readonly CompanyRepository _companyRepository;
   private readonly CrmContext _context;
   private readonly EventDispatcher _eventDispatcher;
   private readonly UserRepository _userRepository;

   public UserController(
      CrmContext context,
      MessageBus messageBus,
      IDomainLogger domainLogger)
   {
      _context = context;
      _userRepository = new UserRepository(context);
      _companyRepository = new CompanyRepository(context);
      _eventDispatcher = new EventDispatcher(
         messageBus, domainLogger);
   }

   public string ChangeEmail(int userId, string newEmail)
   {
      var user = _userRepository.GetUserById(userId);

      var error = user.CanChangeEmail();
      if (error != null)
      {
         return error;
      }

      var company = _companyRepository.GetCompany();

      user.ChangeEmail(newEmail, company);

      _companyRepository.SaveCompany(company);
      _userRepository.SaveUser(user);
      _eventDispatcher.Dispatch(user.DomainEvents);

      _context.SaveChanges();
      return "OK";
   }
}