using MyMicroservice_AspNetMvc.Data;
using MyMicroservice_AspNetMvc.Repositories;

namespace MyMicroservice_AspNetMvc.Controllers;

[Route("order")]
public class OrderController(IOrderRepository orderRepository) : Controller
{
   [HttpGet("/")]
   public IActionResult Index()
   {
      var orders = orderRepository.GetAllOrders();
      return View(orders);
   }

   [HttpGet("add")]
   public IActionResult Add()
   {
      var order = new Order();
      return View();
   }

   [HttpPost("add")]
   public IActionResult Add(Order order)
   {
      if (!ModelState.IsValid)
      {
         // Fix your errors first
         return View(order);
      }

      var saved = orderRepository.SaveOrder(order);
      return RedirectToAction(nameof(Index));
   }

   [HttpGet("edit/{id:int}")]
   public IActionResult Edit(int id)
   {
      var order = orderRepository.GetOrderById(id);
      if (order == null)
      {
         return View("NotFound");
      }

      return View(order);
   }

   [HttpPost("edit/{id:int}")]
   public IActionResult Edit(int id, Order order)
   {
      if (!ModelState.IsValid || id != order.Id)
      {
         // Fix your errors first
         return View(order);
      }

      var saved = orderRepository.SaveOrder(order);
      return RedirectToAction(nameof(Index));
   }

   [HttpGet("delete/{id:int}")]
   public IActionResult Delete(int id)
   {
      var order = orderRepository.GetOrderById(id);
      if (order == null)
      {
         return View("NotFound");
      }

      return View(order);
   }

   [HttpPost("delete/{id}")]
   public IActionResult Delete(int id, string? confirm)
   {
      orderRepository.DeleteOrder(id);
      return RedirectToAction(nameof(Index));
   }
}