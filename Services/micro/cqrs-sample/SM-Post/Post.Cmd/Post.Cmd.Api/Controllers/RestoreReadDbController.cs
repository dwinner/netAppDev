using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Common.DTOs;

namespace Post.Cmd.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RestoreReadDbController(ILogger<RestoreReadDbController> logger, ICommandDispatcher commandDispatcher)
   : ControllerBase
{
   [HttpPost]
   public async Task<ActionResult> RestoreReadDbAsync()
   {
      try
      {
         await commandDispatcher.SendAsync(new RestoreReadDbCommand());
         return StatusCode(StatusCodes.Status201Created, new BaseResponse
         {
            Message = "Read database restore request completed successfully!"
         });
      }
      catch (InvalidOperationException ex)
      {
         logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
         return BadRequest(new BaseResponse
         {
            Message = ex.Message
         });
      }
      catch (Exception ex)
      {
         const string safeErrorMessage = "Error while processing request to restore read database!";
         logger.Log(LogLevel.Error, ex, safeErrorMessage);
         return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
         {
            Message = safeErrorMessage
         });
      }
   }
}