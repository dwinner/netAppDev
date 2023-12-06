namespace UsingMocks4;

public interface IEmailGateway
{
   void SendReceipt(string email, string productName, int quantity);
}