namespace LoggingV2;

internal interface IDomainLogger
{
   void UserTypeHasChanged(int userId, UserType oldType, UserType newType);
}