namespace EF;

public class BusSpy : IBus
{
   private readonly List<string> _sentMessages = new();

   public void Send(string message)
   {
      _sentMessages.Add(message);
   }

   public BusSpy ShouldSendNumberOfMessages(int number)
   {
      Assert.Equal(number, _sentMessages.Count);
      return this;
   }

   public BusSpy WithEmailChangedMessage(int userId, string newEmail)
   {
      var message = "Type: USER EMAIL CHANGED; " +
                    $"Id: {userId}; " +
                    $"NewEmail: {newEmail}";
      Assert.Contains(_sentMessages, x => x == message);

      return this;
   }
}