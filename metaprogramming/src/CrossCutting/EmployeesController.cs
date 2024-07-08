using Microsoft.AspNetCore.Mvc;

namespace CrossCutting;

[Route("/api/employees")]
public class EmployeesController : Controller
{
   [HttpPost]
   public int Register([FromBody] Employee employee) => 1;
}