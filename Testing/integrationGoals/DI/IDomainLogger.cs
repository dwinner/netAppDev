namespace DI;

internal interface IDomainLogger
{
   void UserTypeHasChanged(int userId, UserType oldType, UserType newType);
}