namespace Version1;

public interface IMessageBus
{
   void SendEmailChangedMessage(int userId, string newEmail);
}