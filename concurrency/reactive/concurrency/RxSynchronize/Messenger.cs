namespace RxSynchronize;

internal class Messenger
{
   public event EventHandler<string> MessageReceived = delegate { };
   public event EventHandler<FriendRequest> FriendRequestReceived = delegate { };

   //Rest of the Messanger code
   public void Notify(string msg)
   {
      MessageReceived(this, msg);
   }

   public void Notify(FriendRequest user)
   {
      FriendRequestReceived(this, user);
   }
}

internal class FriendRequest
{
   public string UserId { get; set; }
}