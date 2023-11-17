using System;
using System.Reactive;
using System.Reactive.Disposables;

namespace CreatingObservables.Chat;

public class ObservableConnection : ObservableBase<string>
{
   private readonly IChatConnection _chatConnection;

   public ObservableConnection(IChatConnection chatConnection) => _chatConnection = chatConnection;

   protected override IDisposable SubscribeCore(IObserver<string> observer)
   {
      var received = observer.OnNext;
      var closed = observer.OnCompleted;
      var error = observer.OnError;

      _chatConnection.Received += received;
      _chatConnection.Closed += closed;
      _chatConnection.Error += error;

      return Disposable.Create(() =>
      {
         _chatConnection.Received -= received;
         _chatConnection.Closed -= closed;
         _chatConnection.Error -= error;
         _chatConnection.Disconnect();
      });
   }
}