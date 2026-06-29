using System.Diagnostics;
using Grpc.Core;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using Northwind.Grpc.Client.Mvc.Models;

// To use GrpcClientFactory.

// To use AsyncUnaryCall<T>.

namespace Northwind.Grpc.Client.Mvc.Controllers;

public class HomeController(ILogger<HomeController> logger, GrpcClientFactory factory)
   : Controller
{
   private readonly Employee.EmployeeClient _employeeClient = factory.CreateClient<Employee.EmployeeClient>("Employee");
   private readonly Greeter.GreeterClient _greeterClient = factory.CreateClient<Greeter.GreeterClient>("Greeter");
   private readonly Product.ProductClient _productClient = factory.CreateClient<Product.ProductClient>("Product");
   private readonly Shipper.ShipperClient _shipperClient = factory.CreateClient<Shipper.ShipperClient>("Shipper");

   public async Task<IActionResult> Index(string name = "Henrietta", int id = 1)
   {
      HomeIndexViewModel model = new();

      try
      {
         var reply = await _greeterClient.SayHelloAsync(
            new HelloRequest { Name = name });

         model.Greeting = $"Greeting from gRPC service: {reply.Message}";

         //ShipperReply shipperReply = await _shipperClient.GetShipperAsync(
         //  new ShipperRequest { ShipperId = id });

         // The same call as above but not awaited.
         AsyncUnaryCall<ShipperReply> shipperCall = _shipperClient.GetShipperAsync(
            new ShipperRequest { ShipperId = id },
            // Deadline must be a UTC DateTime.
            deadline: DateTime.UtcNow.AddSeconds(3));

         var metadata = await shipperCall.ResponseHeadersAsync;

         foreach (var entry in metadata)
         {
            // Not really critical, just doing this to make it easier to see.
            logger.LogCritical($"Key: {entry.Key}, Value: {entry.Value}");
         }

         var shipperReply = await shipperCall.ResponseAsync;

         model.ShipperSummary = "Shipper from gRPC service: " +
                                $"ID: {shipperReply.ShipperId}, Name: {shipperReply.CompanyName},"
                                + $" Phone: {shipperReply.Phone}.";
      }
      catch (RpcException rpcException) when (rpcException.StatusCode ==
                                       global::Grpc.Core.StatusCode.DeadlineExceeded)
      {
         logger.LogWarning("Northwind.Grpc.Service deadline exceeded.");
         model.ErrorMessage = rpcException.Message;
      }
      catch (Exception ex)
      {
         logger.LogWarning("Northwind.Grpc.Service is not responding.");
         model.ErrorMessage = ex.Message;
      }

      return View(model);
   }

   public async Task<IActionResult> Products(decimal minimumPrice = 0M)
   {
      var reply = await _productClient.GetProductsMinimumPriceAsync(
         new ProductsMinimumPriceRequest { MinimumPrice = minimumPrice });

      return View(reply.Products);
   }

   public async Task<IActionResult> Employees()
   {
      var reply = await _employeeClient.GetEmployeesAsync(
         new EmployeesRequest());

      return View(reply.Employees);
   }

   public IActionResult Privacy() => View();

   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   public IActionResult Error() => View(new ErrorViewModel
      { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}