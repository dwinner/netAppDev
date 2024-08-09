using System.Reactive.Linq;

namespace HotAndColdObservables;

internal class ChatServer
{
   private static int _createdServers;

   public ChatServer() => ServerId = _createdServers++;

   public int ServerId { get; set; }

   public static ChatServer Current => new();

   public IObservable<string> ObserveMessages()
   {
      //"Never" is used so that the resulted observable will not complete, so  the observers wont be detached
      return Observable.Never<string>().StartWith("Message1", "Message2", "Message3")
         .Select(m => "Server" + ServerId + " - " + m);
   }
}