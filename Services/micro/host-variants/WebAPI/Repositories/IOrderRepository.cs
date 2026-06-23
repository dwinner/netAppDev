using WebApi_Microservice.Data;

namespace MyMicroservice.Repositories;

public interface IOrderRepository
{
   void DeleteOrder(int id);
   List<Order> GetAllOrders();
   Order? GetOrderById(int id);
   int SaveOrder(Order order);
}