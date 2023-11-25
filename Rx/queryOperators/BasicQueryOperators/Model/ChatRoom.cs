using System;

namespace BasicQueryOperators.Model
{
   internal class ChatRoom
   {
      public string Id { get; set; }
      public IObservable<ChatMessage> Messages { get; set; }

      public override string ToString() => "ChatRoom: " + Id;
   }
}