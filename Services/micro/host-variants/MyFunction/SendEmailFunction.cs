using System.Net.Mail;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Azure_MyFunction;

public class SendEmailFunction(ILogger<SendEmailFunction> logger, IConfiguration configuration)
{
   private readonly SmtpClient _smtpClient = new(configuration["EmailSettings:MailServer"]);

   private readonly string _emailAddress = configuration["EmailSettings:RecipientAddress"] ??
                                           throw new ArgumentNullException("EmailSettings:RecipiientAddress not set");

   [Function(nameof(SendEmailFunction))]
   public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
   {
      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var data = JsonSerializer.Deserialize<EmailModel>(
         requestBody,
         new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
      if (data is null)
      {
         return new BadRequestObjectResult("Invalid request payload.");
      }

      logger.LogInformation("Sending email to {EmailAddress} with subject: {DataSubject}", _emailAddress, data.Subject);
      var mailMessage = new MailMessage(_emailAddress, _emailAddress, data.Subject, data.Body);
      _smtpClient.Send(mailMessage);

      return new OkObjectResult("Welcome to Azure Functions!");
   }
}

public record EmailModel(string Subject, string Body);