using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace DateTimeSvc.Services;

public sealed class InviterService : Inviter.InviterBase
{
   public override Task<Response> Invite(Request request, ServerCallContext context)
   {
      var eventDateTime = DateTime.UtcNow.AddDays(1);
      var eventDuration = TimeSpan.FromHours(2);
      return Task.FromResult(new Response
      {
         Invitation = $"{request.Name}, invite you",
         Start = Timestamp.FromDateTime(eventDateTime),
         Duration = Duration.FromTimeSpan(eventDuration)
      });
   }
}