using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Common.DTOs;

namespace Post.Cmd.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EditCommentController(ILogger<EditCommentController> logger, ICommandDispatcher commandDispatcher)
   : ControllerBase
{
   [HttpPut("{id:guid}")]
   public async Task<ActionResult> EditCommentAsync(Guid id, EditCommentCommand command)
   {
      try
      {
         command.Id = id;
         await commandDispatcher.SendAsync(command);
         return Ok(new BaseResponse
         {
            Message = "Edit comment request completed successfully!"
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
      catch (AggregateNotFoundException ex)
      {
         logger.Log(LogLevel.Warning, ex,
            "Could not retrieve aggregate, client passed an incorrect post ID targetting the aggregate!");
         return BadRequest(new BaseResponse
         {
            Message = ex.Message
         });
      }
      catch (Exception ex)
      {
         const string safeErrorMessage = "Error while processing request to edit a comment on a post!";
         logger.Log(LogLevel.Error, ex, safeErrorMessage);
         return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
         {
            Message = safeErrorMessage
         });
      }
   }
}