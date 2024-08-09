using System.Reactive.Linq;

namespace RxCreatingObservables.Chat;

public class ChatClient
{
   private readonly List<IChatConnection> _connections = [];

   public IChatConnection Connect(string user, string password)
   {
      Console.WriteLine("Connect");
      var chatConnection = new ChatConnection();
      _connections.Add(chatConnection);
      return chatConnection;
   }

   public IObservable<string> ObserveMessages(string user, string password)
   {
      var connection = Connect(user, password);
      return connection.ToObservable();
   }

   public IObservable<string> ObserveMessagesDeferred(string user, string password) =>
      Observable.Defer(() =>
      {
         var connection = Connect(user, password);
         return connection.ToObservable();
      });

   #region Testing Utils

   public void NotifyRecieved(string msg)
   {
      foreach (var chatConnection in _connections)
      {
         chatConnection.NotifyRecieved(msg);
      }
   }

   public void NotifyClosed()
   {
      foreach (var chatConnection in _connections)
      {
         chatConnection.NotifyClosed();
      }
   }

   public void NotifyError()
   {
      foreach (var chatConnection in _connections)
      {
         chatConnection.NotifyError();
      }
   }

   #endregion
}