namespace Transaction;

public class UserController
{
   private readonly CompanyRepository _companyRepository;
   private readonly EventDispatcher _eventDispatcher;
   private readonly Transaction _transaction;
   private readonly UserRepository _userRepository;

   public UserController(
      Transaction transaction,
      MessageBus messageBus,
      IDomainLogger domainLogger)
   {
      _transaction = transaction;
      _userRepository = new UserRepository(transaction);
      _companyRepository = new CompanyRepository(transaction);
      _eventDispatcher = new EventDispatcher(
         messageBus, domainLogger);
   }

   public string ChangeEmail(int userId, string newEmail)
   {
      var userData = _userRepository.GetUserById(userId);
      var user = UserFactory.Create(userData);

      var error = user.CanChangeEmail();
      if (error != null)
      {
         return error;
      }

      var companyData = _companyRepository.GetCompany();
      var company = CompanyFactory.Create(companyData);

      user.ChangeEmail(newEmail, company);

      _companyRepository.SaveCompany(company);
      _userRepository.SaveUser(user);
      _eventDispatcher.Dispatch(user.DomainEvents);

      _transaction.Commit();
      return "OK";
   }
}