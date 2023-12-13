namespace V2;

public interface IDomainLogger
{
   void UserTypeHasChanged(int userId, UserType oldType, UserType newType);
}