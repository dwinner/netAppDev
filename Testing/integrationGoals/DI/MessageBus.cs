﻿namespace DI;

public class MessageBus
{
   private IBus _bus;

   public void SendEmailChangedMessage(int userId, string newEmail)
   {
      _bus.Send($"Subject: USER; Type: EMAIL CHANGED; Id: {userId}; NewEmail: {newEmail}");
   }
}