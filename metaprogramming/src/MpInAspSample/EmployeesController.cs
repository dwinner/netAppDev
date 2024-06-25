using Microsoft.AspNetCore.Mvc;

namespace MpInAspSample;

[Route("/api/employees")]
public class EmployeesController : Controller
{
   [HttpPost]
   public IActionResult Register(Employee employee) => Ok();

   [HttpGet]
   public IEnumerable<Employee> AllEmployees()
   {
      return new Employee[]
      {
         new("Jane", "Doe"),
         new("John", "Doe")
      };
   }
}