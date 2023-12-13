namespace SampleProject;

public class MessageBus
{
   private static IBus _bus;

   public static void SendEmailChangedMessage(int userId, string newEmail)
   {
      _bus.Send($"Subject: USER; Type: EMAIL CHANGED; Id: {userId}; NewEmail: {newEmail}");
   }
}