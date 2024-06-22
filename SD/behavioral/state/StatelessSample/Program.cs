using Stateless;

namespace StatelessSample;

internal static class Program
{
   public static bool ParentsNotWatching { get; set; }

   private static void Main()
   {
      var stateMachine = new StateMachine<Health, Activity>(Health.NonReproductive);
      stateMachine
         .Configure(Health.NonReproductive)
         .Permit(Activity.ReachPuberty, Health.Reproductive);
      stateMachine
         .Configure(Health.Reproductive)
         .Permit(Activity.Historectomy, Health.NonReproductive)
         .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant,
            () => ParentsNotWatching);
      stateMachine.Configure(Health.Pregnant)
         .Permit(Activity.GiveBirth, Health.Reproductive)
         .Permit(Activity.HaveAbortion, Health.Reproductive);
   }
}