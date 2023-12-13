namespace DomainEvents;

internal interface IBus
{
   void Send(string message);
}