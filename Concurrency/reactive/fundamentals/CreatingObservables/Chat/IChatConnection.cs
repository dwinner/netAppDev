namespace RxCreatingObservables.Chat;

public interface IChatConnection
{
   event Action<string> Received;
   event Action Closed;
   event Action<Exception> Error;

   void Disconnect();

   #region Testing Utils

   void NotifyClosed();
   void NotifyError();
   void NotifyRecieved(string msg);

   #endregion
}