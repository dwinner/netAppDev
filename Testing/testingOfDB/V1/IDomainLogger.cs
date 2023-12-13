namespace V1;

public interface IDomainLogger
{
   void UserTypeHasChanged(int userId, UserType oldType, UserType newType);
}