namespace RxCreatingObservables.Chat;

public static class ChatExtensions
{
   public static IObservable<string> ToObservable(this IChatConnection connection) =>
      new ObservableConnection(connection);
}