namespace Transaction;

public interface IDomainLogger
{
   void UserTypeHasChanged(int userId, UserType oldType, UserType newType);
}